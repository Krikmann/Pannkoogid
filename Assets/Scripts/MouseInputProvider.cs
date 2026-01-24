using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputProvider : MonoBehaviour
{
    private Camera currentCamera;

    public Vector2 WorldPosition {get; private set;}
    public event Action Clicked;

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
        Clicked?.Invoke();
    }
}
