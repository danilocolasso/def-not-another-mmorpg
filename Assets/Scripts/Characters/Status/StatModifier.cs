public class StatModifier
{
    public enum ModifierType { Flat, Percent };

    public readonly object Source;
    public readonly float Value;
    public readonly ModifierType Type;

    public StatModifier(object source, float value, ModifierType type = ModifierType.Flat)
    {
        Source = source;
        Value = value;
        Type = type;
    }
}
