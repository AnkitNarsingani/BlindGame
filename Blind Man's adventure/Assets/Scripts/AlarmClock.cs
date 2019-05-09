using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlarmClock : MonoBehaviour, IPointerDownHandler
{
    Text time;
    [SerializeField] private int startMinutes;
    [SerializeField] private float speed = 0.05f;
    private AudioSource[] alarmSound;
    private int minutes;
    private float timer;
    bool canTime = false;

    void Start()
    {
        time = GetComponentInChildren<Text>();
        alarmSound = GetComponents<AudioSource>();
        minutes = startMinutes;
        alarmSound[0].Play();
    }

    void FixedUpdate()
    {
        if(canTime)
        {
            if (minutes != 30)
                ChangeTime();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(!canTime)
        {
            alarmSound[0].Stop();
            alarmSound[1].Play();
            canTime = true;
        }
    }

    void ChangeTime()
    {
        if (timer > speed)
        {
            if(minutes != startMinutes + 15)
            {
                minutes += 1;
                time.text = "07 : " + minutes.ToString();
                timer = 0;
            }
            else
            {
                canTime = false;
                startMinutes += 15;
                alarmSound[0].Play();
                return;
            }
        }

        timer += Time.deltaTime;
    }
}
