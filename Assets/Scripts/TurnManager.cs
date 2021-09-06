using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticMovement>> units = new Dictionary<string, List<TacticMovement>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticMovement> _turnTeam = new Queue<TacticMovement>();


    // TODO - NECESSARIO IMPLEMENTAR - VARIAVEIS E METODOS ZUMBIS
    static int activeZombies; // Armazena todos os zumbis "vivos"
    static int movingZombies; // Armazena zumbis que ainda estao se movimentando
    static string _turnTeamName;

    
    public static void AddZombie()
    {
        activeZombies++;
    }
    public static void RemoveZombie()
    {
        if (activeZombies < 0)
            activeZombies--;
    }

    public static void IncreaseMovingZombies()
    {
        movingZombies++;
    }
    public static void SubtractMovingZombies()
    {
        if (movingZombies < 0)
            movingZombies--;
    }

    public static int GetActiveZombies()
    {
        return activeZombies;
    }
    // 

    


    public Queue<TacticMovement> turnTeam
    {
        get { return _turnTeam; }
    }

    public static string turnTeamName
    {
        get { return _turnTeamName; }
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

        Debug.Log("teamList: " + teamList.Count);


        foreach (TacticMovement unit in teamList)
        {
            _turnTeam.Enqueue(unit);
            _turnTeamName = unit.tag;
            // Debug.Log("Turno Atual: " + unit.tag);
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

        if (unit.tag == "Zombie")
            AddZombie();

        list.Add(unit);

    }

    //TODO - Criar funcao removeUnit para quando unidade morrer
}
