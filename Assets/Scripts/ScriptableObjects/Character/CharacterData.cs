using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Scriptable Objects/Character/Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private new string name = "New Character";
    [SerializeField] private int level = 1;
    [SerializeField] private int experience = 0;

    public string Name => name;
    public int Level => level;
    public int Experience => experience;
}
