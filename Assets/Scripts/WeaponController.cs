using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject Weapon1, Weapon2;


    GameObject _activeWeapon;

    public GameObject activeWeapon
    {
        get { return _activeWeapon; }
    }

    void Start()
    {
        _activeWeapon = Weapon1;   
    }

    void Update()
    {
        
    }
}
