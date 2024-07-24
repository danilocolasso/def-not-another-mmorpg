using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroManager : MonoBehaviour
{
    private const float BASE_AGGRO = 20f;
    private const float MIN_AGGRO = 0f;
    private const float MAX_AGGRO = 100f;
    private const float AGGRO_UPDATE_INTERVAL = 0.2f;

    private Enemy character;
    [SerializeField] private Collider2DTrigger detectionArea;
    private readonly Dictionary<Character, float> aggroValues = new();
    private readonly Dictionary<Character, Coroutine> aggroCoroutines = new();

    protected void Awake()
    {
        character = GetComponent<Enemy>();
    }

    protected void Start()
    {
        if (detectionArea == null)
        {
            detectionArea = GetComponentInChildren<Collider2DTrigger>();
            Debug.LogWarning($"Performance --> Assign {name} Detection Area to AggroManager in the Inspector!");
        }

        detectionArea.SetRadius(character.Status.Range);
    }

    public void Enable()
    {
        if (character.IsDead) return;

        detectionArea.Enable();
    }

    public void Disable()
    {
        StopAllCoroutines();
    
        detectionArea.Disable();
        aggroValues.Clear();
        aggroCoroutines.Clear();
    }

    public void StartAggro(Collider2D other)
    {
        Character target = other.GetComponentInParent<Character>();

        if (target.IsDead) return;

        if (!aggroValues.ContainsKey(target))
        {
            aggroValues.Add(target, 0);
            aggroCoroutines.Add(target, StartCoroutine(IncrementCoroutine(target)));
            return;
        }

        StopCoroutine(aggroCoroutines[target]);
        aggroCoroutines[target] = StartCoroutine(IncrementCoroutine(target));
    }

    public void StopAggro(Collider2D other)
    {
        Character target = other.GetComponentInParent<Character>();

        StopCoroutine(aggroCoroutines[target]);
        aggroCoroutines[target] = StartCoroutine(DecrementCoroutine(target));
    }

    private void Undetect(Character target)
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

    private IEnumerator IncrementCoroutine(Character target)
    {
        while (aggroValues[target] < MAX_AGGRO)
        {
            yield return new WaitForSeconds(AGGRO_UPDATE_INTERVAL);

            int levelDifference = target.Data.Level - character.Data.Level;
            float multiplier = levelDifference > 0 ? (1 - levelDifference * 0.1f) : (1 + levelDifference * 0.1f);
            float aggro = levelDifference == 0 ? BASE_AGGRO : BASE_AGGRO * multiplier;

            aggroValues[target] = Mathf.Min(aggroValues[target] + aggro, MAX_AGGRO);
            Debug.Log($"{target} aggro value: {aggroValues[target]}");
        }

        Debug.Log($"{target} is detected by {character}");
        Disable();
        Detect(target);
    }

    private IEnumerator DecrementCoroutine(Character target)
    {
        while (aggroValues[target] > MIN_AGGRO)
        {
            yield return new WaitForSeconds(AGGRO_UPDATE_INTERVAL);
            
            int levelDifference = target.Data.Level - character.Data.Level;
            float multiplier = levelDifference > 0 ? (1 + levelDifference * 0.1f) : (1 - Mathf.Abs(levelDifference) * 0.1f);
            float aggro = levelDifference == 0 ? BASE_AGGRO : BASE_AGGRO * multiplier;

            aggroValues[target] = Mathf.Max(aggroValues[target] - aggro, MIN_AGGRO);
            Debug.Log($"{target} aggro value: {aggroValues[target]}");
        }

        Debug.Log($"{target} is no longer detected by {character}");
        Undetect(target);
    }
}
