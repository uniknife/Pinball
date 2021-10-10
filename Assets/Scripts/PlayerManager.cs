using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody playerRb;
    private int score;
    private int firstHoleCount;
    private int secondHoleCount;
    private int thirdHoleCount;
    [SerializeField] int timer;
    private bool isGameActive;
    [SerializeField] float forceMagnitude;
    [SerializeField] bool canShoot;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI firstHoleCountText;
    [SerializeField] TextMeshProUGUI secondHoleCountText;
    [SerializeField] TextMeshProUGUI thirdHoleCountText;
    [SerializeField] TextMeshProUGUI shootText;
    [SerializeField] Text bestScoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Text gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        canShoot = true;
        isGameActive = true;
        score = 0;
        firstHoleCount = 0;
        secondHoleCount = 0;
        thirdHoleCount = 0;

        scoreText.SetText("Score: " + score);
        firstHoleCountText.SetText("1st Hole Count: " + firstHoleCount);
        secondHoleCountText.SetText("2nd Hole Count: " + secondHoleCount);
        thirdHoleCountText.SetText("3rd Hole Count: " + thirdHoleCount);
        timerText.SetText("Time: " + timer);
        if (DataManager.Instance != null && DataManager.Instance.bestPlayer != null)
        {
            UpdateBestPlayer();
        }
        shootText.gameObject.SetActive(true);
        StartCoroutine(TimerCountDown());
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && isGameActive)
        {
            playerRb.AddForce(Vector3.up * forceMagnitude);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isGameActive)
        {
            if (collider.gameObject.CompareTag("ShootBox"))
            {
                canShoot = true;
                shootText.gameObject.SetActive(true);
            }
            else if (collider.gameObject.CompareTag("CannotShoot"))
            {
                canShoot = false;
                shootText.gameObject.SetActive(false);
            }
            if (collider.gameObject.CompareTag("1st Hole"))
            {
                score += 150;
                firstHoleCount++;
                firstHoleCountText.SetText("1st Hole Count: " + firstHoleCount);
            }
            if (collider.gameObject.CompareTag("2nd Hole"))
            {
                score += 100;
                secondHoleCount++;
                secondHoleCountText.SetText("2nd Hole Count: " + secondHoleCount);
            }
            if (collider.gameObject.CompareTag("3rd Hole"))
            {
                score += 50;
                thirdHoleCount++;
                thirdHoleCountText.SetText("3rd Hole Count: " + thirdHoleCount);
            }
            scoreText.SetText("Score: " + score);
        }

    }

    void UpdateBestPlayer()
    {
        bestScoreText.text = "Best Score: " + DataManager.Instance.bestPlayer + ": " + DataManager.Instance.bestScore;
    }

    IEnumerator TimerCountDown()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1.0f);
            UpdateTimer(1);
            if (timer == 0)
            {
                GameOver();
            }
        }
    }

    void UpdateTimer(int countTime)
    {
        timer -= countTime;
        timerText.text = "Time: " + timer;
    }

    void GameOver()
    {
        DataManager.Instance.SaveBestScore(score);
        UpdateBestPlayer();
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
}
