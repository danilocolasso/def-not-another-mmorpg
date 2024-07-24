using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities = new();

    private readonly Dictionary<string, Ability> abilityList = new();
    private readonly Dictionary<Ability, Coroutine> cooldownList = new();
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
        AddAbilities(abilities);
    }

    public bool UseAbility(string abilityName, Character target)
    {
        if (!abilityList.TryGetValue(abilityName, out Ability ability))
        {
            Debug.Log($"{character.name} does not have \"{abilityName}\" ability!");
            return false;
        }

        if (IsAbilityOnCooldown(ability))
        {
            Debug.Log($"{ability.name} is on cooldown!");
            return false;
        }

        if (!character.IsInRange(target, ability.range))
        {
            Debug.Log($"{target.name} is out of range!");
            return false;
        }

        if (!ability.CanUse(character, target))
        {
            Debug.Log($"{character.name} cannot use {abilityName} on {target.name}!");
            return false;
        }

        ability.Use(character, target);
        StartCooldown(ability);
        Debug.Log($"{character.name} used {abilityName} on {target.name}");
        
        return true;
    }

    private void AddAbility(Ability ability)
    {
        if (!HasAbility(ability.name))
        {
            abilityList.Add(ability.name, ability);
        }
    }

    private void AddAbilities(List<Ability> abilities)
    {
        foreach (Ability ability in abilities)
        {
            AddAbility(ability);
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