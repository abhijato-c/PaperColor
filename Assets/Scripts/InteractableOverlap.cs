using UnityEngine;

public abstract class InteractableOverlap : InteractableBase
{
    public void OnTriggerEnter2D(Collider2D collision) {
        Interact();
        GameManager.Instance.AddInteraction(this.Interact);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        Reset();
        GameManager.Instance.AddInteraction(this.Reset);
    }
}