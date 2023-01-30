using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGen : MonoBehaviour
{
    public MapInfo info;
    public CameraMove cam;
    public MapHolder map;
    public MapSaved save;
    public GameObject mapHolder;
    public GameObject tilePrefab;
    public string mapName;
    public float alphaValue;

    public void Start()
    {
        foreach (Map _Map in map.maps)
        {
            _Map.mapData = new Tile[(int)map.mapSize.x, (int)map.mapSize.y, (int)map.mapSize.z];
        }
        map.CreateMap();
        CreateMap();

        //sets camera position so that the camera scripts is not dependent on this script since camera will be in multiple scripts
        cam.target.transform.position = new Vector3((map.mapSize.x / 2), 0, -map.mapSize.z);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            save.map = map;
    }


    //Loops through the map holder to get every map and every layer of every map to get every tile and makes a gameobject per tile
    //It adds a obj and renderer since those can not be saved
    //Add a ID to tilestats for later getting
    public void CreateMap()
    {
        int mapCount = 0;

        foreach (Map _Map in map.maps) //per map make a gameobject to save all the tiles in seperate folders
        {
            GameObject _MapPart = new GameObject();
            _MapPart.gameObject.name = "map part: " + mapCount;
            _MapPart.transform.SetParent(mapHolder.transform);

            foreach (Layer _Layer in _Map.layers)
            {
                GameObject _LayerPart = new GameObject(); //Per layer make a gameobject so we know where the object is in which layer
                _LayerPart.gameObject.name = "Layer part: " + _Layer.layerIndex ;
                _LayerPart.transform.SetParent(_MapPart.transform);

                foreach (Tile _Tile in _Layer.tiles) //Gets tile makes gameobject adds id sets position, csale and parent
                {
                    GameObject tile = Instantiate(tilePrefab);
                    tile.GetComponent<TileStats>().ID = new Vector3(_Tile.pos.x, _Tile.pos.y, _Tile.pos.z);
                    tile.transform.position = new Vector3(_Tile.pos.x, _Tile.pos.y, -_Tile.pos.z) * map.tileSize;
                    tile.transform.localScale = Vector3.one * map.tileSize;
                    tile.transform.SetParent(_LayerPart.transform);

                    //Add gameobject and renderer since it can't be saved
                    _Tile.obj = tile;
                    _Tile.renderer = tile.GetComponent<Renderer>();

                    //add into a 3D array so its easier to access without having to do weird caluclations to figure out which tile we need it a list
                    _Map.mapData[(int)_Tile.pos.x, (int)_Tile.pos.y, (int)_Tile.pos.z] = _Tile;

                    if(!_Tile.selected)
                        BuildLogic.ChangeAlpha(alphaValue, _Tile.renderer); //sets the alpha if needed
                }

                if (_Layer.layerIndex != 0)
                    _Layer.ChangeVisibility(false, false);

            }
            mapCount++;
        }
        mapCount = 0;

        info.map = map;
    }
}
