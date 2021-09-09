using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<GameObject> Weapons = new List<GameObject>();

    GameObject _activeWeapon;
    int  _activeWeaponNumber;

    public int activeWeaponNumber
    {
        get { return _activeWeaponNumber; }
        set { _activeWeaponNumber = value; }
    }

    public GameObject activeWeapon
    {
        get { return _activeWeapon; }
        set { _activeWeapon = value; }
    }

    void Start()
    {
        _activeWeapon = Weapons[0];   
    }

    void Update()
    {
        _activeWeapon = Weapons[_activeWeaponNumber];
    }
}
