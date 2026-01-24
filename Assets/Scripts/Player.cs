using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference select;

    private void OnEnable()
    {
        move.action.started += Move;
    }

    private void OnDisable()
    {
        move.action.started -= Move;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Debug.Log("move");
        gameObject.transform.position += new Vector3(100.0f, 0.0f, 0.0f);
    }
}
