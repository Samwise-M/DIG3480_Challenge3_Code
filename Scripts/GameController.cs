using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnvalues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text PlugText;

    private bool gameOver;
    private bool restart;
    private int score;

    private void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        PlugText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }
    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnvalues.x, spawnvalues.x), spawnvalues.y, spawnvalues.z);
                Instantiate(hazard, spawnPosition, transform.rotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'Enter' to Restart";
                restart = true;
                break;
            }
        }
        }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            GameOverText.text = "You win!";
            PlugText.text = "Game Created By Samwise Majchrzak";
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        PlugText.text = "Game Created By Samwise Majchrzak";
        gameOver = true;
    }
}
