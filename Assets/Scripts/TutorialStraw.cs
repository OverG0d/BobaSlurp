using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialStraw : Straw
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject slideDownArrow;
    public GameObject upArrow;
    public GameObject chewArrow;
    public GameObject chewBox;
    public GameObject chewTextBox;
    public GameObject skipTutorialButton;
    public GameObject textBox;
    public Text controlText;

    bool tutorialDone = false;

    protected override void Start()
    {
        base.Start();
        if (LevelManager.manager.levelIndex == 1)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
    }

    protected override void LeftRightSwipeActions(Touch touch)
    {       
        base.LeftRightSwipeActions(touch);
        if (!tutorialDone)
            TutorialPhase2();
    }

    protected override void DownSwipeActions()
    {
        base.DownSwipeActions();
        if (!tutorialDone)
            TutorialPhase3();
    }

    protected override void UpSwipeActions()
    {
        base.UpSwipeActions();
        if (!tutorialDone)
            TutorialPhase4();
    }

    private void TutorialPhase2()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        slideDownArrow.SetActive(true);
    }

    private void TutorialPhase3()
    {
        slideDownArrow.SetActive(false);
        upArrow.SetActive(true);
    }

    private void TutorialPhase4()
    {
       upArrow.SetActive(false);
       tutorialDone = true;
    }
}
