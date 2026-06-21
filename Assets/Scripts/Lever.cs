using System;
using UnityEngine;

public class Lever : InteractableBase
{
    public Sprite OnSprite;
    public Sprite OffSprite;

    public void Start()
    {
        Type = InteractableType.ManualTrigger;
    }

    public override void Interact()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OnSprite;
        GetObstacleFromTarget().Activate();
    }

    public override void Reset()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OffSprite;
        GetObstacleFromTarget().Deactivate();
    }
}
