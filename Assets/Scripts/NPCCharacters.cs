using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacters : BaseCharacters
{
    new void Start()
    {
        base.Start();
        _hp = 9;
        _precisaoUnidade = 5;
        _defesa = 3;
        _visao = 40;
    }
}
