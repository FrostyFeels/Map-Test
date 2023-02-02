using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Attack[] attack;
    public WeaponShot[] weapons;
    public Ability[] abilities;

    public Attack selectedAttack;
    public WeaponShot selectedWeapon;
    public Ability selectedAbility;

    public bool active = false;
    public bool hasAttacked = false;

    public List<EnemyHealth> targets = new List<EnemyHealth>();

    [SerializeField] private LayerMask enemy;

    public void Start()
    {
        attack = new Attack[weapons.Length + abilities.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            attack[i] = weapons[i];
        }

        for (int i = weapons.Length; i < weapons.Length + abilities.Length; i++)
        {
            attack[i] = abilities[i - weapons.Length];
        }
    }
    public void Update()
    {
        if (active && targets.Count < selectedWeapon.targetAmount)
        {
            if (Input.GetMouseButtonDown(0))
                SelectTarget(true);

            if(Input.GetMouseButtonDown(1))
                SelectTarget(false);
        }
        
        if (active && Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedAttack.DoAttack(targets);
        }
            
    }

    public void SelectTarget(bool enable)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, enemy))
        {
            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

            if (!enable)
            {
                if (targets.Contains(health))
                    targets.Remove(health);
            }
            else
            {
                targets.Add(health);

            }         
            
        }
        else
            Debug.Log("no character found");
    }


}
