using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
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

    public GameObject blueShoot;
    public GameObject orangeShoot;
    public Transform shootSpawn;


    private Rigidbody _rb;
    private AudioSource _audio;
    private float _nextFire = 0.0f;

    public enum ShipColor {
        orange,
        blue
    };
    
    public ShipColor shipColor = ShipColor.orange;
    
    public Material orangeShip;
    public Material blueShip;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }


    void Update(){
        if (Input.GetButton("Fire1") && Time.time > _nextFire){
            GameObject shoot = orangeShoot;
            if (shipColor == ShipColor.blue)
                shoot = blueShoot;
            _nextFire = Time.time + fireRate;
            GameObject clone  = Instantiate(shoot, shootSpawn.position, shootSpawn.rotation) as GameObject;
            _audio.Play();
        }
        if (Input.GetKeyDown("j")) {
            ChangeShipColor();
        }
    }


    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
       
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        _rb.velocity = (movement * speed);

        _rb.position = new Vector3(
            Mathf.Clamp (_rb.position.x, bundary.xMin, bundary.xMax),
            0.0f,
            Mathf.Clamp(_rb.position.z, bundary.zMin, bundary.zMax)
           );

        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);
    }

    void ChangeShipColor() { 
        if (shipColor == ShipColor.orange) {
            GetComponent<Renderer>().sharedMaterial = blueShip;
            shipColor = ShipColor.blue;
        } else {
            GetComponent<Renderer>().sharedMaterial = orangeShip;
            shipColor = ShipColor.orange;
        }

    }
}
