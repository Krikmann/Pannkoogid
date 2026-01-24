using UnityEngine;

public class AudioTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GlobalReferences.audioManager.playSound("TEST");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GlobalReferences.audioManager.playSound("ERM_LOW_0");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GlobalReferences.audioManager.playSound("FAKE_ERM_LOW_0");
        }
    }
}
