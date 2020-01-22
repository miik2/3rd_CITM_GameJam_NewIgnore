using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Animator reload;
    

    public void ReloadAnim()
    {
        reload.SetTrigger("reload");
    }
}
