using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    public void Open() {
        GetComponent<SpriteRenderer>().sprite = OpenSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Close() {
        GetComponent<SpriteRenderer>().sprite = ClosedSprite;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
