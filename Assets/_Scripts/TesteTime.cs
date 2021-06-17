using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteTime : MonoBehaviour
{
    public static TesteTime instance;

    private float timeScale = 1;
    public float TimeScale
    {
        set
        {
            timeScale = value;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }

    public void SlowTimeShort(float timeSpeed, float duration)
    {
        timeSpeed =  Mathf.Clamp01(timeSpeed);

        timeScale = timeSpeed;

        StartCoroutine(ieAcellerateBack(timeSpeed, duration));
    }

    IEnumerator ieAcellerateBack(float timeSpeed, float duration)
    {
        //float  = 3;

        float timeDiff = 1 - timeSpeed;

        for (float i = 0; i < duration; i+= Time.unscaledDeltaTime)
        {
            timeScale = timeSpeed + (timeDiff * (i / duration));

            yield return null;
        }

        timeScale = 1;
    }
}
