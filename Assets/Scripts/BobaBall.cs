using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BobaBall : Topping
{
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();  
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 10 && iceBarrier == 0)
        {
            iceBarrier++;
            FreezeTopping(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("MainCamera"))
        {
            LevelManager.manager.UpdateToppingCount(1);
        }

        if (col.CompareTag("TouchPoint"))
        {
            if (iceBarrier == 1 && Aimer.suckedObjects.Count < 2 && Straw.submerged)
            {
                iceBarrier--;
                DefrostTopping(transform);
                Aimer.suckedObjects.Add(gameObject);
                return;
            }

            if (Aimer.suckedObjects.Count < 2 && iceBarrier == 0 && Straw.submerged)
            {               
                Aimer.suckedObjects.Add(gameObject);
                ToppingGettingSucked(transform, col.gameObject.transform.parent.GetChild(0));
            }
        }

        if (col.CompareTag("EndPoint"))
        {
            if (Aimer.suckCount < 2)
            {
                SetChewValueAndDestroy(gameObject);
                LevelManager.manager.AddPoints(5);
            }            
            else
            {
                //StopToppingMovement(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("MainCamera"))
        {
            LevelManager.manager.UpdateToppingCount(-1);
        }

        if (col.CompareTag("TouchPoint"))
        {
            isGettingSuckedIn = false;
        }
    }
}
