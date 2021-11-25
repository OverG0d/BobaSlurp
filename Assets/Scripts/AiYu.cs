using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiYu : Topping
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("MainCamera"))
        {
            LevelManager.manager.UpdateToppingCount(1);
        }

        if (col.CompareTag("TouchPoint"))
        {
            if (Aimer.suckedObjects.Count < 2 && Straw.submerged)
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
