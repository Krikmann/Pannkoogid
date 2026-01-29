using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    [SerializeField] private UnityEvent clicked;
    private MouseInputProvider _mouse;
    private CapsuleCollider2D _collider;

    public GameObject visitorPanel;
    public Camera visitorCamera;
    public Camera mainCamera;

    private Button _idCardBtn;
    private Button _ermBtn;
    private Button _suudiBtn;

    private bool _bZoomedIn;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _mouse = FindFirstObjectByType<MouseInputProvider>();
        _mouse.Clicked += MouseOnClicked;

        GameEvent.current.onZoomOutTriggered += ZoomOut;
        GameEvent.current.onRequestedZoomOut += GlobalZoomOut;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        _idCardBtn = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .FirstOrDefault(b => b.name == "IDCardTestButton");
        _ermBtn = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .FirstOrDefault(b => b.name == "ERMTestButton");
        _suudiBtn = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .FirstOrDefault(b => b.name == "SuudiButton");
    }

    private void MouseOnClicked()
    {
        // Ignore click if hovering any of the buttons
        if ((_idCardBtn != null && _mouse.IsPointerOverUI(_idCardBtn.gameObject)) ||
            (_ermBtn != null && _mouse.IsPointerOverUI(_ermBtn.gameObject)) ||
            (_suudiBtn != null && _mouse.IsPointerOverUI(_suudiBtn.gameObject)))
            return;
        
        if (_collider.OverlapPoint(_mouse.WorldPosition))
        {
            GameEvent.current.RequestZoomOut();
            ZoomIn();
        }
    }

    public void ZoomIn()
    {
        _bZoomedIn=true;
        visitorCamera.enabled = true;
        mainCamera.enabled = false;
        visitorPanel.SetActive(true);
        _mouse.SetCamera(visitorCamera);

        //pass this visitor as gameobject
        GameEvent.current.ZoomedIn(gameObject);
    }

    public void ZoomOut()
    {
        _bZoomedIn = false;
        mainCamera.enabled = true;
        visitorCamera.enabled = false;
        visitorPanel.SetActive(false);
        _mouse.SetCamera(mainCamera);
    }

    private void GlobalZoomOut()
    {
        if (_bZoomedIn)
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