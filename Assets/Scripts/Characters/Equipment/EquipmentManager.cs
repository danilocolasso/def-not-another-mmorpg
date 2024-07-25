using System;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Serializable]
    public struct Weapons
    {
        public Weapon right;
        public Weapon left;
    }

    private Character character;
    [SerializeField] private Weapons weapons;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void Equip(Equipment equipment)
    {
        // TODO implement
    }

    public void Equip(Weapon weapon)
    {
        Equip(weapon, weapon.hand);
    }

    public void Equip(Weapon weapon, Weapon.Hand hand)
    {
        if (hand == Weapon.Hand.Left)
        {
            EquipLeftHand(weapon);
            return;
        }

        EquipRightHand(weapon);
    }

    public void Unequip(Weapon.Hand hand)
    {
        if (hand == Weapon.Hand.Left)
        {
            UnequipLeftHand();
            return;
        }

        UnequipRightHand();
    }

    private void EquipRightHand(Weapon weapon)
    {
        if (weapons.right != null)
        {
            UnequipRightHand();
        }

        weapons.right = weapon;
        weapons.right.sprite = weapon.sprite;
    }

    private void EquipLeftHand(Weapon weapon)
    {
        if (weapons.left != null)
        {
            UnequipLeftHand();
        }
        else if (weapons.right != null && weapons.right.hand == Weapon.Hand.Both)
        {
            UnequipRightHand();
        }

        weapons.left = weapon;
        weapons.left.sprite = weapon.sprite;
    }

    private void UnequipRightHand()
    {
        weapons.right = null;
        weapons.right.sprite = null;
    }

    private void UnequipLeftHand()
    {
        weapons.left = null;
        weapons.left.sprite = null;
    }
}
