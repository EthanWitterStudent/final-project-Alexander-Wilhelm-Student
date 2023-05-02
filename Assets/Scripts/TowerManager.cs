using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] GameObject[] towers;
    [SerializeField] float boxCastSize;
    [SerializeField] BoxCollider2D towerCheck;

    [SerializeField] AudioClip placeFailSound;
    AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower() {
        Vector3 pos = FindObjectOfType<UIScript>().GridButtonClick();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos); //get its position in the world
        worldPos = new Vector3(worldPos.x, worldPos.y, 0); //snap to zero 
        Debug.Log("World point: " + worldPos);
        BoxCollider2D check = Instantiate(towerCheck, worldPos, Quaternion.identity);
        ContactFilter2D filter = new ContactFilter2D(); filter.SetLayerMask(LayerMask.GetMask("Tower"));
        //List<Collider2D> hits = new List<Collider2D>();
        if (check.OverlapCollider(filter, new List<Collider2D>()) > 0) {    //todo: is this a memory leak?
            
        } else Instantiate(towers[0], worldPos, Quaternion.identity);
        
        Destroy(check.gameObject);

        //BoxCollider2D checkCollider = Instantiate(towerCheck, pos, Quaternion.identity);
        //check if tower is there already
        
    }
}
