using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour, IObstacle
{
    public Sprite ClosedSprite;
    public Sprite OpenedSprite;
    public float Force = 16f;
    public float Delay = 0.5f;


    public void Activate()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.time = 0.184f;
        source.Play();
        gameObject.GetComponent<SpriteRenderer>().sprite = OpenedSprite;
    }
    public void Deactivate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ClosedSprite;
    }

    public IEnumerator CloseStaple()
    {
        yield return new WaitForSeconds(Delay);
        Deactivate();
    }

    public void Reset(){ Deactivate(); }
}
