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
    
    public void checkMaxTowers()  //expensive, but shouldn't be a problem
    {
        int count = 0;
        foreach (Toggle sel in towerSelectors)
        {
            if (sel.isOn)
            {
                count++;
            }
        }
        startButton.interactable = (count <= maxTowers && count > 0);
    }

    public void TowerSetup()
    {
        TowerInfo towerinfo = FindObjectOfType<TowerInfo>();
        foreach (Toggle sel in towerSelectors)
        {
            if (sel.isOn)
            {
                TowerButtonInfo tbInfo = sel.gameObject.GetComponentInParent<TowerButtonInfo>();
                if (tbInfo.tower != null) towerinfo.towers.Add(tbInfo.tower);
                if (tbInfo.towerImage != null) towerinfo.towerImgs.Add(tbInfo.towerImage);
            }
        }
        FindObjectOfType<LevelManager>().LoadGame();
    }
}
