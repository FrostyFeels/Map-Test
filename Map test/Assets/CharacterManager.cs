using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public Character selected;

    [SerializeField] private LayerMask player;


    public void Start()
    {
        foreach (Transform _obj in transform)
        {
            characters.Add(_obj.GetComponent<Character>());
            
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            select();
    }

    public void select()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, player))
        {
            Character _select = hit.collider.GetComponent<Character>();

            if (selected != null && selected == _select)
                return;

            if(selected == null)
            {
                selected = _select;
                selected.selected = true;
                return;
            }

            selected.selected = false;
            selected.OnDeSelect();
            selected = _select;
            selected.selected = true;
        }
        else
            Debug.Log("no character found");
    }
}
