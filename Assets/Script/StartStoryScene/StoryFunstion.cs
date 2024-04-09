using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WaitForSeconds = UnityEngine.WaitForSeconds;

public class StoryFunstion : MonoBehaviour
{
    public GameObject Text_Loading;
    public GameObject NextBtn;
    public GameObject[] TextArray;

    private void Start()
    {
        Text_Loading.SetActive(false);
        NextBtn.SetActive(false);
        for (int i = 0; i < TextArray.Length; i++)
        {
            TextArray[i].SetActive(false);
        }
        StartCoroutine(ShowTextWithDelay());

    }

    IEnumerator ShowTextWithDelay()
    {
        for (int i = 0; i < TextArray.Length; i++)
        {
            TextArray[i].SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        
        NextBtn.SetActive(true);
        
        
    }
    
    public void NextBtn_Fun()
    {
        Text_Loading.SetActive(true);
        Invoke("DeactivateLoadingText", 3f);
    }
    
    private void DeactivateLoadingText()
    {
        Text_Loading.SetActive(false);
        SceneManager.LoadScene("StartFirstScene");
    }
}