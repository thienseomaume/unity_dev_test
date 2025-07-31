using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    private static EventCenter _instance;
    public Action<bool> _nearBallAction;
    public Action _kick;
    public Action _autoKick;
    public Action _reset;
    public static EventCenter Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnNearBall(bool isNear)
    {
        _nearBallAction?.Invoke(isNear);
    }
    public void OnKick()
    {
        _kick?.Invoke();
    }
    public void OnAutoKick()
    {
        _autoKick?.Invoke();
    }
    public void OnReset()
    {
        _reset?.Invoke();
    }
}
