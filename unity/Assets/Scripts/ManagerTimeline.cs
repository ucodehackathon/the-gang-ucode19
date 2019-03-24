using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTimeline : MonoBehaviour
{
    public static ManagerTimeline Instance { get; private set; }
    public TimerTimeline timer;
    [SerializeField] private Text currentTime;
    [SerializeField] private Slider timeSlider;
    private static DateTime firstTime;
    private static DateTime lastTime;
    private TimeSpan totalDeltaTime { get { return lastTime - firstTime; } }
    private static double updateRate;

    private void Awake()
    {
        Instance = this;
    }

    private const int approximateIndexPlayers = 11;

    private static int approximateIndex(DateTime time)
    {
        return ((int) Math.Round((time - firstTime).TotalMilliseconds / updateRate) * approximateIndexPlayers);
    }

    private static DateTime GetNextTime(DateTime time)
    {
        time = time.AddMilliseconds(updateRate);
        if (time > lastTime)
        {
            return lastTime;
        }
        return time;
    }

    private static int getIndex(DateTime time)
    {
        int approximate = approximateIndex(time);
        DateTime timeApproximate = ParseData.rows[approximate].time;
        if (timeApproximate == time)
        {
            return approximate;
        }
        bool relation = timeApproximate < time;
        int increment = relation ? 1 : -1;
        while(relation == timeApproximate < time)
        {
            approximate += increment;
            timeApproximate = ParseData.rows[approximate].time;
        }

        return approximate;
    }

    // Start is called before the first frame update
    void Start()
    {
        firstTime = ParseData.rows[0].time;
        lastTime = ParseData.rows[ParseData.rows.Count - 1].time;
        DateTime secondTime = firstTime;
        for(int i = 0; i < ParseData.rows.Count; i++)
        {
            if (ParseData.rows[i].time > secondTime)
            {
                secondTime = ParseData.rows[i].time;
                break;
            }
        }

        updateRate = (secondTime - firstTime).TotalMilliseconds;
        StartWatch();
    }

    public void StartWatch()
    {
        timer.StartWatch(firstTime);
    }

    public void StopWatch()
    {
        timer.Stop();
    }

    public void ResumeWatch()
    {
        timer.Resume();
    }

    // Update is called once per frame
    void Update()
    {
        DateTime time = timer.CheckWatch();
        if (time > lastTime)
        {
            time = lastTime;
        }
        UpdateTimeSlider(time);
        currentTime.text = time.ToString();
        if (timer.IsRunning)
        {
            //Debug.Log("BASE: " + time.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            int index = getIndex(time);
            DateTime nextTime = GetNextTime(time);
            int nextIndex = getIndex(nextTime);
            ManagerPlayers.Instance.UpdatePlayers(index, nextIndex);
        } else
        {
            ManagerPlayers.Instance.StopVelocities();
        }
    }

    private const int minimumSecondDelta = 2;

    //scroll between 0 and 1
    public void SetNewTime(float scroll)
    {
        TimeSpan delta = TimeSpan.FromSeconds(totalDeltaTime.TotalSeconds * scroll);
        if (delta.TotalSeconds > minimumSecondDelta)
        {
            DateTime newTime = firstTime + delta;
            timer.Restart(newTime);
        }
    }

    private const float triggerSens = -0.00004f;
    private const float triggerMultBackwards = 2;

    private float triggerInput
    {
        get
        {
            float trigger = Input.GetAxis("JYTrigger") * triggerSens;
            if (trigger < 0)
            {
                trigger *= triggerMultBackwards;
            }
            return trigger;
        }
    }

    private void UpdateTimeSlider(DateTime time)
    {
        float deltaBase = (float) ((time - firstTime).TotalSeconds / totalDeltaTime.TotalSeconds);
        deltaBase += triggerInput;
        timeSlider.value = deltaBase;
    }
}
