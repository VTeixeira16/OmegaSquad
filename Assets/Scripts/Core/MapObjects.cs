using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjects : MonoBehaviour
{
    [SerializeField] GameObject prefabTile;
    [SerializeField] GameObject plane;

    public GameObject getPrefabTile()
    {
        return prefabTile;
    }

    public GameObject getPlane()
    {
        return plane;
    }
}
