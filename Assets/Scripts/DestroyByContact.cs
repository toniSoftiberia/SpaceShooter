using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue = 50;
    public string exception;

    private GameController gc;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gc = gameControllerObject.GetComponent<GameController>();
        }
        if (gc == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "BoltRed" || other.tag == "BoltPurple")
            other.tag = other.tag;
        if (other.tag != "Boundary" && (exception != null && other.tag != exception)) {
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag == "Player") {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gc.GameOver();
            }else
                Destroy(other.gameObject);
            Destroy(gameObject);
            gc.addScore(scoreValue);
        }
        if (other.tag == exception)
            Destroy(other.gameObject);
    }
}
