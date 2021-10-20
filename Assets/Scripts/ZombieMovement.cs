using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : NPCMovement
{ 

    new void Start()
    {
        base.Start();

        //TODO Migrar pra init
        unitState = UnitStates.Vagando;

        // Debug.Log("Zumbis ativos: " + TurnManager.GetActiveZombies());
    }

    new void Update()
    {
        //TODO - Como implementacao de estados de zumbis estava com muitos bugs, sera retomada posteriormente
        //Confirmar se Update funciona corretamente, pois NPCMovement herda TactcMovement
        base.Update();
    }
}
