using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    private TextMeshProUGUI text;
    
    public UnityEvent end;

    private bool timeIsRunning = true;

    [SerializeField] private float timer = 4f;
    
    private float timeOffset = 0;
    private float tempoTimer;
    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        tempoTimer = timer;
        timeIsRunning = true;
    }

    void Update()
    {
        if (timeIsRunning)
        {
            tempoTimer -= Time.deltaTime;
            text.text = Mathf.RoundToInt(tempoTimer).ToString();

            if (tempoTimer <= -0.5f)
            {
                timeIsRunning = false;
                end.Invoke();
            }
        }
    }
}
