using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusLife : Buffs {
  private GameSession gameSession;
  void Start() {
    gameSession = FindObjectOfType<GameSession>();
}
  protected override void ApplyEffect() {
    gameSession.PlusLife();
  }
}
