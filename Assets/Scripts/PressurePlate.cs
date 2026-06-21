using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : InteractableOverlap {
    public Sprite Relaxed;
    public Sprite Compressed;
    public AudioSource pressedAudio;
    public AudioSource releasedAudio;

    public void Start()
    {
    }

    public override void Interact() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Compressed;

        GetObstacleFromTarget().Activate();

        pressedAudio.time = 0.0f;
        pressedAudio.Play();
    }

    public override void Reset() {
        // check if any ghosts are still on it
        List<Collider2D> overlaps = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(ContactFilter2D.noFilter, overlaps);
        foreach (Collider2D collider in overlaps) {
            if (collider.gameObject.CompareTag("Entity")) return;
            if (collider.gameObject.CompareTag("Player")) return;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = Relaxed;
        GetObstacleFromTarget().Deactivate();

        releasedAudio.time = releasedAudio.clip.length - 0.01f;
        releasedAudio.Play();
    }
}
