using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacters : BaseCharacters
{
    //TODO - Implementar sistema de experiencia
    private int _level, _xp;

    public int level
    {
        get { return _level; }
        set { _level = value; }
    }
    public int xp
    {
        get { return _xp; }
        set { _xp = value; }
    }

    new void Start()
    {
        base.Start();
        _hp = 7;
        _precisaoUnidade = 5;
        _defesa = 4;
        _visao = 40;
    }

    new void Update()
    {
        base.Update();
    }
}
