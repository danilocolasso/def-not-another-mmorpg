using UnityEngine;

[CreateAssetMenu(fileName = "NewHumanoidCharacterGraphics", menuName = "Scriptable Objects/Character/Graphics/Humanoid")]
public class HumanoidCharacterGraphics : ScriptableObject
{
    [Header("Body")]
    [SerializeField] private Color32 skinColor = Color.white;
    [SerializeField] private Sprite hair;
    [SerializeField] private Sprite beard;
    [SerializeField] private Sprite head;
    [SerializeField] private Sprite chest;
    [SerializeField] private Sprite hand;
    [SerializeField] private Sprite underware;
    [SerializeField] private Sprite leg;
    [SerializeField] private Sprite foot;
    

    [Header("Equipment")]
    [SerializeField] private Sprite helmet;
    [SerializeField] private Sprite glasses;
    [SerializeField] private Sprite shoulderLeft;
    [SerializeField] private Sprite shoulderRight;
    [SerializeField] private Sprite back;
    [SerializeField] private Sprite torso;
    [SerializeField] private Sprite gloves;
    [SerializeField] private Sprite pants;
    [SerializeField] private Sprite boots;
    [SerializeField] private Sprite weaponLeft;
    [SerializeField] private Sprite weaponRight;
    
    public Color32 SkinColor => skinColor;
    public Sprite Hair => hair;
    public Sprite Beard => beard;
    public Sprite Head => head;
    public Sprite Chest => chest;
    public Sprite Hand => hand;
    public Sprite Underware => underware;
    public Sprite Leg => leg;
    public Sprite Foot => foot;

    public Sprite Helmet => helmet;
    public Sprite Glasses => glasses;
    public Sprite ShoulderLeft => shoulderLeft;
    public Sprite ShoulderRight => shoulderRight;
    public Sprite Back => back;
    public Sprite Torso => torso;
    public Sprite Gloves => gloves;
    public Sprite Pants => pants;
    public Sprite Boots => boots;
    public Sprite WeaponLeft => weaponLeft;
    public Sprite WeaponRight => weaponRight;
}