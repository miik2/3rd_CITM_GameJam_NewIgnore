using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Animator transition;

    public float transitiontime = 1f;

    void Start()
    {
        StartCoroutine(DeactivateCanvas());
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void NormalTransitionStart()
    {
        StartCoroutine(NormalTransition());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        GameObject go = GameObject.FindGameObjectWithTag("ima");
        Image im = go.GetComponent<Image>();
        im.enabled = true;
        transition.SetBool("m", true);
        Debug.Log(transition.GetBool("m"));
        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator DeactivateCanvas()
    {
        yield return new WaitForSeconds(transitiontime);
        GameObject go = GameObject.FindGameObjectWithTag("ima");
        Image im = go.GetComponent<Image>();
        im.enabled = false;
    }

    IEnumerator NormalTransition()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ima");
        Image im = go.GetComponent<Image>();
        im.enabled = true;
        transition.SetBool("m", true);
        yield return new WaitForSeconds(transitiontime);
        transition.SetBool("m", false);
        yield return new WaitForSeconds(transitiontime);
        im.enabled = false;
    }
}
