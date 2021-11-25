using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Topping : MonoBehaviour
{
    protected bool isGettingSuckedIn;

    protected Rigidbody2D rigid;

    protected Transform startPoint;

    protected Transform endPoint;

    public int iceBarrier;

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (isGettingSuckedIn && !Aimer.hitIce && Straw.submerged)
        {   if (Vector3.Distance(startPoint.position, endPoint.position) <= 8)
            {
                float dist = Vector3.Distance(startPoint.position, endPoint.position);
                rigid.AddForce(Vector3.Normalize(endPoint.position - startPoint.position) * 60 / dist, ForceMode2D.Force);
            }
        }
    }

    protected void ToppingGettingSucked(Transform _startPoint, Transform _endPoint)
    {
        startPoint = _startPoint;
        endPoint = _endPoint;
        isGettingSuckedIn = true;
    }

    protected void FreezeTopping(Transform _topping)
    {
        _topping.GetChild(0).transform.gameObject.SetActive(false);
        _topping.GetChild(1).transform.gameObject.SetActive(true);
    }

    protected void DefrostTopping(Transform _topping)
    {
        _topping.GetChild(0).transform.gameObject.SetActive(true);
        _topping.GetChild(1).transform.gameObject.SetActive(false);
    }

    protected void SetChewValueAndDestroy(GameObject _topping)
    {
        //Decrease number of toppings by 1
        //Level.instance.numBobas--;
        //LevelManager.manager.UpdateToppingCount(-1);
        Debug.Log("Called");

        /*Aimer.chewValue += _chewValue;
        if (Aimer.chewValue > 100)
            Aimer.chewValue = 100;*/

        Aimer.suckCount++;

        _topping.SetActive(false);
        Destroy(_topping);
    }

    protected void StopToppingMovement(Topping topping)
    {
        topping.rigid.velocity = Vector3.zero;
        topping.rigid.angularVelocity = 0;
        topping.isGettingSuckedIn = false;
    }
}
