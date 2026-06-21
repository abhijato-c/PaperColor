using JetBrains.Annotations;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour, IObstacle
{
    public Sprite OpenSprite;
    public Sprite ClosedSprite;
    public bool state;

    public void Activate() {
        GetComponent<SpriteRenderer>().sprite = OpenSprite;
        GetComponent<BoxCollider2D>().enabled = false;
        state = true;
    }

    public void Deactivate() {
        GetComponent<SpriteRenderer>().sprite = ClosedSprite;
        GetComponent<BoxCollider2D>().enabled = true;
        state = false;
    }
}
