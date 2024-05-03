using UnityEngine;

public class ObjectVisibilityToggle : MonoBehaviour
{
    public float toggleInterval = 2f;
    public float disableDelay = 1f;

    private bool canToggle = true;

    public GameObject StartText;
    public GameObject ExitText;

    private SpriteRenderer color;

    private AudioSource ClickSound;
    public AudioClip clip;
    

    

    private void Start()
    {
        InvokeRepeating("ToggleVisibility", 0f, toggleInterval);
        StartText.SetActive(false);
        ExitText.SetActive(false);
        color = gameObject.GetComponent<SpriteRenderer>();
        ClickSound = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canToggle)
        {
            ClickSound.PlayOneShot(clip);
            canToggle = false;
            CancelInvoke("ToggleVisibility");
            Invoke("DisableObject", disableDelay);
        }
    }

    private void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void DisableObject()
    {
        color.material.color = Color.black;
        StartText.SetActive(true);
        ExitText.SetActive(true);
    }
}


