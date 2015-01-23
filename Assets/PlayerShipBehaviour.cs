using UnityEngine;
using System.Collections;

public class PlayerShipBehaviour : MonoBehaviour {

    public float speed = 1.0f;
    public Vector3 velocity;

	// Use this for initialization
	void Start () {
        velocity = -Vector3.forward*speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
