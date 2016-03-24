using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
    public float
        xMin = -6,
        xMax = 6,
        zMin = -4,
        zMax = 8;
}

public class PlayerController : MonoBehaviour {

    public float speed = 10;
    public float tilt;
    public Boundary bundary;
    public float fireRate = 0.25f;

    public GameObject purpleShoot;
    public GameObject redShoot;
    public Transform shootSpawn;


    private Rigidbody _rb;
    private AudioSource _audio;
    private float _nextFire = 0.0f;

    public enum ShipColor {
        red,
        purple
    };

    public ShipColor shipColor = ShipColor.red;

    public Material redShip;
    public Material purpleShip;

    public GameObject playerExplosion;

    private GameController gc;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gc = gameControllerObject.GetComponent<GameController>();
        }
        if (gc == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }


    void Update() {
        if (Input.GetButton("Fire1") && Time.time > _nextFire) {
            GameObject shoot = redShoot;
            if (shipColor == ShipColor.purple)
                shoot = purpleShoot;
            _nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(shoot, shootSpawn.position, shootSpawn.rotation) as GameObject;
            _audio.Play();
        }
        if (Input.GetKeyDown("j")) {
            ChangeShipColor();
        }
    }


    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rb.velocity = (movement * speed);

        _rb.position = new Vector3(
            Mathf.Clamp(_rb.position.x, bundary.xMin, bundary.xMax),
            0.0f,
            Mathf.Clamp(_rb.position.z, bundary.zMin, bundary.zMax)
           );

        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
    }

    void ChangeShipColor() {
        if (shipColor == ShipColor.red) {
            GetComponent<Renderer>().sharedMaterial = purpleShip;
            shipColor = ShipColor.purple;
        } else {
            GetComponent<Renderer>().sharedMaterial = redShip;
            shipColor = ShipColor.red;
        }

    }

    void OnTriggerEnter(Collider other) {
        if (other.tag != "Boundary") {
            if ((other.tag == "BoltRed" && shipColor == ShipColor.purple) ||
                (other.tag == "BoltPurple" && shipColor == ShipColor.red)) {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gc.GameOver();
                Destroy(other.gameObject);
                Destroy(gameObject);
            } 
        }
    }
}
