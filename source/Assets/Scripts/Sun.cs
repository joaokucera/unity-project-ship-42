using UnityEngine;
using System.Collections;

public enum DayPeriod
{
    Day,
    Night
}

public class Sun : MonoBehaviour
{
    private DayPeriod dayPeriod = DayPeriod.Day;

    private float maxRange = 200;
    private float minRange = 50;

    void Start()
    {
        light.range = maxRange;
    }

    void Update()
    {
        if (dayPeriod == DayPeriod.Day)
        {
            light.range -= Time.deltaTime;

            if (light.range <= minRange)
            {
                dayPeriod = DayPeriod.Night;
            }
        }
        else
        {
            light.range += Time.deltaTime;

            if (light.range >= maxRange)
            {
                dayPeriod = DayPeriod.Day;
            }
        }
    }
}
