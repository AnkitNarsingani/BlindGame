using UnityEngine;
using UnityEngine.UI;

public class AlarmClock : MonoBehaviour, UnityEngine.EventSystems.IPointerDownHandler
{
    [SerializeField] private AudioClip alarmRing;
    [SerializeField] private AudioClip alarmSnooze;
    [Range(0.05f, 1)] [SerializeField] private float fastForwardSpeed;
    [Range(2, 5)] [SerializeField] private float animationSpeed;
    [SerializeField] private float YPositionMaxClamp;
    private float YPositionMinClamp;
    private Text minutes;
    private AudioSource alarmSource;
    RectTransform rectTransform;
    private int alarmCounter = 0;

    [SerializeField] UnityEngine.Events.UnityEvent onAlarmSnoozed;

    void Start()
    {
        minutes = transform.GetChild(1).GetComponentInChildren<Text>();
        alarmSource = GetComponent<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
        YPositionMinClamp = rectTransform.anchoredPosition.y;
        StartAlarm();
    }

    public void StartAlarm()
    {
        StartCoroutine(GoingUpAnimation());

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

    private System.Collections.IEnumerator GoingUpAnimation()
    {
        while(rectTransform.anchoredPosition.y < YPositionMaxClamp)
        {       
            rectTransform.position += new Vector3(0, animationSpeed, 0);
            yield return null;
        }
    }

    private System.Collections.IEnumerator FastForwardTime(int amount)
    {
        yield return new WaitForSeconds(1);
        int currentTime = int.Parse(minutes.text);
        float currentSpeed = fastForwardSpeed;
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
            fastForwardSpeed -= 0.3f;
        }  
        else
        {
            while(rectTransform.position.y > YPositionMinClamp)
            {
                rectTransform.position -= new Vector3(0, animationSpeed, 0);
                yield return null;
            }

            onAlarmSnoozed.Invoke();
        }
            
    }
}
