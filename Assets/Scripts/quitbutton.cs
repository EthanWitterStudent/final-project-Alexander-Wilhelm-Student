using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitbutton : MonoBehaviour
{
    public void doExitGame() //doesn't work in WebGL
    {
        Application.Quit();
    }
}
