using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {
    //config parameters
    [Range(0.1f, 10f)]
    [SerializeField]
    public float gameSpeed = 1f;
    [SerializeField]
    int pointsPerBlockDestroyed = 50;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI liveText;
    [SerializeField]
    int currentScore = 0;
    public int AvailibleLives = 3;
    public int Lives { get; set; }

    private void Awake() {
      int gameStatusCount = FindObjectsOfType<GameSession>().Length;
      if (gameStatusCount > 1) {
        gameObject.SetActive(false);
        Destroy(gameObject);
      } else {
        DontDestroyOnLoad(gameObject);
      }
    }

    private void Start() {
      Lives = AvailibleLives;
      scoreText.text = currentScore.ToString();
      liveText.text = $"LIVES: {Lives.ToString()}";
    }

    void Update() {
      Time.timeScale = gameSpeed;
    }
    
    public void AddToScore() {
      currentScore += pointsPerBlockDestroyed;
      scoreText.text = currentScore.ToString();
    }

    public void CurrentLive() {
      liveText.text = $"LIVES: {Lives.ToString()}";
    }

    public void PlusLife() {
      Lives ++;
      CurrentLive();
    }

    public void ResetGame() {
      Destroy(gameObject);
    }
}