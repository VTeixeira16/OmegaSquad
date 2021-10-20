using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript
{
    static bool confirmacaoAtaque = false;
    static int chanceAcerto = 0;


    public static bool GetConfirmacaoAtaque()
    {
        return confirmacaoAtaque;
    }
    public static void SetConfirmacaoAtaque(bool value)
    {
        confirmacaoAtaque = value;
    }
    public static int GetChanceAcerto()
    {
        return chanceAcerto;
    }

    public static void NPCAttack(GameObject Atacante, GameObject Defensor)
    {
        SetConfirmacaoAtaque(true);
        Atacar(Atacante, Defensor);
    }
    
    public static bool Atacar(GameObject Atacante, GameObject Defensor)
    {
        if(Atacante.GetComponent<BaseCharacters>().acoes <= 0)
            return false;

        TurnManager.SetActualTargetAttack(Defensor);
        
        int _precisaoArma, _precisaoUnidade, _alcanceArma, _danoArma;
        float _distanciaAlvo;

        if (Atacante.tag == "Player")
            _precisaoArma = Atacante.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>().precisaoArma;
        else
            _precisaoArma = 15;

        _precisaoUnidade = Atacante.GetComponent<BaseCharacters>().precisaoUnidade;
        _alcanceArma = Atacante.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>().alcanceArma;
        _danoArma = Atacante.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>().danoArma;

        _distanciaAlvo = Vector3.Distance(Atacante.transform.position, Defensor.transform.position);

        CalculaChanceAcerto(_precisaoArma, _precisaoUnidade, _alcanceArma, _distanciaAlvo);

        if (!confirmacaoAtaque)
        {
            // Debug.Log("confirmacao falsa");
            confirmacaoAtaque = true;
            return false;
        }

        if (CalculaDano())
            Defensor.GetComponent<BaseCharacters>().hp -= _danoArma;

        Atacante.GetComponent<BaseCharacters>().acoes--;
        Atacante.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>().cartuchoQtd--;
        TurnManager.SetActualTargetAttack(null);
        confirmacaoAtaque = false;
        chanceAcerto = 0;

        return true;
    }

    static void CalculaChanceAcerto(int PrecisaoArma, int PrecisaoUnidade, int AlcanceArma, float DistanciaAlvo)
    {
        // Debug.Log("PrecisaoArma: " + PrecisaoArma);
        // Debug.Log("PrecisaoUnidade: " + PrecisaoUnidade);
        // Debug.Log("AlcanceArma: " + AlcanceArma);
        // Debug.Log("DistanciaAlvo: " + DistanciaAlvo);

        // int precisaoTotal = (PrecisaoArma + PrecisaoUnidade) * 5;
        // int proximidadeAlvo = (AlcanceArma / (int)DistanciaAlvo) * 7;

        int precisaoTotal = (PrecisaoArma + PrecisaoUnidade) * 3;
        int proximidadeAlvo = (AlcanceArma / (int)DistanciaAlvo) * 3;

        chanceAcerto = precisaoTotal + proximidadeAlvo;
        if (chanceAcerto > 100)
            chanceAcerto = 100;

        // Debug.Log("chanceAcerto: " + chanceAcerto);

    }

    static bool CalculaDano()
    {
        int verificaAcerto = Random.Range(0, 100);
        Debug.Log("Verifica Acerto: " + verificaAcerto + " / " + chanceAcerto);

        if (verificaAcerto < chanceAcerto)
            return true;
        else
            return false;
        //TODO - Fazer calculo de dano

    }
}
