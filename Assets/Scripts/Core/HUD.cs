using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text turnTeamNameTxt;



    //Devera mudar para lista que contera players ativos
    [SerializeField] GameObject player1;

    [SerializeField] List<GameObject> MovementsQtd = new List<GameObject>();
    [SerializeField] List<GameObject> ActionsQtd = new List<GameObject>();


    int hudMovements;
    int hudActions;


    void Start()
    {
    }
    void Update()
    {
        hudMovements = player1.GetComponent<PlayerMovement>().movimentos;
        hudActions = player1.GetComponent<PlayerCharacters>().acoes;

        for (int x = 0; x < MovementsQtd.Count; x++)
        {
            if(x < hudMovements)
                MovementsQtd[x].gameObject.SetActive(true);
            else
                MovementsQtd[x].gameObject.SetActive(false);
        }

        for (int x = 0; x < ActionsQtd.Count; x++)
        {
            if (x < hudActions)
                ActionsQtd[x].gameObject.SetActive(true);
            else
                ActionsQtd[x].gameObject.SetActive(false);
        }

        turnTeamNameTxt.text = "Turn: " + TurnManager.turnTeamName;
    }
}
