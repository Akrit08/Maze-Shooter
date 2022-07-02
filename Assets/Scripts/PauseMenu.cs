using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenu;
    public Slider slider;
    public GameObject sensitivityText;
    void Start()
    {
        Resume();
    }
    private void Update()
    {
        sensitivityText.GetComponent<TMPro.TextMeshProUGUI>().text = "" + slider.value;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public  void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuUI.SetActive(false);
        SettingsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadSettings()
    {
        PauseMenuUI.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void GoBackToPauseMenu()
    {
        SettingsMenu.SetActive(false);
        PauseMenuUI.SetActive(true);
    }
    public void OnSensitivityChange()
    {       
        PlayerLook.mouseSensitivity = slider.value;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Game_Menu");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
