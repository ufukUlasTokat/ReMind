using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class CartController : MonoBehaviour
{
    private Rigidbody cartRigidbody;
    private XRGrabInteractable grabInteractable;

    private Transform originalParent;
    private Transform playerHand;

    void Start()
    {
        // Get the Rigidbody and XRGrabInteractable components
        cartRigidbody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Store the original parent of the cart
        originalParent = transform.parent;

        // Ensure the Rigidbody is set up correctly
        cartRigidbody.isKinematic = true; // Make kinematic to prevent physics interference

        // Subscribe to the grab and release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Get the transform of the hand or controller grabbing the cart
        playerHand = args.interactorObject.transform;

        // Parent the cart to the player's hand
        transform.SetParent(playerHand);

        // Optionally reset the cart's position relative to the player's hand
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Cart grabbed and parented to player hand!");
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Unparent the cart from the player's hand
        transform.SetParent(originalParent);

        // Optionally keep the cart at its current position or apply a release force
        cartRigidbody.isKinematic = false; // Re-enable physics if necessary

        Debug.Log("Cart released and unparented from player hand!");
    }
}
