using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;

    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;
    private int lives = 5;
    private bool isPaused = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            
        }
    }


    public void StartGame(int difficulty)
    {
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);

        spawnRate /= difficulty;
        isGameActive = true;

        titleScreen.gameObject.SetActive(false);

        UpdateLife();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public int getLives()
    {
        return lives;
    }

    public void reduceLife()
    {
        lives--;
        UpdateLife();
    }

    public void UpdateLife()
    {
        livesText.text = "Lives: " + lives;
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive && !isPaused)
        {
            isPaused = true;
            isGameActive = false;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGameActive && isPaused)
        {
            isPaused = false;
            isGameActive = true;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }
}
