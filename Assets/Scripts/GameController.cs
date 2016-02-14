using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;


    private int score = 0;
    private bool _gameOver = false;
    private bool _restart = false;


    // Use this for initialization
    void Start () {
        StartCoroutine (SpawnWaves());
        UpdateScore();
        restartText.text = "";
        gameOverText.text = "";

    }

    void Update()
    {
        if (_restart)
            if (Input.GetKeyDown(KeyCode.R))
                Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int i = 0; i < hazardCount; ++i)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            if (_gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                _restart = true;
            }
        }
    }

    public void addScore (int newScorevalue)
    {
        score += newScorevalue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        _gameOver = true;
    }
}
