using UnityEngine;
using UnityEngine.UIElements;

public class OnTouchSound : MonoBehaviour
{
    public AudioClip Touch_Screen_Sound;
    private AudioSource TouchSound;
    public GameObject TouchObject;

    private void Start()
    {
        TouchSound = gameObject.AddComponent<AudioSource>();
    }
}