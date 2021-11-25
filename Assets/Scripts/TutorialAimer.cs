using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAimer : Aimer
{
    protected override void StrawCtrlOff()
    {
        base.StrawCtrlOff();
        TutorialStraw tutorialStraw = (TutorialStraw)straw;
        tutorialStraw.chewBox.SetActive(false);
        tutorialStraw.chewTextBox.SetActive(true);
        tutorialStraw.upArrow.SetActive(true);
        tutorialStraw.textBox.SetActive(false);
        tutorialStraw.controlText.text = "";
    }

    protected override void StrawCtrlOn()
    {
        base.StrawCtrlOn();
        TutorialStraw tutorialStraw = (TutorialStraw)straw;       
        tutorialStraw.chewTextBox.SetActive(false);
        tutorialStraw.upArrow.SetActive(false);
        tutorialStraw.textBox.SetActive(false);
        tutorialStraw.skipTutorialButton.SetActive(false);
        tutorialStraw.controlText.text = "";
    }

}

#region Face Pic Code
/*protected override void ChewFace50Pct()
    {
        //base.ChewFace50Pct();
        TutorialStraw tutorialStraw = (TutorialStraw)straw;
        if (tutorialStraw.dialogIndex == 2 && chewValue > 50)
        {
            tutorialStraw.upArrow.SetActive(true);
            tutorialStraw.chewBox.SetActive(true);
            tutorialStraw.controlText.text = "Chew";
            tutorialStraw.dialogIndex++;
        }
    }*/
#endregion
