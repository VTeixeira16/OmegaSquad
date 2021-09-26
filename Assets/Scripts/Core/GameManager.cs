using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static SystemLanguage currentLanguage;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //Inucializacao de Textos
        currentLanguage = Application.systemLanguage;
        Texts.CheckLanguage();
        //currentLanguage = SystemLanguage.English;

    }

    public static void SetCurrentLanguage(SystemLanguage Language)
    {
        currentLanguage = Language;
    }
    
    public static SystemLanguage GetCurrentLanguage()
    {
        return currentLanguage;
    }
}
