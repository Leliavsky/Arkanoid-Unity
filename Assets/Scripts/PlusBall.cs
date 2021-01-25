using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlusBall : Buffs {
    private BallManager ballManager;

    private void Start() {
        ballManager = FindObjectOfType<BallManager>();
    }

    protected override void ApplyEffect() {
        foreach (Ball ball in ballManager.Balls.ToList()) {
            ballManager.SpawnBalls(ball.gameObject.transform.position, 2);
        }
    }
}
