using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Cooldown 
{
    public enum Progress
    {
        Ready,
        Started,
        InProgress,
        Finished,
        
    }

    public Progress CurrentProgress = Progress.Ready;
    public float TimeLeft
    {
        get
        {
            return TimeLeft;
        }
    }
    public bool IsOnCooldown
    {
        get
        {
            return IsOnCooldown;
        }
    }

    public float Duration = 1.0f;

    private float _CurrentDuration = 0f;
    private bool _IsOnCooldown = false;

    private Coroutine _Coroutine;

    public void StartCooldown()
    {
        if (CurrentProgress is Progress.Started or Progress.InProgress)
            return;

        _Coroutine = CorountineHost.Instance.StartCoroutine(DoCooldown());
    }
    public void StopCooldown()
    {
        if (_Coroutine != null)
            CorountineHost.Instance.StopCoroutine(_Coroutine);

        _CurrentDuration = 0f;
        _IsOnCooldown = false;
        CurrentProgress = Progress.Ready;
    }
    IEnumerator DoCooldown()
    {
        CurrentProgress = Progress.Started;
        _CurrentDuration = Duration;
        _IsOnCooldown = true;

        while (_CurrentDuration > 0)
        {
            _CurrentDuration -= Time.deltaTime;
            CurrentProgress = Progress.InProgress;

            yield return null;
        }

        _CurrentDuration = 0f;
        _IsOnCooldown = false;

        CurrentProgress = Progress.Finished;
    }

    
}
