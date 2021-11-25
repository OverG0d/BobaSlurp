using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aimer : MonoBehaviour
{
    public Straw straw;
    public static int maxChewValue = 100;
    public static int suckCount = 0;
    public static bool hitIce = false;
    public static List<GameObject> suckedObjects = new List<GameObject>();
    //public Image image;
    //public Sprite slurpFace;
    //public Sprite nomFace;
    public Text text;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            //straw.reverseStraw();
            //SetSpriteColour();
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {

    }

    IEnumerator Straw()
    {
        straw.isSwiped = true;
        yield return new WaitForSeconds(1f);
        straw.isSwiped = false;
    }

    protected virtual void StrawCtrlOff()
    {
        straw.canControl = false;
        straw.canSlideDown = false;
    }

    protected virtual void StrawCtrlOn()
    {
        straw.canControl = true;
        straw.canSlideDown = true;
    }

    
}

#region Face Pic Code
//public static int chewValue = 0;

/*public void Stuck()
{
    if (straw.canControl == true && chewValue > 0)
    {
        StartCoroutine(Control());
    }

}

IEnumerator Control()
{
    StrawCtrlOff();

    while (chewValue > 0)
    {
        //chewValue -= 5;
        //SetSpriteColour();
        straw.canControl = false;
        yield return new WaitForSeconds(0.5f);
    }

    StrawCtrlOn();
}
public void SetSpriteColour()
{
    if (chewValue >= maxChewValue)
    {
        ChewFace100Pct();
    }
    else if (chewValue >= maxChewValue * 0.75f)
    {
        ChewFace75Pct();
    }
    else if (chewValue >= maxChewValue * 0.50f)
    {
        ChewFace50Pct();
    }
    else if (chewValue > 0)
    {
        ChewFaceStart();
    }
    else
    {
        ChewFaceIdle();
    }
}

private void ChewFace100Pct()
{
    image.color = new Color(1, 0, 0);
    straw.speed = 0f;
    image.overrideSprite = nomFace;
    chewValue = 100;
    straw.canControl = true;
    straw.canSlideDown = false;
}

private void ChewFace75Pct()
{
    image.color = new Color(1, 0.5f, 0);
    straw.speed = 2f;
    image.overrideSprite = slurpFace;
}

protected virtual void ChewFace50Pct()
{
    image.color = new Color(1, 0.75f, 0);
    image.overrideSprite = slurpFace;
}

private void ChewFaceStart()
{
    image.color = new Color(0.9f, 0.9f, 0);
    image.overrideSprite = slurpFace;
}

private void ChewFaceIdle()
{
    image.color = new Color(1f, 1f, 1f);
    image.overrideSprite = slurpFace;
}*/
#endregion 
