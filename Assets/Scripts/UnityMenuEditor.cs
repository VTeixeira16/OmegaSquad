using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnityMenuEditor : MonoBehaviour
{
#if UNITY_EDITOR

    [MenuItem("Tools/Reload Map")]
    public static void ReloadMap()
    {
        DeleteMap();
        CreateMap();
    }

    public static void CreateMap()
    {
        int mapColumn = 20;
        int mapLine = 20;
        int c = 0;
        int l = 0;

        //Deve retornar só um
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        GameObject tilePrefab = map.GetComponent<MapObjects>().getPrefabTile();
        GameObject plane = map.GetComponent<MapObjects>().getPlane();

        for(c = 0; c < mapColumn; c++)
        {
            for (l = 0; l < mapLine; l++)
            {
                string tileName = "Tile_" + c + "_" + l;

                GameObject tile = Instantiate(tilePrefab, map.transform.position, tilePrefab.transform.rotation, map.transform);
                tile.name = tileName;
                tile.transform.position = new Vector3(c, tilePrefab.transform.position.y, l);
            }
        }

        float planeX = (mapColumn / 2) - 0.5f;
        float planeY = (mapLine / 2) - 0.5f;
        float sizeX = (mapColumn / 10) + 1;
        float sizeY = (mapLine / 10) + 1;
        plane.transform.position = new Vector3(planeX, 0, planeY);
        plane.transform.localScale = new Vector3(sizeX, 1, sizeY);
    }

    public static void DeleteMap()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
 
        foreach (GameObject t in tiles)
        {
            DestroyImmediate(t.gameObject);
        }
    }



    // TODO - Avaliar real necessidade, pois informacoes podem ser armazenadas em prefab
    [MenuItem("Tools/Assign Tile Material")]
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");

        foreach(GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }

    [MenuItem("Tools/Assign Tile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.AddComponent<TileScript>();
        }
    }




#endif
}
