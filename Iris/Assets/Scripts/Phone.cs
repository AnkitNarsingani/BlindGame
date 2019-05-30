using System;
using UnityEngine;

public class Phone : MonoBehaviour, UnityEngine.EventSystems.IPointerDownHandler
{
    AudioSource audioSource;
    RectTransform rectTransform;
    [SerializeField] AudioClip vibrate;

    Animator animator;
    [Range(2, 5)] [SerializeField] private float animationSpeed;
    [SerializeField] private float YPositionMaxClamp;
    private float YPositionMinClamp;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        YPositionMinClamp = rectTransform.anchoredPosition.y;
    }

    public void RecieveCall()
    {
        StartCoroutine(GoingUpAnimation());
        if (audioSource.isPlaying)
            audioSource.Stop();

        animator.SetBool("Vibrate", true);
        audioSource.clip = vibrate;
        audioSource.Play();
    }

    private System.Collections.IEnumerator GoingUpAnimation()
    {
        while (rectTransform.anchoredPosition.y < YPositionMaxClamp)
        {
            rectTransform.position += new Vector3(0, animationSpeed, 0);
            yield return null;
        }
    }

    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData data)
    {
        if (audioSource.isPlaying && audioSource.clip == vibrate)
        {
            AnswerCall();
        }
    }

    private void AnswerCall()
    {
        audioSource.Stop();
        animator.SetBool("Vibrate", false);

    }
}
