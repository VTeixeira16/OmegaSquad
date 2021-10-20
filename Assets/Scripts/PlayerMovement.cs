using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : TacticMovement
{
    WeaponController weaponCtl;

    void Start()
    {
        Init();
        weaponCtl = this.GetComponent<PlayerCharacters>().weaponContainer.GetComponent<WeaponController>();
    }

    new void Update()
    {
        base.Update();

        if (!turn)
        {
            return;
        }

        //TODO - Verificacao deve ocorrer uma unica vez e no inicio do turno
        if (!_movendo)
        {
            if (!calculouTiles)
            {
                FindSelectableTiles();
                calculouTiles = true;
            }
            CheckMouse();
            CheckInput();
        }
        else
        {
            if(baseCharacters.qtdMovimentos > 0)
            {
                calculouTiles = false;
                Move();
            }

        }
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile" && baseCharacters.qtdMovimentos > 0)
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();
                    if (t.selecionavel)
                    {
                        MoveToTile(t);
                    }
                }

                if (hit.collider.tag == "NPC" || hit.collider.tag == "Zombie")
                {
                    _atacando = AttackScript.Atacar(this.gameObject, hit.collider.gameObject);
                }
            }
        }
    }
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponCtl.activeWeaponNumber = 0;
            _changeWeapon = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponCtl.activeWeaponNumber = 1;
            _changeWeapon = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (baseCharacters.acoes > 0 && weaponCtl.activeWeapon.GetComponent<WeaponScript>().Recarregar())
            {
                baseCharacters.acoes--;
                _recarregando = true;
            }
        }

        // TODO - Implementacao nao esta funcionando corretamente.
        if (Input.GetKeyDown(KeyCode.End))
        {
            baseCharacters.acoes = 0;
            baseCharacters.qtdMovimentos = 0;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            if(AttackScript.GetConfirmacaoAtaque() && TurnManager.GetActualTargetAttack() != null)
            {
                AttackScript.Atacar(this.gameObject, TurnManager.GetActualTargetAttack());
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (AttackScript.GetConfirmacaoAtaque() && TurnManager.GetActualTargetAttack() != null)
            {
                AttackScript.SetConfirmacaoAtaque(false);
                TurnManager.SetActualTargetAttack(null);
            }
        }
    }
}
