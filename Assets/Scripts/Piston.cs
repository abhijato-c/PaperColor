using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Piston : MonoBehaviour, IObstacle {
    public Sprite ExtendedSprite;
    public Sprite RetractedSprite;
    public bool state;

    [SerializeField] private BoxCollider2D extendedCollider;
    [SerializeField] private BoxCollider2D retractedCollider;

    public void Activate() {
        GetComponent<SpriteRenderer>().sprite = ExtendedSprite;
        state = true;
        print("piston act");

        extendedCollider.enabled = true;
        retractedCollider.enabled = false;
    }
    public void Deactivate() {
        GetComponent<SpriteRenderer>().sprite = RetractedSprite;
        state = false;

        extendedCollider.enabled = false;
        retractedCollider.enabled = true;
    }

    public void Reset() { Activate(); }

    private void OnValidate() {
        if (state) Activate(); 
        else Deactivate();
    }
}
