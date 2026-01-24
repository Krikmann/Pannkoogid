using UnityEngine;

public class Selectable : MonoBehaviour
{
    public GameObject visitorPanel;
    public Camera visitorCamera;

    private Camera mainCam;
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void ZoomIn()
    {
        mainCam.enabled = false;
        visitorCamera.enabled = true;
        visitorPanel.SetActive(true);
    }

    public void ZoomOut()
    {
        mainCam.enabled = true;
        visitorCamera.enabled = false;
        visitorPanel.SetActive(false);
    }
}
