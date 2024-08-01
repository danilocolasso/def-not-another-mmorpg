using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private readonly Dictionary<HumanoidArm, Weapon> weapons = new();
    private readonly Dictionary<Equipment, Equipment> equipments = new();

    [SerializeField] private HumanoidGraphics humanoidGraphics;

    public HumanoidArms Arms => humanoidGraphics.Arms;

    public void Initialize(Character character)
    {
        Debug.Assert(humanoidGraphics != null, $"Critical --> Assign a HumanoidGraphics to {character} EquipmentManager in the Inspector!");
        InitializeWeapons();
    }

    public void Equip(Equipment equipment)
    {
        equipments[equipment] = equipment;
    }

    public void Equip(Weapon weapon, HumanoidArm arm)
    {
        if (weapon.hands.Contains(Weapon.Hand.Both))
        {
            Unequip(humanoidGraphics.Arms.Left);
        }

        Unequip(arm);
        weapons[arm] = weapon;
        weapon.Equip(arm);
    }

    public void Unequip(Equipment equipment)
    {
        equipments[equipment] = null;
    }

    public void Unequip(HumanoidArm arm)
    {
        if (weapons[arm] != null)
        {
            weapons[arm].Unequip(arm);
            weapons[arm] = null;
        }
    }

    public void Wield()
    {
        humanoidGraphics.Wield();
    }

    public void Unwield()
    {
        humanoidGraphics.Unwield();
    }

    public void Aim(Character target)
    {
        if (weapons[humanoidGraphics.Arms.Right] != null)
        {
            weapons[humanoidGraphics.Arms.Right].Aim(humanoidGraphics.Arms.Right, target);
        }

        if (weapons[humanoidGraphics.Arms.Left] != null)
        {
            weapons[humanoidGraphics.Arms.Left].Aim(humanoidGraphics.Arms.Left, target);
        }
    }

    private void InitializeWeapons()
    {
        weapons.Add(humanoidGraphics.Arms.Left, null);
        weapons.Add(humanoidGraphics.Arms.Right, null);
    }
}
