using UnityEngine;

[System.Serializable]
public struct Status
{
    private float min;
    private float max;
    private float current;

    public readonly float Min => min;
    public readonly float Max => max;
    public readonly float Current => current;

    public Status(float min, float max, float current)
    {
        this.min = min;
        this.max = max;
        this.current = Mathf.Clamp(current, min, max);
    }

    public Status(float current)
    {
        min = 0;
        max = 0;
        this.current = Mathf.Max(0, current);
    }

    public Status(float min, float current)
    {
        this.min = min;
        max = 0;
        this.current = Mathf.Clamp(current, min, max);
    }

    public void SetMin(float value)
    {
        min = value;
    }

    public void SetMax(float value)
    {
        max = value;
    }

    public void SetCurrent(float value)
    {
        current = Mathf.Clamp(value, min, max);
    }

    public void Increment(float value = 1f)
    {
        SetCurrent(current + value);
    }

    public void Decrement(float value = 1f)
    {
        SetCurrent(current - value);
    }
}
