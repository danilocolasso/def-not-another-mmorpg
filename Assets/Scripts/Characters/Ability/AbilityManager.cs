using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities = new List<Ability>();
    private Dictionary<string, Ability> abilityDictionary = new Dictionary<string, Ability>();

    private void Awake()
    {
        foreach (Ability ability in abilities)
        {
            if (!abilityDictionary.ContainsKey(ability.name))
            {
                abilityDictionary.Add(ability.name, ability);
                Debug.Log($"Added {ability.name} to Ability Dictionary");
            }
        }
    }

    public void UseAbility(string abilityName, Character caster, Character target)
    {
        if (abilityDictionary.TryGetValue(abilityName, out Ability ability))
        {
            ability.Activate(caster, target);
            Debug.Log($"{caster.name} used {abilityName} on {target.name}");
            return;
        }

        Debug.LogError($"{caster.name} does not have \"{abilityName}\" ability!");
    }
}
