using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacters : MonoBehaviour
{
    [Header("Características Base")]
    [SerializeField] protected string _nome;
    [SerializeField] protected uint _hp, _precisaoUnidade, _defesa, _visao, _acoes, _qtdMovimentos;

    // TODO - Implementar sistema de proteção
    // Protecao: 0 nenhuma / 1 parcial e 2 completa.
    protected int _protecao;
    protected bool _vivo;

    public string nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public uint precisaoUnidade
    {
        get { return _precisaoUnidade; }
        set { _precisaoUnidade = value; }
    }
    public uint defesa
    {
        get { return _defesa; }
        set { _defesa = value; }
    }
    public uint acoes
    {
        get { return _acoes; }
        set { _acoes = value; }
    }
    public uint qtdMovimentos
    {
        get { return _qtdMovimentos; }
        set { _qtdMovimentos = value; }
    }
    public uint visao
    {
        get { return _visao; }
        set { _visao = value; }
    }
    public uint hp
    {
        get { return _hp; }
        set
        {
            if (_vivo)
            {
                _hp = _hp + value;
            }
            if (_hp <= 0)
            {
                _vivo = false;
                _hp = 0;
            }
        }
    }

    public bool vivo
    {
        get { return _vivo; }
        set { _vivo = value; }
    }
    public int protecao
    {
        get { return _protecao; }
        set { _protecao = value; }
    }

    protected void Start()
    {
        _vivo = true;
    }

    // TODO - Variavel devera ser removida justamente por ser duplicidade da negacao de vivo
    protected void Update()
    {

        if (this._hp <= 0)
        {
            this._hp = 0;
            this._vivo = false;
        }
    }
}
