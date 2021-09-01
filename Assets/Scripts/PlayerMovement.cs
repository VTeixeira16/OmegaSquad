using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : TacticMovement
{
    void Start()
    {
        Init();
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
            }
        }
    }


}
