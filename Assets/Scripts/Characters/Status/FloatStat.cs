using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatStat
{
    private readonly float baseValue;
    [SerializeField] private float value;
    private Dictionary<object, StatModifier> modifiers;

    public float Value => value;

    public FloatStat(float value)
    {
        baseValue = value;
        this.value = value;
        modifiers = new Dictionary<object, StatModifier>();
    }

    public void SetValue(float value)
    {
        this.value = value;
    }

    public void Increment(float value = 1f)
    {
        this.value += value;
    }

    public void Decrement(float value = 1f)
    {
        this.value -= value;
    }

    public void AddModifier(StatModifier modifier)
    {
        if (modifiers.ContainsKey(modifier.Source))
        {
            modifiers[modifier.Source] = modifier;
        }
        else
        {
            modifiers.Add(modifier.Source, modifier);
        }

        CalculateModifiers();
    }

    public void RemoveModifier(object source)
    {
        modifiers.Remove(source);
        CalculateModifiers();
    }

    private void CalculateModifiers()
    {
        float value = baseValue;
        float flat = 0f;
        float percent = 0f;

        foreach (var modifier in modifiers.Values)
        {
            switch (modifier.Type)
            {
                case StatModifier.ModifierType.Flat:
                    flat += modifier.Value;
                    break;
                case StatModifier.ModifierType.Percent:
                    percent += modifier.Value;
                    break;
            }
        }

        SetValue(value + flat + (value * percent));
    }
}
