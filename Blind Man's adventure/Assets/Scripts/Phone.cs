using System;
using UnityEngine;

public class Phone : MonoBehaviour, UnityEngine.EventSystems.IPointerDownHandler
{
    AudioSource audioSource;
    [SerializeField] AudioClip vibrate;
    Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void RecieveCall()
    {
        if(audioSource.isPlaying)
            audioSource.Stop();

        animator.Play("Vibrate");
        audioSource.clip = vibrate;
        audioSource.Play();
    }

    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData data)
    {
        if (audioSource.isPlaying && audioSource.clip == vibrate)
        {
            RejectCall();
        }
    }

    private void RejectCall()
    {
        animator.SetBool("Rejected", true);
    }
}
