using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public MapInfo info;
    public MapViewer viewer;
    

    [SerializeField] LayerMask tileMask;
    public Map visibleMaps;

    public Vector3 startTile, endTile;
    public bool choseTile;


    public List<Tile> selectedTiles = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MapSelecter();
        }
        else
        {
            choseTile = false;
            startTile = Vector3.one * -1;
            endTile = Vector3.one * -1;
            selectedTiles.Clear();
        }
    }

    public void MapSelecter()
    {
        TileStats tile = BuildLogic.OnTileSelect(tileMask);

        if (tile == null || tile.ID == endTile)
            return;

        if (!choseTile)
        {
            startTile = tile.ID;
            choseTile = true;
        }
    
        endTile = tile.ID;
        

        RedoBuildTiles();
        Build();
    }
    public void Build()
    {
        visibleMaps = info.map.maps[0];
        
        Vector3[] startEnd = BuildLogic. GetStart(startTile, endTile);

        Vector3 start = startEnd[0];
        Vector3 end = startEnd[1];

        BuildTiles(BuildLogic.GetEdges(start, end, visibleMaps.mapData, viewer.currentLevel));
        BuildTiles(BuildLogic.GetMiddle(start, end, visibleMaps.mapData, viewer.currentLevel));


    }
    public void BuildTiles(List<Tile> dataList)
    {
        Debug.Log(dataList.Count);

        foreach (Tile _Tile in dataList)
        {
            selectedTiles.Add(_Tile);
            _Tile.selected = true;
            BuildLogic.ChangeAlpha(1, _Tile.renderer);
        }
    }
    public void RedoBuildTiles()
    {
        foreach (Tile _Tile in selectedTiles)
        {
            _Tile.selected = !_Tile.selected;
            BuildLogic.ChangeAlpha(.33f, _Tile.renderer);
        }
        selectedTiles.Clear();
    }
}
