using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonfunsion : MonoBehaviour
{
    public GameObject LoadingText;

    private void Start()
    {
        LoadingText.SetActive(false);
    }

    public void StartFunsion()
    {
        LoadingText.SetActive(true);
        Invoke("DeactivateLoadingText", 5f);
    }

    private void DeactivateLoadingText()
    {
        LoadingText.SetActive(false);
        SceneManager.LoadScene("StartStoryScene");
    }

    public void ExitFunsion()
    {
        Application.Quit();
    }
}
