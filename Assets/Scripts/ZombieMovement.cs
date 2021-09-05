using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : NPCMovement
{ 
    enum ZombieStates : int
    {
        Vagando, // TODO - Implementar
        Perseguindo, // TODO - Implementar
        Movendo,
        Atacando, // TODO - Implementar
        Idle, 
        Morrendo, // TODO - Implementar
        Morto // TODO - Implementar
    }

    ZombieStates zombieState;

    new void Start()
    {
        base.Start();

        //TODO Migrar pra init
        zombieState = ZombieStates.Vagando;

        // Debug.Log("Zumbis ativos: " + TurnManager.GetActiveZombies());
    }

    new void Update()
    {
        //TODO - Como implementacao de estados de zumbis estava com muitos bugs, sera retomada posteriormente
        base.Update();
    }
}
