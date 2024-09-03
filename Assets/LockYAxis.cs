using UnityEngine;

public class LockYAxis : MonoBehaviour
{
    public float yPosition = -0.1393829f; // Set this to the ground level

    void Update()
    {
        Vector3 position = transform.position;
        position.y = yPosition;
        transform.position = position;
    }
}