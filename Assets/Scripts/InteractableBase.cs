
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected GameObject target;

    public abstract void Interact();
    public abstract void Reset();

    protected IObstacle GetObstacleFromTarget()
    {
        return target.GetComponent<IObstacle>();
    }
}