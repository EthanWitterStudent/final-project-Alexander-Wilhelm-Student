using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class UIScript : MonoBehaviour
{

    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Button[] towerButtons;
    
    [SerializeField] Transform[] gridButtons;
    [SerializeField] Image stonksPanel;

    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] float randomTextColor;
    [SerializeField] Image fadePanel;
    [SerializeField] float panelFadeTime;

    //[SerializeField] GameObject test;

    GameManager gameManager;

    Color fadecolor;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fadecolor = new Color(0, 0, 0, Time.deltaTime/panelFadeTime); //dont recalculate this
        UpdateMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        stonksPanel.fillAmount = gameManager.GetCashTimer()/gameManager.GetCashDelay();
    }

    public void setupTowerButton(int index, GameObject tower, Sprite image) {
        //Debug.Log($"SETTING TOWER BUTTON INDEX {index} TOWER {tower.name} IMAGE {image.name}");
        Button button = towerButtons[index];
        button.gameObject.SetActive(true);
        if (image != null) button.GetComponent<Image>().sprite = image;
        if (tower.GetComponent<Health>() != null) button.GetComponentInChildren<TextMeshProUGUI>().text = $"${tower.GetComponent<Health>().GetCashCost()}";
    }

    public void UpdateMoneyText() {
        moneyText.text = $"${gameManager.GetCash().ToString("D4")}";
    }

    public Vector3 GridButtonClick()
    {
        Transform tf = GetClosestButton(gridButtons); // get the screen position of the button we're clicking
        return tf.position;

        /* unused
        //Debug.Log("Screen point: " + tf.position);  
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(tf.position); //get its position in the world
        worldPos = new Vector3(worldPos.x, worldPos.y, 0); //snap to zero 
        //Debug.Log("World point: " + Camera.main.ScreenToWorldPoint(tf.position));
        return worldPos;
        //Instantiate(test, worldPos, Quaternion.identity); //test, later we use this to spawn towers
        */
    }


    Transform GetClosestButton(Transform[] buttons) //get the closest button (the one we're clicking)
    {                                               //i do this not by choice but because i can't be assed to spend my best years dragging shit in the inspector
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = Mouse.current.position.ReadValue();
        
        foreach (Transform t in buttons)
        {
            float dist = Vector2.Distance(t.position, currentPos);
            if (dist <= minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        
        return tMin;
    }

    public IEnumerator WinText() {
        winText.gameObject.SetActive(true);
        while (true) {
            winText.color = Color.white - new Color(Random.Range(0, randomTextColor), Random.Range(0, randomTextColor), Random.Range(0, randomTextColor), 0);
            yield return null;
        }
    }
    public IEnumerator FadePanel() {
            fadePanel.gameObject.SetActive(true);
            fadePanel.color = new Color(0, 0, 0, 0);
            while (fadePanel.color.a <= 1)
            {
                fadePanel.color += fadecolor;
                yield return null;
            }
        }

    /* from space defender, could be of use later
        put into ui script, let's not leave this here!!!!


        public IEnumerator FadeText(TextMeshProUGUI text, float fadeFactor) {

            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            yield return new WaitForSeconds(fadeHold);
            Debug.Log($"{text.color.r} {text.color.g} {text.color.b} {text.color.a}");
            while (text.color.a >= Mathf.Epsilon)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a - (fadeFactor*Time.deltaTime))); //INSANE!!!! WHY AM I DOING THIS
                yield return null;
            }

        }

        */
}
