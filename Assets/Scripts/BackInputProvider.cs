using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackInputProvider : MonoBehaviour
{
    public event Action BackClicked;

    private void OnBack(InputValue _)
    {
        BackClicked?.Invoke();
    }
}
