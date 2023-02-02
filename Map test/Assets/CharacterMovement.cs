using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MapSaved savemap;
    public Character character;


    public Tile[,,] curMap;
    [SerializeField] private LayerMask tiles;
    [SerializeField] private Vector3 lastTile;
    [SerializeField] private Vector3 curTile;
    public List<Vector3> path;


    [SerializeField] private LineRenderer line;
    public int moves;

    public bool active;
    public bool hasMoved;

    List<Tile> inRangeTiles = new List<Tile>();
    List<Tile> coloredPath = new List<Tile>();

    public void Start()
    {
        //setting the curTile to the players position and then making the z negative for tile id standards
        curTile = character.transform.position;
        curTile.y = character.transform.position.y - 1;
        curTile.z *= -1;
        path.Add(curTile);
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && active)
            DragPath();
    }


    //this function adds the paths for the character to walk on
    public void DragPath()
    {
        TileStats _Tile = BuildLogic.OnTileSelect(tiles);

        //Check if tile is null
        if (_Tile == null)
            return;

        Vector3 tile = _Tile.ID;

        if (curTile == tile)
            return;

        //Distance check
        if (Vector2.Distance(new Vector2(tile.x, tile.z), new Vector2(curTile.x, curTile.z)) > 1)
            return;

        //checks if tile is the previous tile to shorten the path
        if (tile == lastTile)
        {
            path.RemoveAt(path.Count - 1);

            if (path.Count > 1)
                lastTile = path[path.Count - 2];

            curTile = tile;
            SetLinePositions(path);
            return;
        }

        //makes sure the path isnt too long
        if (moves + 1 <= path.Count)
            return;

        lastTile = curTile;
        curTile = tile;
        path.Add(curTile);
        SetLinePositions(path);
    }

    //Sets the line position for a line
    public void SetLinePositions(List<Vector3> _path)
    {
        UnShowPath();
        int count = 0;

        line.positionCount = _path.Count;
        Vector3[] linepos = new Vector3[_path.Count];

        foreach (Vector3 _pos in _path)
        {
            linepos[count] = new Vector3((int)_pos.x, (int)_pos.y + 1, -_pos.z) * savemap.map.tileSize;
            count++;
        }
        line.SetPositions(linepos);
        ShowPath();
    }

    //Colors amd uncolors the line the character will walk
    public void ShowPath()
    {
        foreach (Vector3 _Pos in path)
        {
            Vector3 id = new Vector3(_Pos.x, 0, _Pos.z);

            Map map = BuildLogic.GetMap(id, savemap.map);
            id = BuildLogic.GetID(id, savemap.map);

            Tile tile = map.mapData[(int)id.x, (int)id.y, (int)id.z];

            BuildLogic.ChangeColor("Path", tile.renderer);
            coloredPath.Add(tile);
        }

    }
    public void UnShowPath()
    {
        if(active)
            BuildLogic.ChangeColor("PathRange", coloredPath);
        else
            BuildLogic.ChangeColor("White", coloredPath);
        coloredPath.Clear();
    }

    //shows and unshows the range available
    public void ShowRange()
    {
        Vector3 middle = path[0];

        Vector3 start = new Vector3(path[0].x - character.movementSpeed, 0, path[0].z - character.movementSpeed);
        Vector3 end = new Vector3(path[0].x + character.movementSpeed, 0, path[0].z + character.movementSpeed);

        if(start.x < 0)
            start.x = 0;

        if (start.z < 0)
            start.z = 0;

        if (end.x > savemap.map.mapSize.x * savemap.map.holderSize.x)
            end.x = savemap.map.mapSize.x * savemap.map.holderSize.x;

        if (end.z > savemap.map.mapSize.z * savemap.map.holderSize.y)
            end.z = savemap.map.mapSize.z * savemap.map.holderSize.y;

        int xDis = 0;
        int zDis = 0;
        int fullDiss;

        for (int z = (int)start.z; z < (int)end.z; z++)
        {
            for (int x = (int)start.x; x < (int)end.x; x++)
            {
                xDis = Mathf.Abs(((int)middle.x - x));
                zDis = Mathf.Abs(((int)middle.z - z));
                fullDiss = xDis + zDis;


                if (fullDiss <= character.movementSpeed)
                {
                    Vector3 id = new Vector3(x, 0, z);

                    Map map = BuildLogic.GetMap(id, savemap.map);
                    id = BuildLogic.GetID(id, savemap.map);

                    Tile tile = map.mapData[(int)id.x, (int)id.y, (int)id.z];

                    BuildLogic.ChangeColor("PathRange", tile.renderer);
                    inRangeTiles.Add(tile);
                }
            }
        }
    }
    public void UnShowRange()
    {
        BuildLogic.ChangeColor("White", inRangeTiles);
        inRangeTiles.Clear();
    }

    //When we get selected and deselected
    public void DeSelect()
    {
        line.positionCount = 0;
        active = false;
        UnShowRange();
        UnShowPath();

    }
    public void OnSelect()
    {
        active = true;
        SetLinePositions(path);
        ShowRange();
       
        
    }
}
