using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCameraPanner : MonoBehaviour
{
    public float panSpeed = 6f;

    public InputActionReference Move;
    private Vector2 moveDirection;

    public PolygonCollider2D movemntBounds;

    // Update is called once per frame
    void Update()
    {
        //bool enabled = Move.action.enabled;
        moveDirection = Move.action.ReadValue<Vector2>();

        //Debug.Log($"Move Enabled: {enabled}, Value: {moveDirection}");
        Vector3 newPosition = transform.position + new Vector3(moveDirection.x, moveDirection.y, 0) * (panSpeed * Time.deltaTime);
        Vector2 newPosition2D = new Vector2(newPosition.x, newPosition.y);

        if (movemntBounds.OverlapPoint(newPosition2D))
        {
            transform.position = newPosition;
        }
        else
        {
            Vector2 closest = movemntBounds.ClosestPoint(newPosition2D);
            transform.position = new Vector3(closest.x, closest.y, transform.position.z);
        }

        //transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * (panSpeed * Time.deltaTime);
        
    }
}
