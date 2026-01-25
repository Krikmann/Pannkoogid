using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    public Sprite[] images;
    private int _counter = 0;
    public Image image;

    private void Awake()
    {
        NextImage();
    }

    public void NextImage()
    {
        if (_counter >= images.Length)
        {
            SceneManager.LoadScene(SceneRefs.MainMenu); // end cutscene
            return;
        }

        image.sprite = images[_counter];
        _counter++;
    }
}
