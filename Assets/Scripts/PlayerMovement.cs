using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : TacticMovement
{
    WeaponController weaponCtl;

    void Start()
    {
        Init();
        weaponCtl = this.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>();
    }

    void Update()
    {

        if (!turn)
        {
            return;
        }
        //TODO - Verificacao deve ocorrer uma unica vez e no inicio do turno
        if (!_movendo)
        {
            FindSelectableTiles();
            CheckMouse();
            CheckInput();
        }
        else
        {
            Move();
        }
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();
                    if (t.selecionavel)
                    {
                        MoveToTile(t);
                    }
                }

                if (hit.collider.tag == "NPC" || hit.collider.tag == "Zombie")
                {
                    AttackScript.Atacar(this.gameObject, hit.collider.gameObject);
                }
            }
        }
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponCtl.activeWeaponNumber = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponCtl.activeWeaponNumber = 1;
        }
    }


}
