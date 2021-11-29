using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectSceneManager : MonoBehaviour
{
    [SerializeField] public int _gameplaySceneOffset = (int)SceneType.Gameplay;
    [Header("Debug")] 
    [SerializeField] private int _currentSceneIndex;

    public static ProjectSceneManager Instance;
    private void Awake()
    {
        Instance = this;
        var activeScene = SceneManager.GetActiveScene();
        var index = activeScene.buildIndex;
        _currentSceneIndex = index;
        CheckGameplayUI();

    }

    private void CheckGameplayUI()
    {
        if (_currentSceneIndex < _gameplaySceneOffset)
        {
            return;
        }

        //We are at gameplay and should check if the UI is loaded.

        var gameplayUIScene = SceneManager.GetSceneByBuildIndex((int)SceneType.GamePlayUI);
        if (gameplayUIScene.isLoaded)
        {
            return;
        }

        SceneManager.LoadScene((int)SceneType.GamePlayUI, LoadSceneMode.Additive);
    }

    public void LoadScene(int sceneIndex)
    {
        if (_currentSceneIndex == sceneIndex)
        {
            return;
        }

        _currentSceneIndex = sceneIndex;

        EventManager.Trigger(eEvent.LoadScene);

        //It should have a fade in/out but for times it is done like this. 

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        CheckGameplayUI();
    }

    public void Restart()
    {
        EventManager.Trigger(eEvent.LoadScene);
        StartCoroutine(RestartRoutine());
    }

    //private IEnumerator RestartRoutine()
    //{
    //    yield return SceneManager.UnloadSceneAsync(_currentSceneIndex);
    //    yield return SceneManager.LoadSceneAsync((int) SceneType.LoadingScene, LoadSceneMode.Additive);
    //    yield return new WaitForSeconds(1f);
    //    yield return SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive);
    //    yield return SceneManager.UnloadSceneAsync((int)SceneType.LoadingScene);
    //}

    private IEnumerator RestartRoutine()
    {
        yield return SceneManager.LoadSceneAsync((int) SceneType.LoadingScene);

        yield return SceneManager.LoadSceneAsync(_currentSceneIndex);
        CheckGameplayUI();
    }

}
