// Author(s): Paul Calande
// Functions for translating based on common translation keys.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilTranslate
{
    private static Translator GetTranslator()
    {
        return ServiceLocator.GetTranslator();
    }

    public static string ItemName(string itemIdentifier)
    {
        return GetTranslator().Translate("Item", itemIdentifier, "Name");
    }

    public static string ItemDescription(string itemIdentifier)
    {
        return GetTranslator().Translate("Item", itemIdentifier, "Description");
    }

    public static string BossName(string bossIdentifier)
    {
        return GetTranslator().Translate("Boss", bossIdentifier, "Name");
    }
}