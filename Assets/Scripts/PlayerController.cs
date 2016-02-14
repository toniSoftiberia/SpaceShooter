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

    public GameObject shot;
    public Transform shotSpawn;


    private Rigidbody _rb;
    private AudioSource _audio;
    private float _nextFire = 0.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            GameObject clone  = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
            _audio.Play();
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
}
