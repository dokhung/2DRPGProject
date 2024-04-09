using UnityEngine;

public class ObjectVisibilityToggle : MonoBehaviour
{
    public float toggleInterval = 2f;      // 게임 오브젝트를 보이고 숨기는 간격
    public float disableDelay = 1f;         // 클릭 이후 깜빡이지 않을 시간

    private bool canToggle = true;

    public GameObject StartText;
    public GameObject ExitText;

    private SpriteRenderer color;

    private AudioSource ClickSound;
    public AudioClip clip;
    

    

    private void Start()
    {
        // InvokeRepeating을 사용하여 주기적으로 ToggleVisibility 메서드를 호출합니다.
        InvokeRepeating("ToggleVisibility", 0f, toggleInterval);
        StartText.SetActive(false);
        ExitText.SetActive(false);
        color = gameObject.GetComponent<SpriteRenderer>();
        ClickSound = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되면 깜빡임을 중지하고 비활성화 예약
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
        // 현재 게임 오브젝트의 활성화 여부를 반전시킵니다.
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void DisableObject()
    {
        // 게임 오브젝트를 배경과 같은 색으로 하여 보이지 않는 연출을 함
        
        color.material.color = Color.black;
        StartText.SetActive(true);
        ExitText.SetActive(true);
    }
}


