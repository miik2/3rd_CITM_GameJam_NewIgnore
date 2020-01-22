using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        StartCoroutine(Starttitle());
    }

    IEnumerator Starttitle()
    {

        yield return new WaitForSeconds(1.5f);
        Animin();
    }

    public void Animout()
    {
        anim.SetBool("n", true);
    }

    public void Animin()
    {
        anim.SetBool("enter", true);
        anim.SetBool("n", false);
    }
}
