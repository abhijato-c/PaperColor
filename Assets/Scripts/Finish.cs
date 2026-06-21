using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Finish : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameManager.Instance.CompleteLevel(this);

        }
    }

    public void SplashColor(Color32 color)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.material.SetColor("_Color", color);
        sprite.material.SetInt("_EnableMask", 1);
    }
}
