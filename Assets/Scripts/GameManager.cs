using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject diver;
    public event Action OnExit;

    private AnimationEventHandler eventHandler;

    private void Exit()
    {
        OnExit?.Invoke();
    }

    private void Awake()
    {
        eventHandler = diver.GetComponent<AnimationEventHandler>();
    }

    private void OnEnable()
    {
        eventHandler.OnFinish += Exit;
    }

    private void OnDisable()
    {
        eventHandler.OnFinish -= Exit;
    }
}
