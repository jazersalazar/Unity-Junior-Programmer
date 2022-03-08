using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    public GameObject ball;
    public GameObject specialBall;
    public Button startButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public AudioClip catchSound;
    public AudioClip missSound;
    public AudioClip explodeSound;
    public ParticleSystem smoke;
    public ParticleSystem explosion;
    public bool isGameActive = false;

    private AudioSource playerAudio;
    private float spawnRangeX = 5.0f;
    private float spawnInterval = 1.5f;
    private int score = 0;
    private int life = 0;

    void Start() {
        startButton.onClick.AddListener(StartGame);
        playerAudio = GetComponent<AudioSource>();
    }

    IEnumerator SpawnBall() {
        while(isGameActive) {
            Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), 10.0f);

            // Spawn special ball at 10% chance
            if(Random.value > 0.9) {
                Instantiate(specialBall, spawnPos, specialBall.transform.rotation);
            } else {
                Instantiate(ball, spawnPos, ball.transform.rotation);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void StartGame() {
        // Initialize values
        isGameActive = true;
        score = 0;
        life = 3;

        // Update UI
        UpdateScore(0);
        UpdateLife(0);
        gameOverText.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);

        ClearBalls();

        StartCoroutine(SpawnBall());
    }

    void ClearBalls() {
        // Cleanup the balls before starting a new game
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach(GameObject ball in balls) {
            Destroy(ball);
        }

        var specialBalls = GameObject.FindGameObjectsWithTag("Special Ball");
        foreach(GameObject ball in specialBalls) {
            Destroy(ball);
        }
    }

    void GameOver() {
        isGameActive = false;
        StopCoroutine("SpawnBall");
        gameOverText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }

    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

        if (scoreToAdd > 0) {
            playerAudio.PlayOneShot(catchSound, 1.0f);
        }
    }

    public void UpdateLife(int lifeToAdd) {
        life += lifeToAdd;

        // In rare case that life is negative
        if (life >= 0) {
            lifeText.text = "Life: " + life;
        }

        if (life < 1) {
            GameOver();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
        } else {
            if (lifeToAdd < 0) {
                playerAudio.PlayOneShot(missSound, 1.0f);
            } else if (lifeToAdd > 0) {
                playerAudio.PlayOneShot(catchSound, 1.0f);
            }
        }
    }
}
