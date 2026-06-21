using UnityEngine;

public class Final : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(100,100,collision.gameObject.transform.position.z);
        }
    }
}
