using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerShipAudioManager))]
public class PlayerShipBehaviour : MonoBehaviour {
    public float speed = 2.0f;
    public Vector3 velocity;

    public float wobbleScale;

    Wobbler wob;

    private bool spacePressedDown;
    private float hornTimer;
    private float activationTimer;
    private bool enableActivationTimer = false;
    public float hornTimeTreshold;
    public HornControllerBehaviour hornController;
    public float activationThreshold; 

	// Use this for initialization
	void Start () {
        velocity = -Vector3.forward*speed;
        wob = new Wobbler(wobbleScale);

    }

	// Update is called once per frame
	void Update () {
        float w = wob.GetWobWob();
        transform.Translate(Vector3.up*w*Time.deltaTime, Space.World);

        if(Input.GetKey(KeyCode.Space)) {
            if(!spacePressedDown) {
                // TODO do horn stuff

                // play sounds
                GetComponent<PlayerShipAudioManager>().StartHorn();
                spacePressedDown  = true;
                hornTimer = 0;
            } else {
                hornTimer += Time.deltaTime;
            }
        } else if(!Input.GetKey(KeyCode.Space) && spacePressedDown) {
            // TODO do horn stuff
            GetComponent<PlayerShipAudioManager>().StopHorn();
            spacePressedDown = false;

            if(hornTimer > hornTimeTreshold) {
                hornController.RegisterSignal(HornControllerBehaviour.SignalType.Long);
            } else { 
                hornController.RegisterSignal(HornControllerBehaviour.SignalType.Short);
            }

            enableActivationTimer = true;
            activationTimer = 0;
        }

        if(enableActivationTimer) {
            activationTimer += Time.deltaTime;
            if(activationTimer > activationThreshold) {
                hornController.SignalsActivate();
                enableActivationTimer = false;
                activationTimer = 0;
            }
        }

	}

    void OnCollisionEnter() {
        GameObject c = GameObject.Find("GameController");
        c.GetComponent<GameFailureBehaviour>().GameOver();

        Destroy(transform.FindChild("wave").gameObject);
    }

}
