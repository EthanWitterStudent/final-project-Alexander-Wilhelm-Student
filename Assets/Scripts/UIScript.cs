using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] Image sliderFillArea;

    [SerializeField] Color goodHealthColor;
    [SerializeField] Color badHealthColor;
    

    [SerializeField] float badHealthValue;

    GameManager gameManager;
    GameObject player;

    int score;
    float health;

    float maxhealth;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>().gameObject;
        maxhealth = player.GetComponent<Health>().GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        score = gameManager.score;
        if (score > 9999999) score = 9999999; //prevent score ui from looking funky

        scoreText.text = score.ToString("D7");

        if (player != null) health = player.GetComponent<Health>().GetHealth();
        else health = 0;

        healthText.text = ((int)health).ToString();

        healthSlider.value = (health/maxhealth);

        if (((float) health) / ((float) maxhealth) <= badHealthValue) {
            sliderFillArea.color = badHealthColor;
        } else sliderFillArea.color = goodHealthColor;
         


    }
}
