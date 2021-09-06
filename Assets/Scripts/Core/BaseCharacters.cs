using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacters : MonoBehaviour
{
    [Header("Caracter�sticas Base")]
    [SerializeField] protected string _nome;
    [SerializeField] protected int _hp, _precisaoUnidade, _defesa, _visao, _acoes;

    // TODO - Implementar sistema de prote��o
    // Protecao: 0 nenhuma / 1 parcial e 2 completa.
    protected int _protecao;
    protected bool _vivo;

    public string nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public int precisaoUnidade
    {
        get { return _precisaoUnidade; }
        set { _precisaoUnidade = value; }
    }
    public int defesa
    {
        get { return _defesa; }
        set { _defesa = value; }
    }
    public int acoes
    {
        get { return _acoes; }
        set { _acoes = value; }
    }
    public int visao
    {
        get { return _visao; }
        set { _visao = value; }
    }
    public int hp
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