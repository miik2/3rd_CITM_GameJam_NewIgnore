using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehavior : MonoBehaviour
{
    public Transition trans;

    public void NewGame()
    {
        trans.LoadNextLevel();
        
    }

    public void HowToPlay()
    {
        StartCoroutine(HowToPlayWait());
    }

    IEnumerator HowToPlayWait()
    {
        trans.NormalTransitionStart();
        yield return new WaitForSeconds(1f);
        GameObject.Find("Title").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("NG").GetComponent<Text>().enabled = false;
        GameObject.FindGameObjectWithTag("EX").GetComponent<Text>().enabled = false;
        GameObject.FindGameObjectWithTag("HTP").GetComponent<Text>().enabled = false;
        GameObject.Find("Controls").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("BK").GetComponent<Text>().enabled = true;
    }

    public void Back()
    {
        StartCoroutine(BackWait());
    }

    IEnumerator BackWait()
    {
        trans.NormalTransitionStart();
        yield return new WaitForSeconds(1f);
        GameObject.Find("Controls").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("BK").GetComponent<Text>().enabled = false;
        GameObject.Find("Title").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("NG").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("EX").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("HTP").GetComponent<Text>().enabled = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
