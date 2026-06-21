using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : MonoBehaviour, IInteractable {
    public GameObject target;
    public Sprite Relaxed;
    public Sprite Compressed;
    private SpriteRenderer renderer;

    void Start() {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Interact();
        GameManager.Instance.AddInteraction(this.Interact);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        Reset();
        GameManager.Instance.AddInteraction(this.Reset);
    }

    public void Interact() {
        renderer.sprite = Compressed;

        if (target.TryGetComponent<IObstacle>(out var interactable)) interactable.Activate();
    }

    public void Reset() {
        // check if any ghosts are still on it
        List<Collider2D> overlaps = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(ContactFilter2D.noFilter, overlaps);
        foreach (Collider2D collider in overlaps) {
            if (collider.gameObject.CompareTag("Entity")) return;
            if (collider.gameObject.CompareTag("Player")) return;
        }

        renderer.sprite = Relaxed;
        if (target.TryGetComponent<IObstacle>(out var interactable)) interactable.Deactivate();
    }
}
