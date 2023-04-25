using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Transform[] gridButtons;

    [SerializeField] GameObject test;

    GameManager gameManager;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void GridButtonClick()
    {
        Transform tf = GetClosestButton(gridButtons); // get the screen position of the button we're clicking
        Debug.Log("Screen point: " + tf.position);  
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(tf.position); //get its position in the world
        worldPos = new Vector3(worldPos.x, worldPos.y, 0); //snap to zero 
        Debug.Log("World point: " + Camera.main.ScreenToWorldPoint(tf.position));

        Instantiate(test, worldPos, Quaternion.identity); //test, later we use this to spawn towers

    }


    Transform GetClosestButton(Transform[] buttons) //get the closest button (the one we're clicking)
    {                                               //i do this not by choice but because i can't be assed to spend my best years dragging shit in the inspector
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = Mouse.current.position.ReadValue();
        foreach (Transform t in buttons)
        {
            float dist = Vector2.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

}
