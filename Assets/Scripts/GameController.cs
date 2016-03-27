using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;

    public GameObject scoreText;
    public GameObject restartText;
    public GameObject gameOverText;


    private int score = 0;
    private bool _gameOver = false;
    private bool _restart = false;


    // Use this for initialization
    void Start () {
        StartCoroutine (SpawnWaves());
        UpdateScore();
        restartText.GetComponent<Text>().text = "";
        gameOverText.GetComponent<Text>().text = "";

}

    void Update()
    {
        if (_restart)
            if (Input.GetButton("X"))
                Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int i = 0; i < hazardCount; ++i)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            if (_gameOver)
            {
                restartText.GetComponent<Text>().text = "Press 'X' for Restart";
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
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.GetComponent<Text>().text = "Game Over!";
        _gameOver = true;
    }
}
