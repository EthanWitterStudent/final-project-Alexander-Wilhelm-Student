using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{   
    // [Header("Meme Selectors")]
    // [SerializeField] GameObject amogusSelect;
    // [SerializeField] GameObject gruSelect;
    // [SerializeField] GameObject pogSelect;
    // [SerializeField] GameObject sanicSelect;
    // [SerializeField] GameObject shrekSelect;
    // [SerializeField] GameObject trollfaceSelect;
    // [SerializeField] GameObject TBHSelect;
    // [SerializeField] GameObject pikachuSelect;
    // [SerializeField] GameObject knucklesSelect;
    // [SerializeField] GameObject keevesSelect;
    // [SerializeField] GameObject minionsSelect;
    // [SerializeField] GameObject morbiusSelect;
    // [SerializeField] GameObject chungusSelect;

    // GameObject[] towerSelectors = new GameObject[] {amogusSelect, };
    
    [SerializeField] Button startLevelButton;
    public List<GameObject> towerRoster;
    [SerializeField] int maxTowersSelected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Add way to cycle through the selectors looking for checked boxes
       MemeMaxEnforcer();
    }

    void MemeMaxEnforcer()
    {
        if (towerRoster.Count <= maxTowersSelected && towerRoster.Count > 0)
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
        Toggle toggle = selector.GetComponent<Toggle>();
        
        if(toggle.isOn == true)
        {
            GameObject link = selector.GetComponent<Select_MemeConnection>().towerLink;
            towerRoster.Add(link);
        }
        if(toggle.isOn == false)
        {
            GameObject link = selector.GetComponent<Select_MemeConnection>().towerLink;
            //check if the list contains the tower, only remove if present
            towerRoster.Remove(link);
        }
    }
}
