using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public static AudioManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
        }
    }

    // Update is called once per frame
    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
