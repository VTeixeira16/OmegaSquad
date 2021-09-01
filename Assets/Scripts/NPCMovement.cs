using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : TacticMovement
{

    GameObject alvo;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (!turn)
        {
            return;
        }
        //TODO - Verificacao deve ocorrer uma unica vez e no inicio do turno
        if (!_movendo)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
        }
        else
        {
            Move();
        }
    }

    void CalculatePath()
    {
        TileScript targetTile = GetTileAlvo(alvo);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        alvo = nearest;
    }
}