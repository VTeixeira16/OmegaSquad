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
    [SerializeField] protected int _movimentos = 5;
    [SerializeField] protected float _alturaPulo = 2;
    [SerializeField] protected float _velMovimento = 7;
    [SerializeField] protected float _velPulo = 4.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpAlvo;

    protected TileScript actualTargetTile;

    bool calculouTiles = false;

    protected bool _turn = false;
    protected int _acoes;

    public bool movendo
    {
        get { return _movendo;}
        set { _movendo = value;}
    }

    public int movimentos
    {
        get { return _movimentos; }
    }
    public float alturaPulo
    {
        get { return _alturaPulo; }
    }
    public float velMovimento
    {
        get { return _velMovimento; }
    }

    public float velPulo
    {
        get { return _velPulo; }
        set { _velPulo = value; }
    }

    public bool turn
    {
        get { return _turn; }
        set { _turn = value; }
    }

    public int acoes
    {
        get { return _acoes; }
        set { _acoes = value; }
    }

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

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

    public void ComputeAdjacencyLists(float jumpHeight, TileScript alvo)
    {
        // tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            TileScript t = tile.GetComponent<TileScript>();
            t.FindNeighbors(alturaPulo, alvo);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(_alturaPulo, null);
        GetTileAtual();

        Queue<TileScript> process = new Queue<TileScript>();

        process.Enqueue(tileAtual);
        tileAtual.visited = true;
        
        while (process.Count > 0)
        {
            TileScript t = process.Dequeue();

            tilesSelecionaveis.Add(t);
            t.selecionavel = true;

            if(t.distance < _movimentos)
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
            alvo.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if(Vector3.Distance(transform.position, alvo) >= 0.05f)
            {
                bool jump = transform.position.y != alvo.y;

                if (jump)
                {
                    Jump(alvo);
                }
                else
                {
                    CalculateHeading(alvo);
                    SetHorizontalVelocity();
                }

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
                Debug.Log("transform.position: " + transform.position);

            }
            else
            {
                //Tile center reached
                transform.position = alvo;
                path.Pop();
                fallingDown = false;
                jumpingUp = false;
                movingEdge = false;
                jumpAlvo = Vector3.zero;
            }

        }
        else
        {
            //todo remove the selectable tiles
            RemoveSelectableTiles();
            movendo = false;
            TurnManager.EndTurn();

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

    void CalculateHeading(Vector3 alvo)
    {
        heading = alvo - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * _velMovimento;
    }

    void Jump(Vector3 alvo)
    {
        if (fallingDown)
        {
            FallDownward(alvo);
        }
        else if (jumpingUp)
        {
            JumpUpward(alvo);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(alvo);
        }

    }

    void PrepareJump(Vector3 alvo)
    {
        float alvoY = alvo.y;
        alvo.y = transform.position.y;

        CalculateHeading(alvo);

        if (transform.position.y > alvo.y)
        {
            fallingDown = false;
            jumpingUp = false; //se for falso, personagem dara um pequeno pulo.
            movingEdge = true;

            jumpAlvo = transform.position + (alvo - transform.position) / 2.0f;
            Debug.Log("transform.position" + transform.position);
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * _velMovimento/ 3.0f;

            float diferenca = alvoY - transform.position.y;

            Debug.Log("Vel Y: " + velocity.y);
            velocity.y = _velPulo * (0.5f + diferenca / 2.0f);
            Debug.Log("Vel Y: " + velocity.y);
        }

    }

    void FallDownward(Vector3 alvo)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= alvo.y)
        {
            fallingDown = false;    
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;
            p.y = alvo.y;
            transform.position = p;

            velocity = new Vector3();

        }
    }

    void JumpUpward(Vector3 alvo)
    {
        //TODO Pulo nao funciona corretamente
        velocity += Physics.gravity * Time.deltaTime;
       
        if (transform.position.y > alvo.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }

    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpAlvo) >= 0.05f)
        {
            SetHorizontalVelocity();

        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
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

        if (tempPath.Count <= _movimentos)
        {
            return t.parent;
        }

        TileScript endTile = null;
        for (int i = 0; i <= _movimentos; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(TileScript target)
    {
        ComputeAdjacencyLists(_alturaPulo, target);
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
        //if (this.GetComponent<CaracBase>().hp > 0)
        {
            this._acoes = 2;
        }
        //else
        {
            //this._acoes = 0;
        }
    }

    public void EndTurn()
    {
        _turn = false;
    }
}
