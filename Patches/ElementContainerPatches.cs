using System;
using ExperienceMultiplier.Extensions;
using HarmonyLib;
using UnityEngine;

namespace ExperienceMultiplier.Patches;

public class ElementContainerPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ElementContainer), nameof(ElementContainer.ModExp))]
    public static void ElementContainerModExp_Prefix(ref ElementContainer __instance, int ele, ref float a, bool chain)
    {
        var chara = __instance.Chara;
        var element = __instance.GetElement(ele);

        if (chara == null || !chara.isChara || chara.isDead || element == null) return;

        var __ai = Mathf.RoundToInt(a);
        a = ResultMultipliedValue(element, __ai, ExperienceMultiplierValueResolver, ResultValueResolver);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ElementContainer), nameof(ElementContainer.ModPotential))]
    public static void ElementContainerModPotential_Prefix(ref ElementContainer __instance, int ele, ref int v)
    {
        var chara = __instance.Chara;
        var element = __instance.GetElement(ele);

        if (chara == null || !chara.isChara || chara.isDead || element == null) return;

        v = ResultMultipliedValue(element, v, PotentialMultiplierValueResolver, ResultValueResolver);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ElementContainer), nameof(ElementContainer.ModTempPotential))]
    public static void ElementContainerModTempPotential_Prefix(ref ElementContainer __instance, int ele, ref int v, int threshMsg = 0)
    {
        var chara = __instance.Chara;
        var element = __instance.GetElement(ele);

        if (chara == null || !chara.isChara || chara.isDead || element == null) return;

        v = ResultMultipliedValue(element, v, PotentialMultiplierValueResolver, ResultValueResolver);
    }

    public static int PotentialLossValue(Element element)
    {
        var value = element.vTempPotential / 4 + EClass.rnd(5) + 5;
        return ResultMultipliedValue(element, value, PotentialLossMultiplierValueResolver, ResultValueResolver);
    }

    private static int ResultMultipliedValue(Element element, int value, Func<Element, int, int> multiplierResolver, Func<Chara, int, int, int> resultValueResolver)
    {
        var chara = element.owner?.Chara;
        if (chara == null || !chara.isChara || chara.isDead) return value;

        var multipliedValue = multiplierResolver(element, value);
        var resultValue = resultValueResolver(chara, value, multipliedValue);

        return resultValue;
    }

    private static int ResultValueResolver(Chara chara, int value, int multipliedValue)
    {
        var resultValue = value;

        if (PluginSettings.MainCharacter.Value)
        {
            if (chara.IsPC)
                resultValue = multipliedValue;
        }

        if (PluginSettings.Faction.Value)
        {
            if (chara.IsPCFaction && !chara.IsPC && !chara.IsPCParty)
                resultValue = multipliedValue;
        }

        if (PluginSettings.Party.Value)
        {
            if (chara.IsPCParty && !chara.IsPC)
                resultValue = multipliedValue;
        }

        if (PluginSettings.PlayerMinion.Value)
        {
            if (chara.IsPCMinion())
                resultValue = multipliedValue;
        }

        if (PluginSettings.FactionMinion.Value)
        {
            if (chara.IsPCFactionMinion && !chara.IsPCMinion() && !chara.IsPCPartyMinion)
                resultValue = multipliedValue;
        }

        if (PluginSettings.PartyMinion.Value)
        {
            if (chara.IsPCPartyMinion && !chara.IsPCMinion())
                resultValue = multipliedValue;
        }

        return resultValue;
    }

    private static int ExperienceMultiplierValueResolver(Element element, int value)
    {
        var multiplier = 1.0f;

        if (element.IsMainAttribute())
            multiplier = PluginSettings.ExperienceMultiplierMainAttributes.Value;

        if (element.IsSkillWeapon())
            multiplier = PluginSettings.ExperienceMultiplierSkillWeapon.Value;

        if (element.IsSkillCombat())
            multiplier = PluginSettings.ExperienceMultiplierSkillCombat.Value;

        if (element.IsSkillCraft())
            multiplier = PluginSettings.ExperienceMultiplierSkillCraft.Value;

        if (element.IsSkillGeneral())
            multiplier = PluginSettings.ExperienceMultiplierSkillGeneral.Value;

        if (element.IsSkillMind())
            multiplier = PluginSettings.ExperienceMultiplierSkillMind.Value;

        if (element.IsSkillStealth())
            multiplier = PluginSettings.ExperienceMultiplierSkillStealth.Value;

        if (element.IsSkillLabor())
            multiplier = PluginSettings.ExperienceMultiplierSkillLabor.Value;

        if (element.IsSpellAbility())
            multiplier = PluginSettings.ExperienceMultiplierSpellAbility.Value;

        return Mathf.RoundToInt(value * multiplier);
    }

    private static int PotentialMultiplierValueResolver(Element element, int value)
    {
        var multiplier = 1.0f;

        if (element.IsMainAttribute())
            multiplier = PluginSettings.PotentialMultiplierMainAttributes.Value;

        if (element.IsSkillWeapon())
            multiplier = PluginSettings.PotentialMultiplierSkillWeapon.Value;

        if (element.IsSkillCombat())
            multiplier = PluginSettings.PotentialMultiplierSkillCombat.Value;

        if (element.IsSkillCraft())
            multiplier = PluginSettings.PotentialMultiplierSkillCraft.Value;

        if (element.IsSkillGeneral())
            multiplier = PluginSettings.PotentialMultiplierSkillGeneral.Value;

        if (element.IsSkillMind())
            multiplier = PluginSettings.PotentialMultiplierSkillMind.Value;

        if (element.IsSkillStealth())
            multiplier = PluginSettings.PotentialMultiplierSkillStealth.Value;

        if (element.IsSkillLabor())
            multiplier = PluginSettings.PotentialMultiplierSkillLabor.Value;

        if (element.IsSpellAbility())
            multiplier = PluginSettings.PotentialMultiplierSpellAbility.Value;

        return Mathf.RoundToInt(value * multiplier);
    }

    private static int PotentialLossMultiplierValueResolver(Element element, int value)
    {
        var multiplier = 1.0f;

        if (element.IsMainAttribute())
            multiplier = PluginSettings.PotentialLossMultiplierMainAttributes.Value;

        if (element.IsSkillWeapon())
            multiplier = PluginSettings.PotentialLossMultiplierSkillWeapon.Value;

        if (element.IsSkillCombat())
            multiplier = PluginSettings.PotentialLossMultiplierSkillCombat.Value;

        if (element.IsSkillCraft())
            multiplier = PluginSettings.PotentialLossMultiplierSkillCraft.Value;

        if (element.IsSkillGeneral())
            multiplier = PluginSettings.PotentialLossMultiplierSkillGeneral.Value;

        if (element.IsSkillMind())
            multiplier = PluginSettings.PotentialLossMultiplierSkillMind.Value;

        if (element.IsSkillStealth())
            multiplier = PluginSettings.PotentialLossMultiplierSkillStealth.Value;

        if (element.IsSkillLabor())
            multiplier = PluginSettings.PotentialLossMultiplierSkillLabor.Value;

        if (element.IsSpellAbility())
            multiplier = PluginSettings.PotentialLossMultiplierSpellAbility.Value;

        return Mathf.RoundToInt(value * multiplier);
    }
}
