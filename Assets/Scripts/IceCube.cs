using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    public enum Size { Small, Medium, Large };

    public Size size;
    public int hits;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;

    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
        stage1.SetActive(true);
    }

    public void Destroy()
    {
        if (hits == 3)
            Destroy(gameObject);
    }

    public void StageLevel(int stage)
    {
        if (stage == 1)
        {
            stage1.SetActive(false);
            stage2.SetActive(true);
        }
        else if (stage == 2)
        {
            stage1.SetActive(false);
            stage2.SetActive(false);
            stage3.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("TouchPoint"))
        {
            if (Aimer.suckedObjects.Count < 2 && !Aimer.hitIce)
            {
                Aimer.suckedObjects.Add(gameObject);
                Aimer.hitIce = true;
                hits++;
                Destroy();
                StageLevel(hits);
            }
        }
    }
}
