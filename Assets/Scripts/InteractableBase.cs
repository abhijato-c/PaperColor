
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    
    [SerializeField] protected AudioSource interactAudio;
    [SerializeField] protected AudioSource resetAudio;

    public abstract void Interact();
    public abstract void Reset();

    protected IObstacle GetObstacleFromTarget()
    {
        return target.GetComponent<IObstacle>();
    }
    protected void PlayInteractionSFX()
    {
        interactAudio.time = 0.0f;
        interactAudio.Play();
    }
    protected void PlayResetSFX()
    {
        // reverses clip
        resetAudio.time = resetAudio.clip.length - 0.01f;
        resetAudio.Play();
    }
}