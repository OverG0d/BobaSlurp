using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Diagnostics;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Level : MonoBehaviour
{
    public GameObject bobaBall, ice, redBean, aiYu;
    //public ArrayLayout mainTopping;

    public static Level instance;

    //private int maxBobas;

    public int spawnHeightOffset;

    //public GameObject platform;

    //public GameObject borderL;

    //public GameObject borderR;

    public bool raising;

    private int rowNumber;

    public float yGap = 0.15f;

    public float borderGap = 0.01f;

    private bool raise1, raise2, raise3;

    private float initPlatY = 0;

    public Animator animation;

    public Text displayText;

    private Dictionary<char, GameObject> dictToppings;

    //public int numBobas
    //{
    //    get { return bobaCount; }

    //    set
    //    {
    //        if (value <= 0)
    //        {
    //            this.bobaCount = 0;
    //            //LevelManager.manager.NextLevel();
    //            //Aimer.chewValue = 0;
                
    //        }
    //        else
    //            this.bobaCount = value;

    //        //if (value <= 0.40 * maxBobas && !raise3)
    //        //{
    //        //    raise3 = true;
    //        //    raising = true;
    //        //}
    //        //else if (value <= 0.60 * maxBobas && !raise2)
    //        //{
    //        //    raise2 = true;
    //        //    raising = true;
    //        //}
    //        //else if (value <= 0.80 * maxBobas && !raise1)
    //        //{
    //        //    raise1 = true;
    //        //    raising = true;
    //        //}
    //    }
    //}

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        InitToppingsDict();
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnFromTxt(SceneManager.GetActiveScene().name);
        //maxBobas = numBobas;
    }

    private void InitToppingsDict()
    { 
        dictToppings = new Dictionary<char, GameObject>();
        dictToppings.Add('b', bobaBall);
        dictToppings.Add('x', ice);
        dictToppings.Add('r', redBean);
        dictToppings.Add('a', aiYu);
    }

    IEnumerator Raise()
    {
        raising = true;
        yield return new WaitForSeconds(2f);
        raising = false;
    }

    void spawnFromTxt(string file_name)
    {
        TextAsset level = Resources.Load<TextAsset>(file_name);

        Debug.Log(file_name);

        StreamReader in_stream = new StreamReader(new MemoryStream(level.bytes));


        int j = 0;

        while (!in_stream.EndOfStream)
        {
            string in_line = in_stream.ReadLine();

            //use dictionary instead of long list of if-statements
            for (int i = 0; i < in_line.Length; i++)
            {
                if (dictToppings.ContainsKey(in_line[i]))
                { 
                    Vector3 spawnPos = new Vector3(-2.25f + (bobaBall.transform.localScale.x * 0.5f) + i * bobaBall.transform.localScale.x, -j * (bobaBall.transform.localScale.y + yGap) + spawnHeightOffset, 0);
                    if (!(j % 2 == 1)) spawnPos.x += 0.25f;
                    Instantiate(dictToppings[in_line[i]], spawnPos, Quaternion.identity);
                    //LevelManager.manager.toppingCount++;
                }
            }
            //----------------------------------------------------//
            j++;
        }

        rowNumber = j;
        in_stream.Close();
    }

    public void NextLevelCoroutine()
    {
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        if (LevelManager.manager.toppingCount <= 0)
        {
            float randNum = UnityEngine.Random.Range(1, 30);
            if (randNum >= 1 && randNum <= 10)
            {
                displayText.text = "Sweet!";
            }
            else if (randNum >= 11 && randNum <= 20)
            {
                displayText.text = "Yummy!";
            }
            else if (randNum >= 21 && randNum <= 30)
            {
                displayText.text = "Delicious!";
            }
            animation.transform.parent.gameObject.SetActive(true);
            animation.Play("Boba_Up");
            yield return new WaitForSeconds(2);
            //LevelManager.manager.AddPoints((int)(Liquid.liquidAmount * 100));
            LevelManager.manager.NextLevel();
        }
    }
}

#region OLD_CODE
//void Update()
//{
//    if (raising)
//    {
//        platform.transform.Translate(0, 0.2f * Time.deltaTime, 0);

//        if (platform.transform.position.y >= -5.05 && raise3)
//        {
//            raising = false;
//            float y = platform.transform.position.y;
//            y = -5.05f;
//        }
//        else if (platform.transform.position.y >= (initPlatY - (initPlatY - -5.05f) * 2 / 3) && raise2 && !raise3)
//        {
//            raising = false;
//        }
//        else if (platform.transform.position.y >= (initPlatY - (initPlatY - -5.05f) / 3) && raise1 && !(raise2 || raise3))
//        {
//            raising = false;
//        }

//    }
//}

//if (rowNumber > 6)
//    platform.transform.position = new Vector2(platform.transform.position.x, -5.05f - (rowNumber - 6) * bobaBall.transform.localScale.y);
//else
//    platform.transform.position = new Vector2(platform.transform.position.x, -5.05f);

//initPlatY = platform.transform.position.y;
//borderL.transform.position = new Vector2(borderL.transform.position.x - borderGap,  (bobaBall.transform.localScale.y * rowNumber) / 2);
//borderR.transform.position = new Vector2(borderR.transform.position.x + borderGap, (bobaBall.transform.localScale.y * rowNumber) / 2);
//borderL.transform.localScale = new Vector2(1, bobaBall.transform.localScale.y * rowNumber);
//borderR.transform.localScale = new Vector2(1, bobaBall.transform.localScale.y * rowNumber);
#endregion