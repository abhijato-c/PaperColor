using UnityEngine;

public class PressurePlate : MonoBehaviour, IInteractable {
    private bool active = false;
    void Start() {
        
    }

    public void Interact() {
        active = !active;
        if (active)
            Debug.Log("Plate Activated");
        else
            Debug.Log("Plate Deactivated");
    }

    public void Reset() {
        active = false;
        Debug.Log("reset");
    }
}
