using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    public Sprite[] images;
    private int end_counter = 0;
    public Image image;

    private void Awake()
    {
        NextImage();
    }

    public void NextImage()
    {
        if (end_counter >= images.Length)
        {
            SceneManager.LoadScene(SceneRefs.MainMenu); // end cutscene
            return;
        }

        image.sprite = images[end_counter];
        end_counter++;
    }
}
