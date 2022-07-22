using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool isTimerOver = false;
    public Coroutine StartTimer(float timerDuration, float timerSpeed)
    {
        Coroutine newTimer = StartCoroutine(TimerCoroutine(timerDuration, timerSpeed));
        return newTimer;
    }

    IEnumerator TimerCoroutine(float duration, float speed)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentTime = duration;
        isTimerOver = true;
    }

    public bool IsTimerOver() => isTimerOver;

}
