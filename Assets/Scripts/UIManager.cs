using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    public Button startButton, nextLevelButton;
    public GameObject menuUI, inGameUI, endUI;
    public TextMeshProUGUI levelText;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        SetBindings();
    }

    private void SetBindings()
    {
        startButton.onClick.AddListener(() =>
        {
            gameManager.StartGame();
            menuUI.SetActive(false);
            inGameUI.SetActive(true);
        });
        nextLevelButton.onClick.AddListener(() =>
        {
            endUI.SetActive(false);
            gameManager.StartNextGame();
            inGameUI.SetActive(true);
        });
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "LEVEL " + (level + 1);
    }

    void Update()
    {
        
    }
    
    public void EndLevel()
    {
        inGameUI.SetActive(false);
        endUI.SetActive(true);
    }
}