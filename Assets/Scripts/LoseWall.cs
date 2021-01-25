using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseWall : MonoBehaviour {
    GameSession gameSession;
    BallManager ballManager;

    private void Awake() {
        ballManager = FindObjectOfType<BallManager>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            Ball ball = collision.GetComponent<Ball>();
            ballManager.Balls.Remove(ball);
            if (ballManager.Balls.Count <= 0) {
                gameSession.Lives--;
                gameSession.CurrentLive();
                ballManager.ResetBalls();
                if (gameSession.Lives <= 0) {
                    SceneManager.LoadScene("Game Over");
                }
            }
        }
    }
}
