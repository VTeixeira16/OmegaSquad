using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texts
{
    //TODO - Migrar para um Json
    string HUD_ChoiceHit;

    static IDictionary<string, string> TextGamePT = new Dictionary<string, string>()
    {
        {"HUD_ChoiceHit", "Chance de Acerto:"},
        {"HUD_HP", "Vida:"}

    };
    static IDictionary<string, string> TextGameEN = new Dictionary<string, string>()
    {
        {"HUD_ChoiceHit", "Choice Hit:"},
        {"HUD_HP", "Hit Points:"}

    };

    static IDictionary<string, string> ActiveDictionary = new Dictionary<string, string>();

    public static void CheckLanguage()
    {
        // Debug.Log("Idioma Atual: " + GameManager.GetCurrentLanguage());

        switch (GameManager.GetCurrentLanguage())
        {
            case SystemLanguage.Portuguese:
                ActiveDictionary = TextGamePT;
                break;
            case SystemLanguage.English:
                ActiveDictionary = TextGameEN;
                break;

            default:
                break;
        }
    }

    public static string GetText(string text)
    {
        ActiveDictionary.TryGetValue(text, out string v);
        return v;

    }


}
