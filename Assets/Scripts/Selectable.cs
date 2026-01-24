using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    [SerializeField] private UnityEvent clicked;
    private MouseInputProvider mouse;
    private BoxCollider2D collider;

    public GameObject visitorPanel;
    public Camera visitorCamera;
    public Camera MainCamera;

    private bool bZoomedIn = false;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        Debug.Log("Box " + collider.bounds);
        mouse = FindFirstObjectByType<MouseInputProvider>();
        mouse.Clicked += MouseOnClicked;

        GameEvent.current.onZoomOutTriggered += ZoomOut;
        GameEvent.current.onRequestedZoomOut += GlobalZoomOut;
        MainCamera = Camera.main;
    }

    private void MouseOnClicked()
    {
        Debug.Log("Mouse: " + mouse.WorldPosition + ", Collider: " + collider.bounds);
        Debug.Log("MousePos: " + mouse.WorldPosition);
        if (collider.OverlapPoint(mouse.WorldPosition))
        {
            GameEvent.current.RequestZoomOut();

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
        bZoomedIn=true;
        visitorCamera.enabled = true;
        MainCamera.enabled = false;
        visitorPanel.SetActive(true);
        mouse.SetCamera(visitorCamera);


    }

    public void ZoomOut()
    {
        bZoomedIn = false;
        MainCamera.enabled = true;
        visitorCamera.enabled = false;
        visitorPanel.SetActive(false);
        mouse.SetCamera(MainCamera);
    }

    private void GlobalZoomOut()
    {
        if (bZoomedIn)
        {
            ZoomOut();
        }
    }

    private void OnDestroy()
    {
        if (GameEvent.current != null)
        {
            GameEvent.current.onZoomOutTriggered -= ZoomOut;
            GameEvent.current.onRequestedZoomOut -= GlobalZoomOut;
        }
    }
}