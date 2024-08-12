using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class MiniQuestController : MonoBehaviour
{
    public int TotalScore;
    public int MaxScore;

    public UnityEvent WhenQuestDone;


    public void AddScore(int scoreAdded)
    {
        TotalScore += scoreAdded;
        CheckScore();
    }

    public void CheckScore()
    {
        if (TotalScore == MaxScore)
        {
            WhenQuestDone.Invoke();
        }
    }
}
