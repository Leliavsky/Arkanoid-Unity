using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOrMinusSpeed : Buffs {
  private GameSession gameSession;
  public float addSpeed;
  private void Start() {
    gameSession = FindObjectOfType<GameSession>();
  }
  protected override void ApplyEffect() {
    gameSession.gameSpeed += addSpeed;
  }
}
