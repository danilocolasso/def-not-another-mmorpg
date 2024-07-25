using System;
using UnityEngine;

public class CharacterHumanoidGraphics : CharacterGraphics
{
    [Serializable]
    private struct BodyRenderer
    {
        public SpriteRenderer RightHand;
        public SpriteRenderer Head;
        public SpriteRenderer Chest;
        public SpriteRenderer Under;
        public SpriteRenderer RightLeg;
        public SpriteRenderer LeftLeg;
        public SpriteRenderer LeftHand;
    }

    [Serializable]
    private struct ArmsGameObject
    {
        public GameObject Right;
        public GameObject Left;
    }

    [Header("Sprite Renderers")]
    [SerializeField] private BodyRenderer body;

    [Header("Game Objects")]
    [SerializeField] private ArmsGameObject arms;

    public override void Initialize(Character character)
    {
        base.Initialize(character);

        if (arms.Right == null || arms.Left == null)
        {
            Debug.LogWarning($"Performance ---> {character} arms not set in Inspector!");
            SetArms();
        }
    }

    public override void SetColor(Color32 color)
    {
        body.Head.color = color;
        body.RightHand.color = color;
        body.Chest.color = color;
        body.RightLeg.color = color;
        body.Under.color = color;
        body.LeftLeg.color = color;
        body.LeftHand.color = color;
    }

    public override void ExitBattle()
    {
        base.ExitBattle();
        ResetRightArm();
    }

    private void FixedUpdate()
    {
        if (character.IsInBattle)
        {
            UpdateRightArm();
            UpdateLeftArm();
        }

        FaceTarget();
    }

    private void SetArms()
    {
        arms.Right = body.RightHand.transform.parent.gameObject;
        arms.Left = body.LeftHand.transform.parent.gameObject;
    }

    private void UpdateRightArm()
    {
        if (character.Target != null)
        {
            Vector3 direction = character.Target.transform.position - transform.position;
            arms.Right.transform.right = -direction;
            return;
        }

        if (arms.Right.transform.right != Vector3.right)
        {
            ResetRightArm();
        }
    }

    private void UpdateLeftArm()
    {
        // TODO implement
    }

    private void ResetRightArm()
    {
        arms.Right.transform.right = Vector3.right;
    }

    public override void Flip(bool flip)
    {
        base.Flip(flip);
        SwapHands(flip);
        FaceTarget();
    }

    private void FaceTarget()
    {
        if (character.Target == null)
        {
            ResetHead();
            return;
        }

        Vector3 direction = (character.Target.transform.position - transform.position).normalized;

        bool headIsFlipped = body.Head.transform.localRotation.y != 0;
        bool isFacingRight = (IsFlipped && headIsFlipped) || (!IsFlipped && !headIsFlipped);
        bool shouldFaceRight = direction.x > 0;
        bool shouldFlipHead = isFacingRight != shouldFaceRight;

        if (shouldFlipHead)
        {
            body.Head.transform.Rotate(0, headIsFlipped ? -FLIP_ANGLE : FLIP_ANGLE, 0);
        }
    }

    private void ResetHead()
    {
        if (body.Head.transform.localRotation.y != 0)
        {
            body.Head.transform.Rotate(Vector3.zero);
        }
    }

    private void SwapHands(bool flip)
    {
        arms.Right.transform.Rotate(0, flip ? FLIP_ANGLE : -FLIP_ANGLE, 0);
        arms.Left.transform.Rotate(0, flip ? FLIP_ANGLE : -FLIP_ANGLE, 0);

        (body.LeftHand.sortingOrder, body.RightHand.sortingOrder) = (body.RightHand.sortingOrder, body.LeftHand.sortingOrder);
    }
}