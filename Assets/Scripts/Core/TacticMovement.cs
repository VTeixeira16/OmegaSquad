using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticMovement : MonoBehaviour
{
    List<TileScript> tilesSelecionaveis = new List<TileScript>();
    GameObject[] tiles;

    protected Stack<TileScript> path = new Stack<TileScript>();
    TileScript tileAtual;

    [SerializeField] protected bool _movendo = false;
    [SerializeField] protected int _movementRange = 5;
    [SerializeField] protected float _velMovimento = 7;

    protected Vector3 velocity = new Vector3();
    protected Vector3 heading = new Vector3();

    protected float halfHeight = 0;

    protected TileScript actualTargetTile;

    bool calculouTiles = false;

    protected bool _turn = false;

    protected BaseCharacters baseCharacters;

    public bool movendo
    {
        get { return _movendo;}
        set { _movendo = value;}
    }

    public int movementRange
    {
        get { return _movementRange; }
    }
    public float velMovimento
    {
        get { return _velMovimento; }
    }

    public bool turn
    {
        get { return _turn; }
        set { _turn = value; }
    }

    protected void Awake()
    {
        baseCharacters = this.GetComponent<BaseCharacters>();
    }

    protected void Update()
    {
        if (turn && baseCharacters.qtdMovimentos <= 0 && baseCharacters.acoes <= 0)
        {
            TurnManager.EndTurn();
        }
        if(this.tag == "Zombie")
        {
            // Debug.Log("baseCharacters.qtdMovimentos" + baseCharacters.qtdMovimentos);
            // Debug.Log("baseCharacters.acoes" + baseCharacters.acoes);
        }
    }

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        // halfHeight = GetComponent<Collider>().bounds.extents.y;
        halfHeight = 0.01f;

        TurnManager.AddUnit(this);
    }

    public void GetTileAtual()
    {
        tileAtual = GetTileAlvo(gameObject);
        tileAtual.atual = true;
    }

    public TileScript GetTileAlvo(GameObject alvo)
    {
        RaycastHit hit;
        TileScript tile = null;

        if(Physics.Raycast(alvo.transform.position, Vector3.down, out hit, 1))
        {
            tile = hit.collider.GetComponent<TileScript>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(TileScript alvo)
    {
        // tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            TileScript t = tile.GetComponent<TileScript>();
            t.FindNeighbors(alvo);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(null);
        GetTileAtual();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(tileAtual);
        tileAtual.visited = true;
        
        while (process.Count > 0)
        {
            TileScript t = process.Dequeue();

            tilesSelecionaveis.Add(t);
            t.selecionavel = true;

            if(t.distance < _movementRange)
            {
                foreach(TileScript tile in t.adjacencyList)
                {
                    if(!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    protected void MoveToTile(TileScript tile)
    {
        path.Clear();
        tile.alvo = true;
        _movendo = true;
        AttackScript.SetConfirmacaoAtaque(false);
        TurnManager.SetActualTargetAttack(null);

        TileScript next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    protected void Move()
    {
        if(path.Count > 0)
        {
            TileScript t = path.Peek();
            Vector3 alvo = t.transform.position;

            //Calculate the unit's position on top of the target tile
            alvo.y += (halfHeight )+ t.GetComponent<Collider>().bounds.extents.y;

            if(Vector3.Distance(transform.position, alvo) >= 0.05f)
            {
                CalculateHeading(alvo);
                SetHorizontalVelocity();

                //transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                //Tile center reached
                transform.position = alvo;
                path.Pop();
            }

        }
        else
        {
            //todo remove the selectable tiles
            RemoveSelectableTiles();
            movendo = false;
            baseCharacters.qtdMovimentos--;
        }
    }

    protected void RemoveSelectableTiles()
    {
        if(tileAtual != null)
        {
            tileAtual.atual = false;
            tileAtual = null;
        }

        foreach(TileScript tile in tilesSelecionaveis)
        {
            tile.Reset();
        }

        tilesSelecionaveis.Clear();
    }

    protected void CalculateHeading(Vector3 alvo)
    {
        heading = alvo - transform.position;
        heading.Normalize();
    }

    protected void SetHorizontalVelocity()
    {
        velocity = heading * _velMovimento;
    }

    protected TileScript FindLowestF(List<TileScript> list)
    {
        TileScript lowest = list[0];

        foreach (TileScript t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected TileScript FindEndTile(TileScript t)
    {
        Stack<TileScript> tempPath = new Stack<TileScript>();

        TileScript next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= _movementRange)
        {
            return t.parent;
        }

        TileScript endTile = null;
        for (int i = 0; i <= _movementRange; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(TileScript target)
    {
        ComputeAdjacencyLists(target);
        GetTileAtual();

        List<TileScript> openList = new List<TileScript>();
        List<TileScript> closedList = new List<TileScript>();

        openList.Add(tileAtual);
        //currentTile.parent = ??
        tileAtual.h = Vector3.Distance(tileAtual.transform.position, target.transform.position);
        tileAtual.f = tileAtual.h;

        while (openList.Count > 0)
        {
            TileScript t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (TileScript tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }

        //todo - what do you do if there is no path to the target tile?
        Debug.Log("Path not found");
    }
    public void BeginTurn()
    {
        calculouTiles = false;
        turn = true;
        TurnManager.SetActualUnit(gameObject);

        if (baseCharacters.hp > 0)
        {
            baseCharacters.qtdMovimentos = baseCharacters.qtdMovimentosBase;
            baseCharacters.acoes = baseCharacters.acoesBase;
        }
        else
        {
            baseCharacters.qtdMovimentos = 0;
            baseCharacters.acoes = 0;
        }
    }

    public void EndTurn()
    {
        _turn = false;
    }
}

