using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    [SerializeField]
    public Sprite[] hitSprites;
    [SerializeField]
    AudioClip breakSound;
    [SerializeField]
    AudioClip touchSound;
    public int Hitpoints = 1;
    public ParticleSystem DestroyEffect;
    private Level level;
    private GameSession gameStatus;
    private BuffsManager buffsManager;

    private SpriteRenderer sr;

    private void Start() {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        buffsManager = FindObjectOfType<BuffsManager>();
        CountBreakableBlocks();
        sr = GetComponent<SpriteRenderer>();
    }

    private void CountBreakableBlocks() {
        if (tag == "Breakable") {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball) {
        Hitpoints--;
        if (Hitpoints <= 0) {
            SpawnDestroyEffect();
            Destroy(gameObject);
            gameStatus.AddToScore();
            level.BlockDestroyed();
            PlayBlockDestroySFX();
            OnBrickDestroy();
        } else {
            sr.sprite = hitSprites[Hitpoints - 1];
            PlayBlockTouchSFX();
        }
    }

    private void SpawnDestroyEffect() {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);

        ParticleSystem.MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }

    private void PlayBlockDestroySFX() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .05f);
    }

    private void PlayBlockTouchSFX() {
        AudioSource.PlayClipAtPoint(touchSound, Camera.main.transform.position, .05f);
    }

    private void OnBrickDestroy() {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float deBuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;

        if (buffSpawnChance <= buffsManager.BuffChance) {
            alreadySpawned = true;
            Buffs newBuff = SpawnBuffs(true);
        }
        if (deBuffSpawnChance <= buffsManager.DebuffChance && !alreadySpawned) {
            Buffs newDebuff = SpawnBuffs(false);
        }
    }

    private Buffs SpawnBuffs(bool isBuff) {
        List<Buffs> collection;

        if (isBuff) {
            collection = buffsManager.AvailableBuffs;
        } else {
            collection = buffsManager.AvailableDebuffs;
        }
        int buffIndex = UnityEngine.Random.Range(0, collection.Count);
        Buffs prefab = collection[buffIndex];
        Buffs newCollectable = Instantiate(prefab, transform.position, Quaternion.identity) as Buffs;

        return newCollectable;
    }
}
