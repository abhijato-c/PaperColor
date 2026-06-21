using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Piston : MonoBehaviour, IObstacle {
    public Sprite ExtendedSprite;
    public Sprite RetractedSprite;
    public bool state = true;

    [SerializeField] private BoxCollider2D extendedCollider;
    [SerializeField] private BoxCollider2D retractedCollider;

    public void Activate() {
        // activate retracts
        GetComponent<SpriteRenderer>().sprite = RetractedSprite;
        state = false;

        extendedCollider.enabled = false;
        retractedCollider.enabled = true; 
    }
    public void Deactivate() {
        // deactivate extends
        GetComponent<SpriteRenderer>().sprite = ExtendedSprite;
        state = true;

        extendedCollider.enabled = true;
        retractedCollider.enabled = false;
    }
}
