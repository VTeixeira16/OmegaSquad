using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] string weaponName;
    [SerializeField] Sprite _photoWeapon;
    [SerializeField] int _alcanceArma, _danoArma, _precisaoArma, _cartuchoMax;
    [SerializeField] bool _grenadeLauncher, _scope;
    int _cartuchoQtd;

    public Sprite photoWeapon
    {
        get { return _photoWeapon; }
    }

    public int alcanceArma
    {
        get { return _alcanceArma; }
    }
    public int danoArma
    {
        get { return _danoArma; }
    }

    public int precisaoArma
    {
        get { return _precisaoArma; }
    }
    public int cartuchoQtd
    {
        get { return _cartuchoQtd; }
        set { _cartuchoQtd = value; }
    }
    public int cartuchoMax
    {
        get { return _cartuchoMax; }
        set { _cartuchoMax = value; }
    }

    void Start()
    {
        _cartuchoQtd = _cartuchoMax;
    }

}
