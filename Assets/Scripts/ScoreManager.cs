using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public void Awake()
    {
        TestSingleton();
    }

    private void TestSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubmitScoreToLeaderboard(int score)
    {
        Leaderboards.MyGameHighScore.SubmitScore(score);
    }

    

}
