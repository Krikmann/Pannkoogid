using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInputProvider : MonoBehaviour
{
    private Camera currentCamera;

    public Vector2 WorldPosition {get; private set;}
    public event Action Clicked;

    public bool bPaused = false;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    public void SetCamera(Camera cam)
    {
        currentCamera = cam;
    }

    private void OnMousePos(InputValue value)
    {
        WorldPosition = currentCamera.ScreenToWorldPoint(value.Get<Vector2>());
    }

    private void OnInteract(InputValue _)
    {
        if(!bPaused)
        {
            Clicked?.Invoke();
        }
    }

    public bool IsPointerOverUI(GameObject uiObject)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Any(r => r.gameObject == uiObject);
    }
}
