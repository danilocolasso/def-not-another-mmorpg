[System.Serializable]
public class IntStat : FloatStat
{
    public new int Value => (int)base.Value;

    public IntStat(int value) : base(value) { }

    public void SetValue(int value)
    {
        base.SetValue(value);
    }

    public void Increment(int value = 1)
    {
        base.Increment(value);
    }

    public void Decrement(int value = 1)
    {
        base.Decrement(value);
    }
}
