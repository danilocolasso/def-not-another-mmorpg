using System;
using System.Linq;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Serializable]
    public struct WeaponSlots
    {
        public Weapon Right;
        public Weapon Left;
    }

    private Character character;
    [SerializeField]  private WeaponSlots weapons;

    public WeaponSlots Weapons => weapons;

    public void Initialize(Character character)
    {
        this.character = character;
        EquipDefaultWeapons();
    }

    public void Equip(Weapon weapon)
    {
        Equip(weapon, weapon.hands.First());
    }

    public void Equip(Weapon weapon, Weapon.Hand hand)
    {
        Debug.Log($"Equipping {weapon.name} to {hand} hand.");
        if (hand == Weapon.Hand.Left)
        {
            EquipLeftWeapon(weapon);
        }
        else
        {
            EquipRightWeapon(weapon);
        }
    }

    public void Unequip(Weapon.Hand hand)
    {
        character.Graphics.Unequip(hand);
    }

    private void EquipRightWeapon(Weapon weapon)
    {
        if (Weapons.Right != null)
        {
            Unequip(Weapon.Hand.Right);
        }

        character.Graphics.Equip(weapon, Weapon.Hand.Right);
        weapons.Right = weapon;

        Debug.Log($"Equipped {weapon.name} to right hand.");
    }

    private void EquipLeftWeapon(Weapon weapon)
    {
        if (Weapons.Left != null)
        {
            Unequip(Weapon.Hand.Left);
        }

        if (Weapons.Right != null && Weapons.Right.hands.Contains(Weapon.Hand.Both))
        {
            Unequip(Weapon.Hand.Right);
        }

        character.Graphics.Equip(weapon, Weapon.Hand.Left);
        weapons.Left = weapon;
    }

    private void EquipDefaultWeapons()
    {
        if (weapons.Right != null)
        {
            Equip(weapons.Right, Weapon.Hand.Right);
        }

        if (weapons.Left != null)
        {
            Equip(weapons.Left, Weapon.Hand.Left);
        }
    }
}
