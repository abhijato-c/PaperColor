using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputAction interactAction;

    private List<InteractableManual> listeningInteractableManuals;

    public void Start()
    {
        listeningInteractableManuals = new();
    }

    public void AddListener(InteractableManual listener)
    {
        listeningInteractableManuals.Add(listener);
        interactAction.Enable();
    }
    public void RemoveListener(InteractableManual listener)
    {
        listeningInteractableManuals.Remove(listener);
        interactAction.Disable();
    }

    private void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            foreach (InteractableManual listener in listeningInteractableManuals)
            {
                listener.Interact();
            }
        }
    }

}
