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
        mouse = FindFirstObjectByType<MouseInputProvider>();
        mouse.Clicked += MouseOnClicked;

        GameEvent.current.onZoomOutTriggered += ZoomOut;
        GameEvent.current.onRequestedZoomOut += GlobalZoomOut;
        MainCamera = Camera.main;
    }

    private void MouseOnClicked()
    {
        if (collider.OverlapPoint(mouse.WorldPosition))
        {
            GameEvent.current.RequestZoomOut();
            ZoomIn();
        }
    }

    public void ZoomIn()
    {
        bZoomedIn=true;
        visitorCamera.enabled = true;
        MainCamera.enabled = false;
        visitorPanel.SetActive(true);
        mouse.SetCamera(visitorCamera);

        //pass this visitor as gameobject
        GameEvent.current.ZoomedIn(this.gameObject);
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