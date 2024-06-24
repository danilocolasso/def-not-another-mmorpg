using UnityEngine;

public class CharacterData: MonoBehaviour
{
    [SerializeField] private new string name = "New Character";
    [SerializeField] private CharacterStatus status;

    public string Name => name;
    public CharacterStatus Status => status;
}
