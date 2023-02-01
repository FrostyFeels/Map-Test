using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public MapInfo info;
    public MapViewer viewer;
    public MapHolder holder;

    public List<GameObject> selectBoxes = new List<GameObject>();
    [SerializeField] private LayerMask maps;

    public void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            ShowMaps();

        if (Input.GetMouseButtonDown(0))
            SelectMap();
    }

    public void ShowMaps()
    {
        info.currentMap = null;

        for (int i = viewer.currentLevel; i > 0; i--)
            viewer.ChangeLevel(-1);            
      
        foreach (GameObject _Box in selectBoxes)
            _Box.SetActive(true);    
    }

    public void SelectMap()
    {
        holder = info.map;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, maps))
        {
            info.currentMap = holder.maps[int.Parse(hit.collider.gameObject.name)];

            foreach (GameObject _Box in selectBoxes)
                _Box.SetActive(false);


            foreach (Map _Map in info.map.maps)
                if (_Map != info.currentMap)
                    _Map.HideMap(false);
                else
                    _Map.HideMap(true);
        }
        else
            Debug.Log("No select found");
     
    }

}
