using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    [SerializeField]
    float speedPaddle = 16f;
    [SerializeField]
    float minX = 1f;
    [SerializeField]
    float maxX = 15f;
    private BallManager ballManager;

    void Start() {
        ballManager = FindObjectOfType<BallManager>();
    }
    
    void Update() {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;

    }

    private float GetXPos() {
        return Input.mousePosition.x / Screen.width * speedPaddle;
    }

    private void OnCollisionEnter2D(Collision2D coll) {//вычисление расположения мяча от центра
        if (coll.gameObject.tag == "Ball") {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x) {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), ballManager.BallSpeed));
            } else {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), ballManager.BallSpeed));
            }
        }
    }
}
