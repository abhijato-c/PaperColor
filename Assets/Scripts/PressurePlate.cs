using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : MonoBehaviour{
    public SpriteRenderer Door;
    public BoxCollider2D DoorCollider;
    public Sprite DoorOpen;
    public Sprite DoorClosed;
    // private bool active = false;

    void Start() {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Activate();
        GameManager.Instance.AddInteraction(this.Activate);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Reset();
        GameManager.Instance.AddInteraction(this.Reset);
    }

    public void Activate() {
        Door.sprite = DoorOpen;
        DoorCollider.enabled = false;
    }

    public void Reset() {
        // check if any ghosts are still on it
        List<Collider2D> overlaps = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(ContactFilter2D.noFilter, overlaps);
        foreach (Collider2D collider in overlaps)
        {
            if (collider.gameObject.CompareTag("ghost")) { return; }
        }

        Door.sprite = DoorClosed;
        DoorCollider.enabled = true;
    }
}
