using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<Sprite> slides;
    private int _count;
    public Image image;

    private void Start()
    {
        NextSlide();
    }

    public void NextSlide()
    {
        if (_count == slides.Count)
        {
            Destroy(gameObject); // No more slides
            return;
        }
        image.sprite = slides[_count++];
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || 
            Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
        {
            NextSlide();
        }
    }
}