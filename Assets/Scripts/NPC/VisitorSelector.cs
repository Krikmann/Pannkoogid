using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisitorSelector : MonoBehaviour
{
    [SerializeField] private UnityEvent clicked;

    public GameObject visitorPanel;
    public Camera visitorCamera;
    private Camera _mainCamera;

    private bool _dragging;
    private MouseInputProvider _mouse;

    private Button _syydiButton;
    private Button _idButton;
    private Button _ermButton;
    private Button _zoomOutButton;

    private void Awake()
    {
        _mouse = FindFirstObjectByType<MouseInputProvider>();
        _mainCamera = Camera.main;

        _syydiButton = Resources.FindObjectsOfTypeAll<Button>()
            .FirstOrDefault(b => b.name == "SuudiButton");
        _idButton = Resources.FindObjectsOfTypeAll<Button>()
            .FirstOrDefault(b => b.name == "IDCardTestButton");
        _ermButton = Resources.FindObjectsOfTypeAll<Button>()
            .FirstOrDefault(b => b.name == "ERMTestButton");
        _zoomOutButton = Resources.FindObjectsOfTypeAll<Button>()
            .FirstOrDefault(b => b.name == "ZoomOutButton");
    }

    private void OnMouseDown()
    {
        Button[] btns = { _syydiButton, _idButton, _ermButton, _zoomOutButton };
        Debug.Log(btns.Any(b =>
            RectTransformUtility.RectangleContainsScreenPoint(b.GetComponent<RectTransform>(), Input.mousePosition,
                null)));
        if (ButtonScript.Instance.isZoomedIn && btns.Any(b =>
                RectTransformUtility.RectangleContainsScreenPoint(b.GetComponent<RectTransform>(), Input.mousePosition,
                    null))) return;
        _dragging = true;
    }

    private void OnMouseUp()
    {
        if (!_dragging) return;
        _dragging = false;

        // Zoom out and in really quickly
        if (ButtonScript.Instance.isZoomedIn)
            ButtonScript.Instance.GetVisitor().GetComponent<VisitorSelector>().ZoomOut();
        ZoomIn();

        //pass this visitor as gameobject
        ButtonScript.Instance.SetCurrentVisitor(gameObject);
    }


    public void ZoomIn()
    {
        visitorCamera.enabled = true;
        _mainCamera.enabled = false;
        visitorPanel.SetActive(true);
        _mouse.SetCamera(visitorCamera);
        ButtonScript.Instance.isZoomedIn = true;
    }

    public void ZoomOut()
    {
        _mainCamera.enabled = true;
        visitorCamera.enabled = false;
        visitorPanel.SetActive(false);
        _mouse.SetCamera(_mainCamera);
        ButtonScript.Instance.isZoomedIn = false;
    }
}