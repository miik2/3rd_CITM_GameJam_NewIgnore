using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehavior : MonoBehaviour
{
    public Transition trans;
    public TitleAnim titleanim;

    public void NewGame()
    {
        StartCoroutine(NewGameWait());   
    }

    IEnumerator NewGameWait()
    {
        titleanim.Animout();
        yield return new WaitForSeconds(2f);
        trans.LoadNextLevel();
    }

    public void HowToPlay()
    {
        StartCoroutine(HowToPlayWait());
    }

    IEnumerator HowToPlayWait()
    {
        titleanim.Animout();
        yield return new WaitForSeconds(2f);
        trans.NormalTransitionStart();
        yield return new WaitForSeconds(1f);
        //GameObject.Find("Title").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("NG").GetComponent<Text>().enabled = false;
        GameObject.FindGameObjectWithTag("Altera").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("ViTA").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("Line1").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("Line2").GetComponent<Image>().enabled = false;
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
        //GameObject.Find("Title").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("Altera").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("ViTA").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("Line1").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("Line2").GetComponent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("NG").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("EX").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("HTP").GetComponent<Text>().enabled = true;
        titleanim.Animin();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
