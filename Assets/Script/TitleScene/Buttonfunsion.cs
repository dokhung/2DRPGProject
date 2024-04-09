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
        Debug.Log("StartFunsion is On");
        LoadingText.SetActive(true);

        // 5초 후에 DeactivateLoadingText 함수를 호출
        Invoke("DeactivateLoadingText", 5f);
    }

    private void DeactivateLoadingText()
    {
        LoadingText.SetActive(false);
        SceneManager.LoadScene("StartStoryScene");
    }

    public void ExitFunsion()
    {
        Debug.Log("ExitFunsion is On");
        Application.Quit();
    }
}
