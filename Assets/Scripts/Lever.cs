using System;
using UnityEngine;

public class Lever : InteractableManual
{
    public Sprite OnSprite;
    public Sprite OffSprite;

    private bool state = false;

    public override void Interact()
    {
        if (state == false) {
            PlayInteractionSFX();
            gameObject.GetComponent<SpriteRenderer>().sprite = OnSprite;
            GetObstacleFromTarget().Activate();
            state = true;
        } else
        {
            Reset();
        }
    }

    public override void Reset()
    {
        PlayResetSFX();
        gameObject.GetComponent<SpriteRenderer>().sprite = OffSprite;
        GetObstacleFromTarget().Deactivate();
        state = false;
    }
}