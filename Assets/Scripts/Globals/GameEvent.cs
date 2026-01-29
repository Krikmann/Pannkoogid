using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent current;

    private void Awake()
    {
        current = this;
    }

    public event Action onZoomOutTriggered;
    public void ZoomOutTriggerEnter()
    {
        if(onZoomOutTriggered != null)
        {
            onZoomOutTriggered();
        }
    }

    public event Action onRequestedZoomOut;
    public void RequestZoomOut()
    {
        if (onRequestedZoomOut != null)
        {
            onRequestedZoomOut();
        }
    }

    public event Action<GameObject> onZoomedIn;
    public void ZoomedIn(GameObject visitor)
    {
        onZoomedIn?.Invoke(visitor);
    }
}
