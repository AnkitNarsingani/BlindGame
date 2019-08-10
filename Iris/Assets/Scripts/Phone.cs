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

    [SerializeField] GameObject callUI;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        YPositionMinClamp = rectTransform.anchoredPosition.y;
    }

    public void RecieveCall()
    {
        StartCoroutine(ShowCallUI());   
    }

    private System.Collections.IEnumerator ShowCallUI()
    {
        while (rectTransform.anchoredPosition.y < YPositionMaxClamp)
        {
            rectTransform.position += new Vector3(0, animationSpeed, 0);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        callUI.SetActive(true);

        if (audioSource.isPlaying)
            audioSource.Stop();

        animator.SetBool("Vibrate", true);
        audioSource.clip = vibrate;
        audioSource.Play();
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
        rectTransform.rotation = Quaternion.identity;
        StartCoroutine("GoingDown");
    }

    private System.Collections.IEnumerator GoingDown()
    {
        while (rectTransform.position.y > YPositionMinClamp)
        {
            rectTransform.position -= new Vector3(0, animationSpeed, 0);
            yield return null;
        }
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }              
}
