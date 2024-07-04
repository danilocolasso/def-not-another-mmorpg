using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities = new List<Ability>();
    private Dictionary<string, Ability> abilityDictionary = new Dictionary<string, Ability>();
    private Dictionary<Ability, Coroutine> cooldownDictionary = new Dictionary<Ability, Coroutine>();

    private void Awake()
    {
        foreach (Ability ability in abilities)
        {
            AddAbility(ability);
        }
    }

    public void UseAbility(string abilityName, Character caster, Character target)
    {
        if (!abilityDictionary.TryGetValue(abilityName, out Ability ability))
        {
            Debug.Log($"{caster.name} does not have \"{abilityName}\" ability!");
            return;
        }

        if (IsAbilityOnCooldown(ability))
        {
            Debug.Log($"{abilityName} is on cooldown!");
            return;
        }

        if (!IsTargetInRange(caster, target, ability))
        {
            Debug.Log($"{target.name} is out of range!");
            return;
        }

        ability.Activate(caster, target);
        StartCooldown(ability);

        Debug.Log($"{caster.name} used {abilityName} on {target.name}");
    }

    private void AddAbility(Ability ability)
    {
        if (!HasAbility(ability.name))
        {
            abilityDictionary.Add(ability.name, ability);
            Debug.Log($"Added {ability.name} to Ability Dictionary");
        }
    }

    private bool HasAbility(string abilityName)
    {
        return abilityDictionary.ContainsKey(abilityName);
    }

    private void StartCooldown(Ability ability)
    {
        if (cooldownDictionary.TryGetValue(ability, out Coroutine cooldown))
        {
            StopCoroutine(cooldown);
        }

        cooldownDictionary[ability] = StartCoroutine(Cooldown(ability));
    }

    private void StopCooldown(Ability ability)
    {
        if (cooldownDictionary.TryGetValue(ability, out Coroutine cooldown))
        {
            StopCoroutine(cooldown);
        }

        cooldownDictionary.Remove(ability);
    }

    private bool IsAbilityOnCooldown(Ability ability)
    {
        return cooldownDictionary.ContainsKey(ability);
    }

    private bool IsTargetInRange(Character caster, Character target, Ability ability)
    {
        return Vector3.Distance(caster.transform.position, target.transform.position) <= ability.range;
    }

    private IEnumerator Cooldown(Ability ability)
    {
        yield return new WaitForSeconds(ability.cooldown);
        Debug.Log($"{ability.name} is off cooldown!");
        StopCooldown(ability);
    }
}