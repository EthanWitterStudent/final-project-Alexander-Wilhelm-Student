using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public int LevelSelected;

   public void MommyBasementSelected()
   {
        LevelSelected = 1;
   }
   //Add more methods a-la MommyBasementSelected for future levels.
   //Level 2 should have a LevelSelected value of 2, and so on
}
