using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript
{
    static bool confirmacaoAtaque;


    static bool GetConfirmacaoAtaque()
    {
        return confirmacaoAtaque;
    }
    static void SetConfirmacaoAtaque(bool value)
    {
        confirmacaoAtaque = value;
    }

    public static void NPCAttack(GameObject Atacante, GameObject Defensor)
    {
        SetConfirmacaoAtaque(true);
        Atacar(Atacante, Defensor);
    }
    
    public static void Atacar(GameObject Atacante, GameObject Defensor)
    {
        TurnManager.SetActualTargetAttack(Defensor);
        if (!confirmacaoAtaque)
        {
            // Debug.Log("confirmacao falsa");
            confirmacaoAtaque = true;
            return;
        }

        if(Atacante.GetComponent<BaseCharacters>().acoes <= 0)
            return;

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

        if (calculaDano(_precisaoArma, _precisaoUnidade, _alcanceArma, _distanciaAlvo))
            Defensor.GetComponent<BaseCharacters>().hp -= _danoArma;

        Atacante.GetComponent<BaseCharacters>().acoes--;
        Atacante.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>().cartuchoQtd--;
        Debug.Log("_cartuchoQtd" + Atacante.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>().activeWeapon.GetComponent<WeaponScript>().cartuchoQtd);


        Debug.Log("_danoArma: " + _danoArma);
        Debug.Log("Atacante: " + Atacante.name);
        Debug.Log("Defensor: " + Defensor.name);
        confirmacaoAtaque = false;


    }

    static bool calculaDano(int PrecisaoArma, int PrecisaoUnidade, int AlcanceArma, float DistanciaAlvo)
    {
        //TODO - Fazer calculo de dano

        return true;
    }
}
