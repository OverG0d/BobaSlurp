using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Liquid : MonoBehaviour
{
    public Straw straw;
    public Transform liquidPivot;

    public float speed;

    public Animator animation;

    public Text displayText;

    public bool done;

    public static float extraPoints;

    void Start()
    {
        done = false;
        extraPoints = 180;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<SpriteRenderer>().bounds.size + "/" + Camera.main.WorldToScreenPoint(liquidPivot.position));
        if (Straw.submerged)
        {
            LowerLiquid();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndPoint"))
        {
            //straw.submerged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EndPoint"))
        {
            //straw.submerged = false;
        }
    }

    private void LowerLiquid()
    {
        float newY = liquidPivot.position.y;
        Vector3 waterLevel = Camera.main.WorldToScreenPoint(liquidPivot.position);
        newY -= speed * Time.deltaTime;
        liquidPivot.position = new Vector2(liquidPivot.position.x, newY);
        if (waterLevel.y <= 0 && !done)
        {
            done = true;
            ScoreManager.instance.SubmitScoreToLeaderboard(LevelManager.manager.points);
            StartCoroutine(Restart());
        }
        extraPoints -= 5 * Time.deltaTime;
    }

    IEnumerator Restart()
    {
        //Aimer.chewValue = 0;
        if (LevelManager.manager.toppingCount > 0)
        {
            float randNum = Random.Range(1, 30);
            if (randNum >= 1 && randNum <= 10)
            {
                displayText.text = "Oof!";
            }
            else if (randNum >= 11 && randNum <= 20)
            {
                displayText.text = "Yikes!";
            }
            else if (randNum >= 21 && randNum <= 30)
            {
                displayText.text = "Sad...";
            }

            animation.Play("Boba_Up");
            yield return new WaitForSeconds(2);
            LevelManager.manager.Restart();
        }
    }
}
