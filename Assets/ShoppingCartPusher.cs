using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShoppingCartPusher : MonoBehaviour
{
    public GameObject handleCollider;    // Assign this in the Inspector to the HandleCollider GameObject
    public Rigidbody cartRigidbody;      // Assign this in the Inspector to the shopping cart's Rigidbody
    public Transform playerTransform;    // Assign this in the Inspector to the player's Transform

    private XRGrabInteractable grabInteractable;
    private Vector3 initialCartOffset;
    private Quaternion initialCartRotationOffset;
    private bool isGrabbed = false;

    void Start()
    {
        // Get the XRGrabInteractable component from the handleCollider
        grabInteractable = handleCollider.GetComponent<XRGrabInteractable>();

        // Ensure Rigidbody constraints are set to prevent lifting
        cartRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        if (grabInteractable.isSelected && !isGrabbed)
        {
            // When first grabbed, calculate the initial offset and rotation
            initialCartOffset = cartRigidbody.position - playerTransform.position;
            initialCartRotationOffset = Quaternion.Inverse(playerTransform.rotation) * cartRigidbody.rotation;
            isGrabbed = true;
        }

        if (grabInteractable.isSelected)
        {
            // The handle is being grabbed, move the cart based on the player's position
            MoveCartWithPlayer();
        }
        else
        {
            isGrabbed = false; // Reset when the handle is released
        }
    }

    void MoveCartWithPlayer()
    {
        // Maintain the initial offset and rotation while following the player's movement
        Vector3 desiredPosition = playerTransform.position + initialCartOffset;
        Quaternion desiredRotation = playerTransform.rotation * initialCartRotationOffset;

        // Smoothly move and rotate the cart towards the desired position and rotation
        cartRigidbody.MovePosition(Vector3.Lerp(cartRigidbody.position, desiredPosition, Time.fixedDeltaTime * 5f));
        cartRigidbody.MoveRotation(Quaternion.Slerp(cartRigidbody.rotation, desiredRotation, Time.fixedDeltaTime * 5f));
    }
}
