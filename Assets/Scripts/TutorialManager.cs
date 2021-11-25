using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
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

    public int dialogIndex; //controls the help text dialog for the tutorial

    private void Start()
    {
        dialogIndex = 0;
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        textBox.SetActive(true);
        skipTutorialButton.SetActive(true);
        controlText.text = "Move";
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Phone Controls
            if (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y >= -2.5)
            {
                /*horizontalMovement = joyStick.Horizontal * speed;
                angle = Mathf.Clamp(angle, -12f, 12f);
                angle += horizontalMovement;
                strawBase.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                text.text = strawBase.transform.rotation.ToString();*/

                if (dialogIndex == 0)
                {
                    
                }

            }
        }
    }

    private void TutorialPhase0()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        slideDownArrow.SetActive(true);
        controlText.text = "Slide Down";
        dialogIndex++;
    }

    private void TutorialPhase1()
    {
        slideDownArrow.SetActive(false);
        dialogIndex++;
    }

    private void TutorialPhase2()
    {
        upArrow.SetActive(true);
        chewBox.SetActive(true);
        controlText.text = "Chew";
        dialogIndex++;
    }

    private void TutorialPhase3()
    {
        chewBox.SetActive(false);
        chewTextBox.SetActive(true);
        upArrow.SetActive(true);
        textBox.SetActive(false);
        controlText.text = "";
        dialogIndex++;
    }

    private void TutorialPhase4()
    {
        chewTextBox.SetActive(false);
        slideDownArrow.SetActive(false);
        upArrow.SetActive(false);
        textBox.SetActive(false);
        skipTutorialButton.SetActive(false);
        controlText.text = "";
        dialogIndex++;
    }
}
