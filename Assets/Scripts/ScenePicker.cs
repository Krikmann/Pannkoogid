using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScenePicker : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public bool isPaused = false;
    [SerializeField] private bool isMainMenu = false;


    private void Update() {
        if(!isMainMenu){
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!isPaused)
                    Pause();
                else
                    Resume();        
            }
        }
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMenu(){
        
        Resume();
        SceneManager.LoadScene(SceneRefs.Instance.MainMenu);
    }
    public void LoadScene1(){
        
        Resume();
        SceneManager.LoadScene(SceneRefs.Instance.Level1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
