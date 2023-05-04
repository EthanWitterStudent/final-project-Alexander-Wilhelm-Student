using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{   
    GameObject[] towerSelectors;
    [SerializeField] Button startLevelButton;
    //Needs to be public, as the list determines what the player can use
    public List<GameObject> towerRoster;
    [SerializeField] int maxTowersSelectable;

    void Start()
    {   //Puts all selectors in a GameObject array
        towerSelectors = GameObject.FindGameObjectsWithTag("Selector");
        // startLevelButton.interactable = false;
        //Trying to loop through the array to check for checks
        foreach(GameObject selector in towerSelectors)
        {
            Debug.Log(selector);
        }
    }

    private void Update() 
    {   //Might kill performance...
        //Update: almost did :D
        CheckSelectedTowers();
    }

    public void CheckSelectedTowers()
    {   //Trying to loop through the array to check for checks
        foreach(GameObject selector in towerSelectors)
        {
            TowerRoster(selector);
        }
        // MemeMaxEnforcer();
    }

    void MemeMaxEnforcer()
    {
        if (towerRoster.Count <= maxTowersSelectable && towerRoster.Count > 0)
        {
           startLevelButton.interactable = true;
        } 
        else
        {
            startLevelButton.interactable = false;
        }
    }

    void TowerRoster(GameObject selector)
    {
        Toggle toggle = selector.GetComponentInChildren<Toggle>();
        
        if(toggle.isOn == true)
        {
            //Adds the tower to the list
            GameObject link = selector.GetComponent<Select_MemeConnection>().towerLink;
           foreach (GameObject towers in towerRoster)
                {
                    if (towers == link)
                    {
                        return;
                    }
                }
            towerRoster.Add(link);
        }
        if(toggle.isOn == false)
        {   //If the toggle is off, checks to see if the tower is in the list, and removes it if present
            GameObject link = selector.GetComponent<Select_MemeConnection>().towerLink;
                foreach (GameObject towers in towerRoster)
                {
                    if (towers == link)
                    {
                        towerRoster.Remove(link);
                    }
                }
        }
    }
}
