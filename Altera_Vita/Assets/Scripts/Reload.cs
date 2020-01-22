using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Animator reload;
    

    public void ReloadAnim()
    {
        reload.SetBool("reload", true);
    }

    public void EndReloadAnim()
    {
        reload.SetBool("reload", false);
    }
}
