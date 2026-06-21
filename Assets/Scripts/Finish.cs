using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Finish : MonoBehaviour
{
    public float amplitude;
    public float period;

    private Vector3 startPos;

    void Start()
    {
        startPos = gameObject.transform.position;
    }

    void Update() {
        gameObject.transform.position = startPos + Vector3.up * (float)(amplitude * Math.Sin(2.0f * Math.PI / period * Time.fixedUnscaledTime));
    }

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
