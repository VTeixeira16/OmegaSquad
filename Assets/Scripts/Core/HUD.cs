using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text turnTeamNameTxt;
    [SerializeField] Image photoActualPlayer, photoActualWeapon;


    //Devera mudar para lista que contera players ativos
    [SerializeField] GameObject player1;

    [SerializeField] List<GameObject> MovementsQtd = new List<GameObject>();
    [SerializeField] List<GameObject> ActionsQtd = new List<GameObject>();


    int hudMovements, hudActions;


    void Start()
    {
    }
    void Update()
    {
        if (TurnManager.turnTeamName == "Player")
        {
            photoActualPlayer.gameObject.SetActive(true);
            photoActualWeapon.gameObject.SetActive(true);

            hudMovements = TurnManager.GetActualUnit().GetComponent<PlayerCharacters>().qtdMovimentos;
            hudActions = TurnManager.GetActualUnit().GetComponent<PlayerCharacters>().acoes;
        
            photoActualPlayer.sprite = TurnManager.GetActualUnit().GetComponent<PlayerCharacters>().photoPerson;

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
        }
        else
        {
            hudMovements = 0;
            hudActions = 0;

            photoActualPlayer.gameObject.SetActive(false);
            photoActualWeapon.gameObject.SetActive(false);

            for (int x = 0; x < MovementsQtd.Count; x++)
            {
                MovementsQtd[x].gameObject.SetActive(false);
            }

            for (int x = 0; x < ActionsQtd.Count; x++)
            {
                ActionsQtd[x].gameObject.SetActive(false);
            }
        }

        // turnTeamNameTxt.text = "Turn: " + TurnManager.turnTeamName;
    }
}
