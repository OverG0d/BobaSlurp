using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBobas : MonoBehaviour
{
    public GameObject bobaBall;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MenuBoba", 0.0f, 5f);
    }

    void MenuBoba()
    {
        int randomNum = Random.Range(0, 4);

        GameObject clone = (GameObject)Instantiate(bobaBall, spawnPoints[randomNum].transform.position, spawnPoints[randomNum].transform.rotation);
        clone.transform.parent = canvas.transform.GetChild(0);
    }

    
}
