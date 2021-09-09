using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<GameObject> Weapons = new List<GameObject>();

    GameObject _activeWeapon;

    public GameObject activeWeapon
    {
        get { return _activeWeapon; }
    }

    void Start()
    {
        _activeWeapon = Weapons[0];   
    }

    void Update()
    {
        
    }
}
