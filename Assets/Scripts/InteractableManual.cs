using UnityEngine;

public abstract class InteractableManual : InteractableBase
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<PlayerInteraction>(out PlayerInteraction interaction))
        {
            interaction.AddListener(this);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerInteraction>(out PlayerInteraction interaction))
        {
            interaction.RemoveListener(this);
        }
    }

}