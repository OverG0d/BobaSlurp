using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBean : Topping
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
        if(col.gameObject.layer == 10 && iceBarrier == 0)
        {
            FreezeTopping(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("TouchPoint"))
        {
            if (iceBarrier == 0)
            {
                ToppingGettingSucked(transform, col.gameObject.transform.parent.GetChild(0));
            }
        }

        if (col.CompareTag("EndPoint"))
        {
            if (iceBarrier == 1)
            {
                DefrostTopping(transform);
                return;
            }

            if (Aimer.suckCount < 2)
            {
                SetChewValueAndDestroy(gameObject);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
                if (colliders.Length > 0)
                {
                    for(int i = 0; i < colliders.Length;i++)
                    {
                        if (colliders[i].gameObject.layer == 9)
                            Destroy(colliders[i].gameObject); 
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("TouchPoint"))
        {
            isGettingSuckedIn = false;
        }
    }
}
