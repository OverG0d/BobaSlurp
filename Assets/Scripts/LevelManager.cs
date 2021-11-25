using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public int levelIndex;
    public GameObject currentLevel;
    public List<GameObject> levels = new List<GameObject>();
    public static LevelManager manager;
    private bool _objectivesCleared;
    public Straw levelStraw;
    public int points;
    public Text pointsText;

    private bool _isTransitioning;

    public bool levelActive = false;

    public Text toppingDisplay;

    public int toppingCount;

    public Transform[] targetTransforms;
    private int _levelPartIndex = -1;
    private float _currentVel;

    void Awake()
    {
        
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
        }
    }

    private void Start()
    {       
        if (levelIndex == 1)
        {
            points = 0;
            AddPoints(points);
        }   
        else
        {
            PlayerData data = SaveSystem.LoadPlayer();
            points = data.points;
            pointsText.text = points + "";
        }
        Invoke("MakeLevelActive", 1.0f);
    }

    private void Update()
    {
        //Vector3 cameraPos = Camera.main.transform.position;

        //float halfdistance = cameraPos.y - Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        //float cameraBottom = cameraPos.y - halfdistance;

        //float distance = Math.Abs(cameraBottom - targetTransforms[levelPartIndex].transform.position.y);
        if (levelActive)
        {
           // Debug.Log((toppingCount <= 0) + ", " + !_isTransitioning);
            if (toppingCount <= 0 && !_isTransitioning)
            {
                if (++_levelPartIndex == targetTransforms.Length) { _objectivesCleared = true; }
                else
                {
                    //_isTransitioning = true;
                    levelStraw.canControl = false;
                }
            }

            if (toppingCount <= 0)
            {
                points *= (int)Liquid.extraPoints;
                if (levelIndex == 1)
                    SaveSystem.SavePlayer(this);
                _objectivesCleared = false;
                levelActive = false;
                if (SceneManager.GetActiveScene().buildIndex != 0)
                Level.instance.NextLevelCoroutine();
                ScoreManager.instance.SubmitScoreToLeaderboard(points);
                toppingCount = 1;
            }

            if (_isTransitioning)
            {
                TransitionNextPart(ref _isTransitioning);
            }
        }

    }

    public void Restart()
    {
        //Aimer.chewValue = 0;
        Aimer.suckCount = 0;
        toppingCount = 0;
        SceneManager.LoadScene(levelIndex);
    }

    public void NextLevel()
    {
        Aimer.suckCount = 0;
        if (levelIndex == 2)
        {
            levelIndex = 0;
            SceneManager.LoadScene(0);
            return;
        }
        levelIndex++;
        SceneManager.LoadScene(manager.levelIndex);
    }

    public void UpdateToppingCount(int amount)
    {
        toppingCount += amount;
        toppingDisplay.text = toppingCount.ToString();
    }

    public void TransitionNextPart(ref bool isTransitioning)
    {

        Vector3 cameraPos = Camera.main.transform.position;

        float halfdistance = cameraPos.y - Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        float cameraBottom = cameraPos.y - halfdistance;
        float distance = 0;

        if (targetTransforms.Length > 0)
        {
            distance = cameraBottom - targetTransforms[_levelPartIndex].transform.position.y;
        }

        if (distance > 0.01f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
            Mathf.SmoothDamp(cameraPos.y, targetTransforms[_levelPartIndex].transform.position.y + halfdistance, ref _currentVel, 0.5f, 100.0f),
            Camera.main.transform.position.z);
        }
        else
        {
            isTransitioning = false;
            levelStraw.canControl = true;
        }
    }

    private void MakeLevelActive() 
    {
        levelActive = true;
    }

    public void AddPoints(int amount)
    {
        if (levelIndex > 0)
        {
            points += amount;
            pointsText.text = points + "";
        }
    }
}