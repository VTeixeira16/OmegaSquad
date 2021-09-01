using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticMovement>> units = new Dictionary<string, List<TacticMovement>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticMovement> _turnTeam = new Queue<TacticMovement>();

    public Queue<TacticMovement> turnTeam
    {
        get { return _turnTeam; }
    }

    void Start()
    {

    }

    void Update()
    {
        if (_turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        List<TacticMovement> teamList = units[turnKey.Peek()];

        foreach (TacticMovement unit in teamList)
        {
            _turnTeam.Enqueue(unit);
        }

        StartTurn();

    }

    public static void StartTurn()
    {
        if (_turnTeam.Count > 0)
        {
            _turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TacticMovement unit = _turnTeam.Dequeue();
        unit.EndTurn();

        if (_turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(TacticMovement unit)
    {
        List<TacticMovement> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticMovement>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);


    }
}
