using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRefs : MonoBehaviour
{
    public static SceneRefs Instance;

    public int MainMenu = 0;
    public int Level1 = 1;
    public int Level2 = 2;
    
    
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
