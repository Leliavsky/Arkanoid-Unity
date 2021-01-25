using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField] 
    Paddle paddle1;
    private Ball initialBall;
    private Rigidbody2D initialBallRb;
    
    Vector2 paddleToBallVector;
    public List<Ball> Balls { get; set; }
    public float BallSpeed = 250;
    bool hasStarted = false;

    void Start() {
        InitBall();
    }

    void Update() {
        if (!hasStarted) {
            LockBallToPaddle();
            if (Input.GetMouseButtonDown(0)) {
                initialBallRb.isKinematic = false;
                initialBallRb.AddForce(new Vector2(0, BallSpeed));
                hasStarted = true;
            }
        }
    }

    private void InitBall() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        Vector3 startingPosition = new Vector3(paddlePos.x, paddlePos.y + .27f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();
        
        Balls = new List<Ball> {
            initialBall
        };
    }
    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        Vector3 startingPosition = new Vector3(paddlePos.x, paddlePos.y + .27f, 0);
        initialBall.transform.position = startingPosition;
    }

    public void ResetBalls() {
        foreach (var ball in Balls.ToList()) {
            Destroy(ball.gameObject);
        }
        hasStarted = false;
        InitBall();
    }

    public void SpawnBalls(Vector3 position, int count) {
        for (int i = 0; i < count; i++) {
            Ball spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as Ball;

            Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
            spawnedBallRb.isKinematic = false;
            spawnedBallRb.AddForce(new Vector2(0, BallSpeed));
            Balls.Add(spawnedBall);
        }
    }
}
