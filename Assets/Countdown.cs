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
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            timer -= Time.deltaTime;
            text.text = Mathf.RoundToInt(timer).ToString();

            if (timer <= -0.5f)
            {
                timeIsRunning = false;
                end.Invoke();
            }
        }
    }
}
