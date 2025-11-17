using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int killCount = 0;
    public TextMeshProUGUI killText;
    public GameObject GameOverPanel;
    public GameObject GameWonPanel;
    public float tempoMaximo = 300f;
    private float tempoRestante;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (GameOverPanel != null)
            GameOverPanel.SetActive(false);

        if (GameWonPanel != null)
            GameWonPanel.SetActive(false);
        tempoRestante = tempoMaximo;
        UpdateTimerUI();
    }
    public void AddKill()
    {
        killCount++;
        UpdateKillUI();
        FindObjectOfType<SBTransform>().CheckKillCount();
    }
    public void UpdateKillUI()
    {
        if (killText != null)
            killText.text = killCount.ToString();
    }
    void Update()
    {
        AtualizarTimer();
        UpdateTimerUI();
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        if (GameOverPanel != null)
            GameOverPanel.SetActive(true);
    }
    public void GameWon()
    {
        Time.timeScale = 0;
        if (GameWonPanel != null)
            GameWonPanel.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void voltarMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MENU");
    }

    void AtualizarTimer()
    {
        tempoRestante -= Time.deltaTime;

        if (tempoRestante < 0)
        {
            tempoRestante = 0;
            GameOver();
        }
    } 
    public void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int min = Mathf.FloorToInt(tempoRestante / 60);
            int sec = Mathf.FloorToInt(tempoRestante % 60);

            timerText.text = $"{min:00}:{sec:00}";
        }
    }
}
