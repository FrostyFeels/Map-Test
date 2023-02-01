using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MapSaved savemap;
    public Character character;


    [SerializeField] private LayerMask tiles;

    public List<Vector3> path;
    [SerializeField] private Vector3 lastTile;
    [SerializeField] private Vector3 curTile;

    [SerializeField] private LineRenderer line;
    public int moves;

    public void Start()
    {
        curTile = character.transform.position;
        curTile.z *= -1;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
            DragPath();
    }

    public void DragPath()
    {
        TileStats _Tile = BuildLogic.OnTileSelect(tiles);

        if (_Tile == null)
            return;

        Vector3 tile = _Tile.ID;
        Debug.Log(tile + " " + Vector2.Distance(new Vector2(tile.x, tile.z), new Vector2(curTile.x, curTile.z)));

        if (curTile == tile)
            return;

        if (Vector2.Distance(new Vector2(tile.x, tile.z), new Vector2(curTile.x, curTile.z)) > 1)
            return;

        if (tile == lastTile)
        {
            path.RemoveAt(path.Count - 1);

            if(path.Count > 1) 
                lastTile = path[path.Count - 2];

            curTile = tile;
            SetLinePositions(path);
            return;
        }

        if (moves + 1 <= path.Count)
            return;

        lastTile = curTile;
        curTile = tile;
        path.Add(curTile);
        SetLinePositions(path);
    }

    public void SetLinePositions(List<Vector3> _path)
    {
        int count = 0;

        line.positionCount = _path.Count;
        Vector3[] linepos = new Vector3[_path.Count];

        foreach (Vector3 _pos in _path)
        {
            Tile tile = savemap.currentMap[(int)_pos.x, (int)_pos.y, (int)_pos.z];
            linepos[count] = new Vector3(tile.pos.x, (int)_pos.y + 1, -tile.pos.z) * savemap.map.tileSize;
            count++;
        }
        line.SetPositions(linepos);
    }
}
