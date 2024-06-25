using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character/Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private new string name = "New Character";
    [SerializeField] private int level = 1;
    [SerializeField] private int experience = 0;
}
