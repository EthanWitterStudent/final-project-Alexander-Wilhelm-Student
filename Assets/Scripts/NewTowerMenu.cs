using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTowerMenu : MonoBehaviour
{
    Toggle[] towerSelectors;
    [SerializeField] Button startButton;
    [SerializeField] int maxTowers;
    [SerializeField] GameObject towerInfoPrefab;
    List<GameObject> towerRoster;

    void Start()
    {   //Puts all selectors in a GameObject array
        towerSelectors = GameObject.FindObjectsOfType<Toggle>(); //the only toggles we're gonna have in that menu are the towers
    }


    /* public void CheckSelectedTowers()
     {   
         foreach(Toggle selector in towerSelectors)
         {
             TowerRoster(selector);
         }
         // MemeMaxEnforcer();
     }
 */
    public void checkMaxTowers()  //expensive, but shouldn't be a problem
    {
        int count = 0;
        foreach (Toggle sel in towerSelectors)
        {
            if (sel.isOn)
            {
                count++;
                Debug.Log(count);
            }
        }
        startButton.interactable = (count <= maxTowers && count > 0);
    }

    public void TowerSetup()
    {

        //TowerInfo towerinfo = Instantiate(towerInfoPrefab, Vector3.zero, Quaternion.identity).GetComponent<TowerInfo>();
        //DontDestroyOnLoad(towerinfo); //prepare it to be sent to game stage;

        TowerInfo towerinfo = FindObjectOfType<TowerInfo>();
        foreach (Toggle sel in towerSelectors)
        {
            if (sel.isOn)
            {
                Debug.Log(sel.name);
                TowerButtonInfo tbInfo = sel.gameObject.GetComponentInParent<TowerButtonInfo>();
                if (tbInfo.tower != null) towerinfo.towers.Add(tbInfo.tower);
                if (tbInfo.towerImage != null) towerinfo.towerImgs.Add(tbInfo.towerImage);
            }
        }
        FindObjectOfType<LevelManager>().LoadGame();
        
    }

    /* huh?????
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
    */
}
