using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text turnTeamNameTxt, textAmmo;
    [SerializeField] Image photoActualPlayer, photoActualWeapon, photoActualEnemy;


    [SerializeField] GameObject HUD_Player;

    [SerializeField] List<GameObject> MovementsQtd = new List<GameObject>();
    [SerializeField] List<GameObject> ActionsQtd = new List<GameObject>();


    WeaponScript actualWeapon;

    int hudMovements, hudActions;


    void Start()
    {
    }
    void Update()
    {
        turnTeamNameTxt.text = "Turn: " + TurnManager.turnTeamName;

        if (TurnManager.turnTeamName == "Player")
        {
            HUD_Player.SetActive(true);

            PlayerCharacters actualPlayer = TurnManager.GetActualUnit().GetComponent<PlayerCharacters>();
            BaseCharacters actualEnemy;
            if (TurnManager.GetActualTargetAttack() != null)
            {
                actualEnemy = TurnManager.GetActualTargetAttack().GetComponent<BaseCharacters>();
                photoActualEnemy.gameObject.SetActive(true);
                photoActualEnemy.sprite = actualEnemy.photoPerson;
            }
            else
            {
                actualEnemy = null;
                photoActualEnemy.gameObject.SetActive(false);
            }

            actualWeapon = actualPlayer.weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>();

            hudMovements = actualPlayer.qtdMovimentos;
            hudActions = actualPlayer.acoes;

            photoActualPlayer.sprite = actualPlayer.photoPerson;
            photoActualWeapon.sprite = actualWeapon.photoWeapon;
            textAmmo.text = actualWeapon.cartuchoQtd + "/" + actualWeapon.cartuchoMax;

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
            HUD_Player.SetActive(false);
        }
    }
}
