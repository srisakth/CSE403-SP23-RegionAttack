using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // Time limit in seconds
    public float _timeLimit;
    private float _timeElapsed;
    private bool _enabled;
    public bool _showMin;

    public TMP_Text _displayText;

    // Function to call when done
    public UnityEvent _timeUpEvent;

    public void ResetTimer()
    {
        _timeElapsed = 0;
    }

    // Starts the timer
    public void StartTimer()
    {
        _enabled = true;
    }

    public void StopTimer()
    {
        _enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled)
        {
            _timeElapsed += Time.deltaTime;
            int remaining = Mathf.CeilToInt(_timeLimit - _timeElapsed);
            int minute = remaining / 60;
            int second = remaining % 60;
            _displayText.text = _showMin ? $"{minute:D2} : {second:D2}" : $"{second}";

            if (_timeElapsed > _timeLimit) 
            {
                _enabled = false;
                _timeUpEvent.Invoke();
            }
        }
    }
}
