using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;
    public void ShowGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        GameOverUI.SetActive(true);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Game_Menu");
    }
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
