using UnityEngine;

public class PressurePlate : MonoBehaviour , IInteractable{
    public SpriteRenderer Door;
    public BoxCollider2D DoorCollider;
    public Sprite DoorOpen;
    public Sprite DoorClosed;
    public bool revert = false;
    public float RevTime = 0.1f;
    private bool active = false;

    void Start() {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Activate();
        GameManager.Instance.AddInteraction(this.Interact);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Reset();
        GameManager.Instance.AddInteraction(this.Interact);
    }

    void Interact()
    {
        Activate();
    }

    public void Activate() {
        active = !active;
        if (active) {
            Door.sprite = DoorOpen;
            DoorCollider.enabled = false;
            if (revert) Invoke("Reset", RevTime);
        }
        else {
            Door.sprite = DoorClosed;
            DoorCollider.enabled = true;
        }
    }

    public void Reset() {
        active = false;
        Door.sprite = DoorClosed;
        DoorCollider.enabled = true;
    }

    void IInteractable.Interact()
    {
        Interact();
    }
}
