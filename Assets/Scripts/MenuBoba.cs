using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoba : MonoBehaviour
{
    public int speed;
    public float killPoint;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        
        if(transform.GetComponent<RectTransform>().localPosition.y >= killPoint)
        {
            Destroy(gameObject);
        }
    }
}
