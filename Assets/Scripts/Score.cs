using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private static Score instance;

    private Score() { }

    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }
            return instance;
        }
    }

    public DateTime timeStart = DateTime.Now;
    public int nbCycle = 1;

    public void Reset()
    {
        timeStart = DateTime.Now;
        nbCycle = 1;
    }

    public string FormattedTime()
    {
        TimeSpan t = DateTime.Now - timeStart;

        string answer = string.Format("{0:D2}m:{1:D2}s",
                t.Minutes,
                t.Seconds);

        return answer;
    }
}
