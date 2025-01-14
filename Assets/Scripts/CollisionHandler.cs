using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                LevelComplete();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void LevelComplete()
    {
        // TODO add sfx and particle effect
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        //TODO add sfx and particles
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            SceneManager.LoadScene(0);
    }
}
