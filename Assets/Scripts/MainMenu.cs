using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public Image loadingProgress;
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadingScreen());
    }
  
    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for(int i = 0; i < scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgress.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
