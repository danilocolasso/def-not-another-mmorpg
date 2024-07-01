using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AggroManager : MonoBehaviour
{
    private const float BASE_AGGRO = 20f;
    private const float MIN_AGGRO = 0f;
    private const float MAX_AGGRO = 100f;
    private const float AGGRO_UPDATE_INTERVAL = 1f;

    private Enemy character;
    private CircleCollider2D detectionArea;
    private readonly Dictionary<Character, float> aggroValues = new Dictionary<Character, float>();
    private readonly Dictionary<Character, Coroutine> aggroCoroutines = new Dictionary<Character, Coroutine>();

    public void Enable()
    {
        if (character.IsDead) return;

        detectionArea.enabled = true;
    }

    public void Disable()
    {
        detectionArea.enabled = false;

        StopAllCoroutines();
        aggroValues.Clear();
        aggroCoroutines.Clear();
    }

    protected void Awake()
    {
        character = GetComponent<Enemy>();
        detectionArea = GetComponent<CircleCollider2D>();
    }

    protected void Start()
    {
        SetDetectionRange(character.Status.Range);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Character target = other.GetComponent<Character>();

        if (target.IsAlive)
        {
            if (!aggroValues.ContainsKey(target))
            {
                aggroValues.Add(target, 0);
                aggroCoroutines.Add(target, StartCoroutine(IncrementCoroutine(target)));
                return;
            }

            StopCoroutine(aggroCoroutines[target]);
            aggroCoroutines[target] = StartCoroutine(IncrementCoroutine(target));
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        Character target = other.GetComponent<Character>();

        StopCoroutine(aggroCoroutines[target]);
        aggroCoroutines[target] = StartCoroutine(DecrementCoroutine(target));
    }

    private void Remove(Character target)
    {
        StopCoroutine(aggroCoroutines[target]);
        aggroCoroutines.Remove(target);
        aggroValues.Remove(target);
    }

    private void Detect(Character target)
    {
        StopAllCoroutines();

        ICommand command = new DetectCharacterCommand(character, target);
        command.Execute();
    }

    private void SetDetectionRange(float range)
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = range;
    }

    private void Increment(Character target)
    {
        int levelDifference = target.Data.Level - character.Data.Level;
        float multiplier = levelDifference > 0 ? (1 - levelDifference * 0.1f) : (1 + levelDifference * 0.1f);
        float aggro = levelDifference == 0 ? BASE_AGGRO : BASE_AGGRO * multiplier;

        aggroValues[target] = Mathf.Min(aggroValues[target] + aggro, MAX_AGGRO);
    }

    private void Decrement(Character target)
    {
        int levelDifference = target.Data.Level - character.Data.Level;
        float multiplier = levelDifference > 0 ? (1 + levelDifference * 0.1f) : (1 - Mathf.Abs(levelDifference) * 0.1f);
        float aggro = levelDifference == 0 ? BASE_AGGRO : BASE_AGGRO * multiplier;

        aggroValues[target] = Mathf.Max(aggroValues[target] - aggro, MIN_AGGRO);
    }

    private IEnumerator IncrementCoroutine(Character target)
    {
        while (aggroValues[target] < MAX_AGGRO)
        {
            Increment(target);
            yield return new WaitForSeconds(AGGRO_UPDATE_INTERVAL);
        }

        Detect(target);
    }

    private IEnumerator DecrementCoroutine(Character target)
    {
        while (aggroValues[target] > MIN_AGGRO)
        {
            Decrement(target);
            yield return new WaitForSeconds(AGGRO_UPDATE_INTERVAL);
        }

        Remove(target);
    }
}
