﻿using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    private AudioSource audioSource;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating ("Fire", delay, fireRate);
	}

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

}
