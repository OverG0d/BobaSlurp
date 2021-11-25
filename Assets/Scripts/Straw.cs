using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Straw : MonoBehaviour
{
    public Transform strawBase;
    public Transform pivot;
    public bool isRotateLeft;
    public bool isRotateRight;

    private Vector3 _touchDragStart;   //First touch position
    private Vector3 _touchDragEnd;   //Last touch position
    private float _minDragDist;  //minimum distance for a swipe to be registered
    public float moveTimer;
    float _yPos;
    public bool isTimerOn;
    public bool isSwiped;
    float _adjustedStabSpeed;
    public float stabSpeed;
    public GameObject aimerObj;
    public bool isTouched;
    public Joystick joyStick;
    public float speed;
    public float rotateSpeed;
    float _xPos;
    public bool canControl;
    public bool buttonCtrlsOn;
    public bool canSlideDown;
    public static bool submerged;
    public float minMovementX;
    public float maxMovementX;
    public int level;
    public int points;
    Touch touch;

    //public float minMovementY;
    //public float maxMovementY;
    float adjustSpeed;
    Camera cam;

    protected virtual void Start()
    {
        aimerObj = transform.Find("Aimer").gameObject;
        isSwiped = false;
        isRotateLeft = true;
        isRotateRight = true;
        canSlideDown = true;
        _minDragDist = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
        //aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(Control());
        cam = Camera.main;
        Aimer.suckCount = 0;
        Aimer.suckedObjects.Clear();
        submerged = false;
        //submerged = false;
    }

    // Update is called once per frame
    void Update()
    {
        // PC Controls
        if (buttonCtrlsOn && canControl)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && isRotateRight && !isTimerOn)
            {
                if (strawBase.transform.rotation.z < 0.25f)
                    strawBase.transform.Rotate(0, 0, rotateSpeed);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && isRotateLeft && !isTimerOn)
            {
                if (strawBase.transform.rotation.z > -0.25f)
                    strawBase.transform.Rotate(0, 0, -rotateSpeed);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!isSwiped)
                {
                    if (!submerged)
                    {
                        aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
                        submerged = true;
                        //isSwiped = true;
                        //isTimerOn = true;
                    }
                    else
                    {
                        aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                        submerged = false;
                        Aimer.suckCount = 0;
                        Aimer.suckedObjects.Clear();
                    }
                }
                
            }
        }

        if (Input.touchCount > 0) // user is touching the screen with a single touch
        {
            var touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                _touchDragStart = touch.position;
                _touchDragEnd = touch.position;

                var rayPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                var rayHit = Physics2D.Raycast(rayPos, (Input.GetTouch(0).position));

                if (rayHit.collider.name == "TouchPoint")
                {
                    if (!submerged && !isSwiped)
                    {
                        //isSwiped = true;
                        //isTimerOn = true;
                        //isTouched = true;
                        aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && !submerged) // update the last position based on where they moved
            {
                LeftRightSwipeActions(touch);
            }
            else if (touch.phase == TouchPhase.Ended && canControl) //check if the finger is removed from the screen
            {
                _touchDragEnd = touch.position;  //last touch position. Omitted if you use list

                if (Mathf.Abs(_touchDragEnd.x - _touchDragStart.x) > _minDragDist || Mathf.Abs(_touchDragEnd.y - _touchDragStart.y) > _minDragDist && canSlideDown)
                {
                    if (_touchDragEnd.y > _touchDragStart.y)  //If the movement was up
                    {                      
                        UpSwipeActions();                     
                    }
                    else
                    {
                        DownSwipeActions();
                    }
                }
            }
        }

        if (submerged)
        {
            //lowering straw
            var pivotTargetY = -3f;
            
            //adjustSpeed += -stabSpeed * Time.deltaTime;
            if (pivot.localPosition.y >= pivotTargetY)
            {
                AdjustStrawHeight(pivotTargetY);
            }
        }
        else
        {
            //raising straw
            var pivotTargetY = 3f;
            if (pivot.localPosition.y <= pivotTargetY)
            {
                AdjustStrawHeight(pivotTargetY);                
            }
        }
    }

    private void AdjustStrawHeight(float targetY)
    {
        //_adjustedStabSpeed += speed;

        _yPos = Mathf.Lerp(pivot.localPosition.y, targetY, Mathf.SmoothStep(0f, 5f, stabSpeed * Time.deltaTime));
        pivot.localPosition = new Vector3(pivot.localPosition.x, _yPos, 0);
    }

    protected virtual void LeftRightSwipeActions(Touch touch)
    {
        _touchDragEnd = touch.position;
        Vector3 newPos = cam.ScreenToWorldPoint(_touchDragEnd);
        //_yPos = Mathf.Lerp(pivot.localPosition.y, newPos.y - 2, Mathf.SmoothStep(0.0f, 1f, 1));
        //_yPos = Mathf.Clamp(_yPos, minMovementY, maxMovementY);
        _xPos = Mathf.Lerp(pivot.localPosition.x, newPos.x, Mathf.SmoothStep(0.0f, 1f, 1));
        _xPos = Mathf.Clamp(_xPos, minMovementX, maxMovementX);
        pivot.localPosition = new Vector3(_xPos, pivot.localPosition.y, 0);
    }

    protected virtual void UpSwipeActions()
    {
        Aimer.suckCount = 0;
        Aimer.suckedObjects.Clear();
        Aimer.hitIce = false;
        //Up swipe
        //text.text = "Up Swipe";
        _adjustedStabSpeed = 0;
        submerged = false;

        strawBase.transform.rotation = Quaternion.identity;
    }

    protected virtual void DownSwipeActions()
    {
        _adjustedStabSpeed = 0;
        submerged = true;

        if (canControl)
        {
            if (!submerged && !isSwiped)
            {
                //isSwiped = true;
                //isTimerOn = true;
                //isTouched = true;
                //canControl = false;
                //aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void reverseStraw()
    {
        stabSpeed = -Mathf.Abs(stabSpeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("LeftWall"))
        {
            isRotateLeft = false;
        }

        if (col.CompareTag("RightWall"))
        {
            isRotateRight = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("LeftWall"))
        {
            isRotateLeft = true;
        }


        if (col.CompareTag("RightWall"))
        {
            isRotateRight = true;
        }
    }

    IEnumerator Control()
    {
        canControl = false;
        yield return new WaitForSeconds(2f);
        canControl = true;
    }


    public void nextLevel()
    {
        LevelManager.manager.NextLevel();

    }

    //SaveSystem.SavePlayer(this)

    //PlayerData data = SaveSystem.LoadPlayer()
    //level = data.level
}

#region OLDCODE
/*public void Stab()
  {
      if (canControl)
      {
          if (!isTimerOn && !swiped)
          {
              swiped = true;
              isTimerOn = true;
              touched = true;
              canControl = false;
              aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;


          }
      }
  }*/

/*Rotate to finger touch
 * Vector2 test = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(test, (Input.GetTouch(0).position));

                if (hit.collider.name == "TouchPoint")
                {
                    //angle = Mathf.Clamp(angle, -15f, 15f);
                    //strawBase.transform.rotation = Quaternion.Euler(0f, 0f, angle);
}*/


/*Touch
 * if (Input.touchCount > 0)
       {
           Touch touch = Input.GetTouch(0);
           if (touch.position.x < Screen.width / 2 && rotateLeft)
           {
               text.text = "Left click" + pivot.transform.eulerAngles.z;
               pivot.transform.Rotate(0, 0, -1f);
           }
           else if (touch.position.x > Screen.width / 2 && rotateRight)
           {
               pivot.transform.Rotate(0, 0, 1f);
           }
       }*/

/*if (moveTimer <= 0)
   {          
       Aimer.suckCount = 0;
       //stabSpeed = Mathf.Abs(stabSpeed);
       aimerObj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
       aimerObj.GetComponent<Aimer>().hitIceCube = false;
       moveTimer = 2;
       _adjustedStabSpeed = 0;
       isTimerOn = false;
       isSwiped = false;
       isTouched = false;
       if (Aimer.chewValue < Aimer.maxChewValue)
           canControl = true;
   }*/


//REFERENCE LINE: _touchDragEnd = touch.position;  


//moving = false;
//Check if drag distance is greater than 20% of the screen height
/*if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
{//It's a drag
 //check if the drag is vertical or horizontal
    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
    {   //If the horizontal movement is greater than the vertical movement...
        if ((lp.x > fp.x))  //If the movement was to the right)
        {   //Right swipe
            if (rotateRight && !isTimerOn)
            {
                //text.text = "Right Swipe";
                strawBase.transform.Rotate(0, 0, 2f);
            }
        }
        else
        {   //Left swipe
            if (rotateLeft && !isTimerOn)
            {
                //text.text = "Left Swipe";
                strawBase.transform.Rotate(0, 0, -2f);
            }
        }
    }*/


/*horizontalMovement = joyStick.Horizontal * speed;
            angle = Mathf.Clamp(angle, -12f, 12f);
            angle += horizontalMovement;
            strawBase.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            text.text = strawBase.transform.rotation.ToString();*/

/*var dir = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - strawBase.transform.position;
var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
angle -= 270;
angle = Mathf.Clamp(angle, -380f, -340f);
strawBase.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward * speed);*/
//Phone Controls
/*if (submerged && Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y >= -2.5)
{


}*/
#endregion