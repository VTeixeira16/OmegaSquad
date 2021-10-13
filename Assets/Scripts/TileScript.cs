using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    //Estados do Tile. Dependendo da estrutura do projeto, informacoes podem ser migradas para um Enum.
    [SerializeField] private bool _andavel = true;
    [SerializeField] private bool _atual = false;
    [SerializeField] private bool _alvo = false;
    [SerializeField] private bool _selecionavel = false;

    public List<TileScript> adjacencyList = new List<TileScript>();

    //Necessario para BFS (Breadth first search
    [SerializeField] private bool _visited = false;
    [SerializeField] private TileScript _parent = null;
    [SerializeField] private int _distance = 0;

    //For A*
    float _f = 0;
    float _g = 0;
    float _h = 0;

    MeshRenderer meshRender;

    public float f
    {
        get { return _f; }
        set { _f = value; }
    }
    public float g
    {
        get { return _g; }
        set { _g = value; }
    }
    public float h
    {
        get { return _h; }
        set { _h = value; }
    }

    public bool visited
    {
        get { return _visited;}
        set { _visited = value;}
    }
    public TileScript parent
    {
        get { return _parent;}
        set { _parent = value;}
    }
    public int distance
    {
        get { return _distance;}
        set { _distance = value;}
    }

    public bool andavel
    {
        get { return _andavel;}
        set { _andavel = value;}
    }

    public bool atual
    {
        get { return _atual;}
        set { _atual = value;}
    }
    public bool alvo
    {
        get { return _alvo;}
        set { _alvo = value;}
    }
    public bool selecionavel
    {
        get { return _selecionavel;}
        set { _selecionavel = value;}
    }

    void Start()
    {
        meshRender = this.GetComponent<MeshRenderer>();

    }

    void Update()
    {
        //Aplica Cores nos tiles de acordo com Variaveis
        if(_atual)
        {
            meshRender.enabled = true;
            GetComponent<Renderer>().material.color = Color.magenta;

        }
        else if(_alvo)
        {
            meshRender.enabled = true;
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(_selecionavel)
        {
            meshRender.enabled = true;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            meshRender.enabled = false;
            GetComponent<Renderer>().material.color = Color.white;
        }
    }


    public void Reset()
    {
        adjacencyList.Clear();

        _atual = false;
        _alvo = false;
        _selecionavel = false;
        
        _visited = false;
        _parent = null;
        _distance = 0;
    }
    public void FindNeighbors(TileScript alvo)
    {
        //Variavel float jumpHeight tem a utilidade de analisar a altura e se e necessario pular.
        //O jumpHeight tambem pode ser usado para alguns movimentos especificos, como voos de personagens, pulo ninja e etc.
        Reset();

        CheckTile(Vector3.forward, alvo);
        CheckTile(Vector3.back, alvo);
        CheckTile(Vector3.right, alvo);
        CheckTile(Vector3.left, alvo);

    }

    public void CheckTile(Vector3 direction,TileScript alvo)
    {

        //Cria um colisor partindo do centro do objeto atual ate o centro do proximo objeto.
        //Dependendo do tipo de verificacao de obstaculo, podera ser necessario aumentar para que seja alcancado o tile todo
        Vector3 halfExtents = new Vector3(0.25f, 1 / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider item in colliders)
        {
            TileScript tile = item.GetComponent<TileScript>();
            if(tile != null && tile.andavel)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == alvo))
                //if (!Physics.Raycast(tile.GetComponent<Collider>().transform.position, Vector3.up, out hit, 1) || (tile == alvo))
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}
