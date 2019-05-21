using UnityEngine;
using UnityEngine.UI;

public class AlarmClock : MonoBehaviour, UnityEngine.EventSystems.IPointerDownHandler
{
    [SerializeField] private AudioClip alarmRing;
    [SerializeField] private AudioClip alarmSnooze;
    [Range(0.05f, 1)] [SerializeField] private float speed;
    private Text minutes;
    private Animator animator;
    private AudioSource alarmSource;
    private int alarmCounter = 0;
    [SerializeField] UnityEngine.Events.UnityEvent momCallEvent;

    void Start()
    {
        minutes = transform.GetChild(1).GetComponentInChildren<Text>();
        alarmSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void StartAlarm()
    {
        if (alarmSource.isPlaying)
            alarmSource.Stop();

        alarmSource.clip = alarmRing;
        alarmSource.Play();
        alarmCounter++;
    }

    void StopAlarm()
    {
        alarmSource.Stop();
        alarmSource.clip = alarmSnooze;
        alarmSource.Play();
    }

    public void OnPointerDown(UnityEngine.EventSystems.PointerEventData data)
    {
        if(alarmSource.isPlaying && alarmSource.clip == alarmRing)
        {
            StopAlarm();
            StartCoroutine(FastForwardTime(15));
        }
    }

    private System.Collections.IEnumerator FastForwardTime(int amount)
    {
        yield return new WaitForSeconds(1);
        int currentTime = int.Parse(minutes.text);
        float currentSpeed = speed;
        while(amount > 0)
        {
            currentTime++;
            if(currentTime < 10)
                minutes.text = "0" + currentTime.ToString();
            else
                minutes.text = currentTime.ToString();
            yield return new WaitForSeconds(currentSpeed);
            if(alarmCounter != 2)
                currentSpeed -= 0.03f;
            amount--;
        }
        if (alarmCounter != 2)
        {
            StartAlarm();
            speed -= 0.3f;
        }  
        else
        {
            animator.SetBool("CanGoUp", true);
        }
            
    }
}
