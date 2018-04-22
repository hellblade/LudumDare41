using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    public UnityEvent PickedUp = new UnityEvent();
    private bool isPickedUp;

    protected bool destroyOnPickup;

    /// <summary>
    /// Gets the type of component required when triggered
    /// </summary>
    /// <value>The required component.</value>
    protected abstract System.Type RequiredComponent { get; }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var component = other.gameObject.GetComponent(RequiredComponent);

        if (component)
        {
            if (!OnPickup(component))
                return;

            PickedUp.Invoke();

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Handles being picked up. Returns false is target should not pick it up
    /// </summary>
    /// <param name="target">Target.</param>
    protected abstract bool OnPickup(Component target);
}