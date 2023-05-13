using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent startTradeEvent;
    [HideInInspector] public UnityEvent finishTradeEvent;

    [HideInInspector] public UnityEvent tradeCanceledEvent;

    [HideInInspector] public UnityEvent tradeEvent;
    [HideInInspector] public UnityEvent onItemChanged;
    public static EventManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
