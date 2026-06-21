
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType
{
    OverlapTrigger,
    ManualTrigger
}

public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    public static InteractableType Type;

    public abstract void Interact();
    public abstract void Reset();

    protected IObstacle GetObstacleFromTarget()
    {
        return target.GetComponent<IObstacle>();
    }
}