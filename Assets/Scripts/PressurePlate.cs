using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : InteractableOverlap {
    public Sprite Relaxed;
    public Sprite Compressed;

    public void Start()
    {
    }

    public override void Interact() {
        PlayInteractionSFX();
        gameObject.GetComponent<SpriteRenderer>().sprite = Compressed;

        GetObstacleFromTarget().Activate();
    }


    public override void Reset() {
        PlayResetSFX();
        gameObject.GetComponent<SpriteRenderer>().sprite = Relaxed;

        GetObstacleFromTarget().Deactivate();
    }
}
