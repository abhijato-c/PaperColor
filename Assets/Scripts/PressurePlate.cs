using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : InteractableBase {
    public Sprite Relaxed;
    public Sprite Compressed;

    public void Start()
    {
        Type = InteractableType.OverlapTrigger;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Interact();
        GameManager.Instance.AddInteraction(this.Interact);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        Reset();
        GameManager.Instance.AddInteraction(this.Reset);
    }

    public override void Interact() {
        gameObject.GetComponent<SpriteRenderer>().sprite = Compressed;

        GetObstacleFromTarget().Activate();
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
    }
}
