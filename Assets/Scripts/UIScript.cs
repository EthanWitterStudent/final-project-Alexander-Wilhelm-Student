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
    [System.NonSerialized] public float[] cooldowns = new float[5];
    [System.NonSerialized] public float[] timers = new float[5];

    [SerializeField] Transform[] gridButtons;
    [SerializeField] Image stonksPanel;

    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] float randomTextColor;
    [SerializeField] Image fadePanel;
    [SerializeField] float panelFadeTime;
    GameManager gameManager;
    Color fadecolor;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fadecolor = new Color(0, 0, 0, Time.deltaTime / panelFadeTime); //dont recalculate this
        UpdateMoneyText();
        towerButtons[0].Select(); //select the first one
    }

    // Update is called once per frame
    void Update()
    {
        stonksPanel.fillAmount = gameManager.GetCashTimer() / gameManager.GetCashDelay();
        for (int i = 0; i < towerButtons.Length; i++)
        {
            if (towerButtons[i].enabled) towerButtons[i].image.fillAmount = (cooldowns[i] - timers[i]) / cooldowns[i];
        }
        
    }

    void FixedUpdate()
    {
        for (int i = 0; i < timers.Length; i++) timers[i] -= Time.fixedDeltaTime;
    }

    public void setupTowerButton(int index, GameObject tower, Sprite image)
    {
        Button button = towerButtons[index];
        button.gameObject.SetActive(true);
        if (image != null) button.GetComponent<Image>().sprite = image;
        Health health = tower.GetComponent<Health>();
        if (health != null)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"${health.GetCashCost()}";
            cooldowns[index] = health.GetCooldown();
        }
    }

    public void UpdateMoneyText()
    {
        moneyText.text = $"${gameManager.GetCash().ToString("D4")}";
    }

    public Vector3 GridButtonClick()
    {
        Transform tf = GetClosestButton(gridButtons); // get the screen position of the button we're clicking
        return tf.position;
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

    public IEnumerator WinText()
    {
        winText.gameObject.SetActive(true);
        while (true)
        {
            winText.color = Color.white - new Color(Random.Range(0, randomTextColor), Random.Range(0, randomTextColor), Random.Range(0, randomTextColor), 0);
            yield return null;
        }
    }
    public IEnumerator FadePanel()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);
        while (fadePanel.color.a <= 1)
        {
            fadePanel.color += fadecolor;
            yield return null;
        }

    public void SelectTowerButton(int index) {
        Debug.Log($"Index: {index}, button {towerButtons[index].gameObject.name}");
        towerButtons[index].Select(); 
    }

    public void SelectTowerButton(int index)
    {
        towerButtons[index].Select();
    }

    public bool getRightClick() {
        Debug.Log("Got RMB check!");
        Debug.Log(Mouse.current.rightButton.wasPressedThisFrame);
        return Mouse.current.rightButton.wasPressedThisFrame;
    }
}
