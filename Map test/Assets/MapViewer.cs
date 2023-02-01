using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewer : MonoBehaviour
{
    public MapInfo info;
    public int currentLevel;
    public List<Map> visibleMaps = new List<Map>();


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeLevel(-1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeLevel(1);
    }

    public void ChangeLevel(int increase)
    {
        int highestHight = 0;
        visibleMaps = info.map.maps; //Make this changed in the world by clicking maps

        foreach (Map _Map in visibleMaps)
        {
            if (_Map.height > currentLevel)
                if(increase > 0)
                {
                    _Map.layers[currentLevel].ChangeVisibility(false, true);
                }
                else
                {
                    _Map.layers[currentLevel].ChangeVisibility(false, false);
                }
                

            if (_Map.height > highestHight)
                highestHight = _Map.height;
        }

        if(currentLevel + increase >= 0 && (highestHight > currentLevel + increase))
            currentLevel += increase;

        foreach (Map _Map in visibleMaps)
        {
            if (_Map.height > currentLevel)
                _Map.layers[currentLevel].ChangeVisibility(true, true);
        }

        ChangeVisibility();
        
    }    
    public void ChangeVisibility()
    {
        foreach (Map _Map in visibleMaps)
        {
            foreach (Layer _Layer in _Map.layers)
            {
                foreach (Tile _Tile in _Layer.tiles)
                {
                    Debug.Log("runs");
                    if (!_Tile.selected)
                        BuildLogic.ChangeColor("UnSelected", _Tile.renderer);
                }
            }
        }
    }
}
