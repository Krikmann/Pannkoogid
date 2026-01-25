using System;
using UnityEngine;

public class spriteZIndexChooser : MonoBehaviour
{
    public GameObject visitor;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = visitor.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Higher Y = further back, lower Y = in front
        _sr.sortingOrder = -(int)(transform.position.y * 1000);
    }
}
