using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputProvider : MonoBehaviour
{
    public Vector2 WorldPosition {get; private set;}
    public event Action Clicked;

    private void OnMousePos(InputValue value)
    {
        WorldPosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }

    private void OnInteract(InputValue _)
    {
        Clicked?.Invoke();
    }
}
