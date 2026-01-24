using UnityEngine;

public class TestCamSwitch : MonoBehaviour
{
    public Camera camMain;
    public Camera cam2;
    private bool flipper;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (flipper)
            {
                camMain.enabled = true;
                cam2.enabled = false;
                flipper = !flipper;
            }
            else
            {
                camMain.enabled = false;
                cam2.enabled = true;
                flipper = !flipper;
            }
        }
    }
}
