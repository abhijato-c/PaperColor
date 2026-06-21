using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : MonoBehaviour{
    [SerializeField] public Door door;

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
        door.Open();
    }

    public void Reset() {
        // check if any ghosts are still on it
        List<Collider2D> overlaps = new List<Collider2D>();
        GetComponent<Collider2D>().Overlap(ContactFilter2D.noFilter, overlaps);
        foreach (Collider2D collider in overlaps) {
            if (collider.gameObject.CompareTag("ghost")) { return; }
        }

        door.Close();
    }
}
