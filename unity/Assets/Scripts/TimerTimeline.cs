using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TimerTimeline : MonoBehaviour
{
    private Stopwatch watch;
    private DateTime time;

    private void Awake()
    {
        watch = new Stopwatch();
    }

    public bool IsRunning { get; private set; }

    public void StartWatch(DateTime time)
    {
        watch.Start();
        IsRunning = true;
        this.time = time;
    }

    public void Restart(DateTime time)
    {
        this.time = time;
        watch.Restart();
    }

    public DateTime StopWatch()
    {
        watch.Stop();
        return time.AddMilliseconds(watch.ElapsedMilliseconds);
    }

    public DateTime CheckWatch()
    {
        return time.AddMilliseconds(watch.ElapsedMilliseconds);
    }

    public void Stop()
    {
        watch.Stop();
        IsRunning = false;
    }

    public void Resume()
    {
        watch.Start();
        IsRunning = true;
    }
}
