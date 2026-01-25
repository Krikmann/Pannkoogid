using System;
using UnityEngine;

public class SyydiPanelAnimOverEmmiter : MonoBehaviour
{
    public ButtonScript visitorPanel;

    public void AnimationOver()
    {
        visitorPanel.SyydiAnimationOver();
    }

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void PlayWoosh()
    {
        GlobalReferences.audioManager.playWHOOSH();
    }
}
