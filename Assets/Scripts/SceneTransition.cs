using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public Animator transition;

    public IEnumerator LoadLevel()
    {
        //transition.Play("Fade_End");
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);
        Debug.Log("LEVELCHANGE");
        SceneManager.LoadScene(LevelManager.manager.levelIndex);
    }
}
