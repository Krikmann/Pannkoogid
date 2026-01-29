using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScenePicker : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public bool isPaused = false;
    [SerializeField] private bool isMainMenu = false;

    private BackInputProvider back;
    private MouseInputProvider mouse;


    private void Update() {
        // if(!isMainMenu){
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         if(!isPaused)
        //             Pause();
        //         else
        //             Resume();        
        //     }
        // }
    }

    private void Awake()
    {
        back = FindFirstObjectByType<BackInputProvider>();
        back.BackClicked += OnBack;
        mouse = FindFirstObjectByType<MouseInputProvider>();
    }
    private void OnDestroy()
    {
        back.BackClicked -= OnBack;
    }

    public void OnBack()
    {
        if (isMainMenu) return;
    
        if (!isPaused)
            Pause();
        else
            Resume();
    }
    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        mouse.bPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        mouse.bPaused = false;
    }

    public void LoadMenu(){
        
        Resume();
        SceneManager.LoadScene(SceneRefs.MainMenu);
    }
    public void LoadScene1(){
        
        Resume();
        SceneManager.LoadScene(SceneRefs.StartingCutscene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
