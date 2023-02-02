using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterMovement movement;
    public CharacterAttack attack;

    public string characterName;
    public float health;
    public int movementSpeed;
    public float damage;

    public bool selected;

    public void Start()
    {
        movement.moves = movementSpeed;
    }

    public void Update()
    {
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                movement.active = true;
                movement.OnSelect();
            }
               

            if (Input.GetKeyDown(KeyCode.Alpha2))
                attack.active = true;
        }
    }

    public void OnDeSelect()
    {
        movement.DeSelect();
    }

    public void CreateMoves()
    {
        int moves = 1 + attack.attack.Length;


    }

    

    public void ShowMoves()
    {

    }

    public void ShowAttacks()
    {

    } 
}
