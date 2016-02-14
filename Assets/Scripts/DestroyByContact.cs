using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue = 50;

    private GameController gc;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gc = gameControllerObject.GetComponent<GameController>();
        }
        if (gc == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if( other.tag != "Boundary")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gc.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
            gc.addScore(scoreValue);
        }
    }
}
