using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Selectable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent clicked;
    private MouseInputProvider mouse;

    private BoxCollider2D collider;
    
    public GameObject visitorPanel;
    public Camera visitorCamera;

    private Camera playerCamera;
    
    private void Awake()
    {
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        
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
            Debug.DrawLine(collider.bounds.min, collider.bounds.max, Color.red, 2f);
            Debug.Log("click not in bounds");
        }
    }

    public void ZoomIn()
    {
        playerCamera.enabled = false;
        visitorCamera.enabled = true; 
        //visitorPanel.SetActive(true);
    }

    public void ZoomOut()
    {
        playerCamera.enabled = true;
        visitorCamera.enabled = false;
        //visitorPanel.SetActive(false);
    }
}
