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
        gameObject.GetComponent<SpriteRenderer>().sprite = OffSprite;
        GetObstacleFromTarget().Deactivate();
        state = false;
    }
}