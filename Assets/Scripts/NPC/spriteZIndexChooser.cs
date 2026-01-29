using System;
using UnityEngine;

public class spriteZIndexChooser : MonoBehaviour
{
    public SpriteRenderer sr;

    private void Update()
    {
        // Higher Y = further back, lower Y = in front
        sr.sortingOrder = -(int)(transform.position.y * 1000);
    }
}
