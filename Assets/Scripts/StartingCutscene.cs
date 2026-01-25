using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingCutscene : MonoBehaviour
{
    public Sprite[] images;
    private int _counter = 0;
    public Image image;

    private void Awake()
    {
        NextImage();
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("Start");
    }

    public void NextImage()
    {
        if (_counter >= images.Length) return;
        image.sprite = images[_counter++];
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(SceneRefs.Level1); // end cutscene
    }
}
