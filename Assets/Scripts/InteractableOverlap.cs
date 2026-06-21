using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableOverlap : InteractableBase
{
    public void OnTriggerEnter2D(Collider2D collision) {
        if (checkGhostOverlap(false)) { return; }

        Interact();
        GameManager.Instance.AddInteraction(this.Interact);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        // check if any ghosts are still triggering
        if (checkGhostOverlap(true)) { return; }
        
        Reset();
        GameManager.Instance.AddInteraction(this.Reset);
    }

    private bool checkGhostOverlap(bool alsoCheckPlayer) {
        List<Collider2D> overlaps = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(ContactFilter2D.noFilter, overlaps);
        foreach (Collider2D collider in overlaps) {
            if (collider.gameObject.CompareTag("Entity")) return true;
            if (alsoCheckPlayer && collider.gameObject.CompareTag("Player")) return true;
        } 

        return false;
    }
}