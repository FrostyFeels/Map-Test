using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterMovement movement;

    public string characterName;
    public float health;
    public int movementSpeed;
    public float damage;

    public void Start()
    {
        movement.moves = movementSpeed;
    }

 
}
