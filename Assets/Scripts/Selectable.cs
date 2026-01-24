using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    [SerializeField] private UnityEvent clicked;
    private MouseInputProvider mouse;
    private BoxCollider2D collider;
    private Camera[] cameras;

    public GameObject visitorPanel;
    public Camera visitorCamera;

    private void Awake()
    {
        cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);

        collider = GetComponent<BoxCollider2D>();
        Debug.Log("Box " + collider.bounds);
        mouse = FindFirstObjectByType<MouseInputProvider>();
        mouse.Clicked += MouseOnClicked;
    }

    private void MouseOnClicked()
    {
        Debug.Log("Mouse: " + mouse.WorldPosition + ", Collider: " + collider.bounds);
        Debug.Log("MousePos: " + mouse.WorldPosition);
        if (collider.OverlapPoint(mouse.WorldPosition))
        {
            Debug.Log("Invoke");
            ZoomIn();
        }
        else
        {
            Debug.Log("click not in bounds");
        }
    }

    public void ZoomIn()
    {
        visitorCamera.enabled = true;

        foreach (var cam in cameras)
        {
            if (cam != visitorCamera)
                cam.enabled = false;
        }
        visitorPanel.SetActive(true);
    }

    public void ZoomOut()
    {
        foreach (var cam in cameras)
        {
            if (cam != visitorCamera)
                cam.enabled = true;
        }

        visitorCamera.enabled = false;
        visitorPanel.SetActive(false);
    }
}