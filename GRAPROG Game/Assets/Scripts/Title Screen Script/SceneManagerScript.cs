using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    [Header("Set Components")]
    public Slider LoadingBar;
    public TextMeshProUGUI ProgressText;
    public Animator SceneTransitionAnimator;
    public GameObject LoadingScreen;

    [Header("Scene Manager Parameters")]
    public string SceneName;

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneName));
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().name));
    }

    IEnumerator LoadLevelAsync (string SceneName)
    {
        AsyncOperation Loading = SceneManager.LoadSceneAsync(SceneName);

        LoadingScreen.SetActive(true);

        while (!Loading.isDone) 
        {
            float LoadProgress = Mathf.Clamp01(Loading.progress/ 0.9f);
            LoadingBar.value = LoadProgress;
            ProgressText.text = LoadProgress * 100 + "%";

            yield return null;
        }

        if (Loading.isDone)
        {
            SceneTransitionAnimator.SetTrigger("SceneChange");
        }
    }
}
