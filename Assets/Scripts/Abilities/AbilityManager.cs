using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities = new();
    private Dictionary<string, Ability> abilityList = new();
    private Dictionary<Ability, Coroutine> cooldownList = new();

    private void Awake()
    {
        foreach (Ability ability in abilities)
        {
            Debug.Log($"Adding {ability} to Ability Dictionary");
            AddAbility(ability);
        }
    }

    public void UseAbility(string abilityName, Character caster, Character target)
    {
        if (!abilityList.TryGetValue(abilityName, out Ability ability))
        {
            Debug.Log($"{caster.name} does not have \"{abilityName}\" ability!");
            return;
        }

        if (IsAbilityOnCooldown(ability))
        {
            Debug.Log($"{ability.name} is on cooldown!");
            return;
        }

        if (!target.IsInRange(caster, ability.range))
        {
            Debug.Log($"{target.name} is out of range!");
            return;
        }

        if (!ability.CanUse(caster, target))
        {
            Debug.Log($"{caster.name} cannot use {abilityName} on {target.name}!");
            return;
        }

        ability.Use(caster, target);
        StartCooldown(ability);
        Debug.Log($"{caster.name} used {abilityName} on {target.name}");
    }

    private void AddAbility(Ability ability)
    {
        if (!HasAbility(ability.name))
        {
            abilityList.Add(ability.name, ability);
            Debug.Log($"Added {ability.name} to Ability Dictionary");
        }
    }

    private bool HasAbility(string abilityName)
    {
        return abilityList.ContainsKey(abilityName);
    }

    private bool IsAbilityOnCooldown(Ability ability)
    {
        return cooldownList.ContainsKey(ability);
    }

    private void StartCooldown(Ability ability)
    {
        if (cooldownList.TryGetValue(ability, out Coroutine cooldown))
        {
            StopCoroutine(cooldown);
        }

        cooldownList[ability] = StartCoroutine(CooldownCoroutine(ability));
    }

    private void StopCooldown(Ability ability)
    {
        if (cooldownList.TryGetValue(ability, out Coroutine cooldown))
        {
            StopCoroutine(cooldown);
        }

        cooldownList.Remove(ability);
    }

    private IEnumerator CooldownCoroutine(Ability ability)
    {
        yield return new WaitForSeconds(ability.cooldown);
        Debug.Log($"{ability.name} is off cooldown!");
        StopCooldown(ability);
    }
}