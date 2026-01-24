using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference select;
    
    private GameObject hoveredCharacter;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
    
        if (hit.collider != null && hit.collider.CompareTag("Visitor"))
        {
            hoveredCharacter = hit.collider.gameObject;
            // Optional: highlight character
        }
        else
        {
            hoveredCharacter = null;
        }
    }

    private void OnEnable()
    {
        move.action.started += Move;
        select.action.started += Select;
    }

    private void OnDisable()
    {
        move.action.started -= Move;
        select.action.started -= Select;
    }

    private void Select(InputAction.CallbackContext obj)
    {
        if (hoveredCharacter != null)
        {
            Debug.Log("Selected: " + hoveredCharacter.name);
            // Perform selection logic
        }
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Debug.Log("move");
        gameObject.transform.position += new Vector3(100.0f, 0.0f, 0.0f);
    }
}
