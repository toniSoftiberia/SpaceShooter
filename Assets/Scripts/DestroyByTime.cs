using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float livetime = 2;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, livetime);
	}
}
