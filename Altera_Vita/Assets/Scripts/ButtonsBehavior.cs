using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehavior : MonoBehaviour
{
    public Transition trans;

    public void NewGame()
    {
        trans.LoadNextLevel();
        
    }

    public void HowToPlay()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
