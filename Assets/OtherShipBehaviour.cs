using UnityEngine;
using System.Collections;

public class OtherShipBehaviour : MonoBehaviour {

    //full range
    public static float posRanZm = -40.0f;
    public static float posRanZM = 40.0f;
    public static float posRanXm = -40.0f;
    public static float posRanXM = 40.0f;
    //exclusion range
    public static float excRanZm = -40.0f;
    public static float excRanZM = -10.0f;
    public static float excRanXm = -20.0f;
    public static float excRanXM = 20.0f;


    private Vector3 startPosition;
    private Vector3 startAngle;

    public float startY = 0.0f;
    public float speed = 1.0f;

    public float avoidanceDistance = 1.0f;
    public float avoidanceSpeed = 0.5f;

    public float wobbleScale = 0.5f;

    private int toMoveFramesX = 0;

    private PlayerShipBehaviour player;
    private Wobbler wob;

    public static Vector3 generateStartPosition() {
        float x, z;
        do {
            x = Random.Range( posRanXm, posRanXM );
        } while(x < excRanXm || x > excRanXM);
        do {
            z = Random.Range( posRanZm, posRanZM );
        } while(z < excRanZm || z > excRanZM);
        return new Vector3(x, 0, z);
    }

	// Use this for initialization
    void Start() {
        player = GameObject.Find("PlayerShip").GetComponent<PlayerShipBehaviour>();


        startPosition = this.gameObject.transform.position;
        startPosition.y = startY;

        startAngle = this.gameObject.transform.rotation * (-Vector3.forward);


        wob = new Wobbler(wobbleScale);
    }

	// Update is called once per frame
	void Update() {

        float w = wob.GetWobWob();
        transform.Translate(Vector3.up*w*Time.deltaTime, Space.World);

        transform.Translate(startAngle*speed*Time.deltaTime, Space.Self);
        if(toMoveFramesX != 0) {
            if(toMoveFramesX > 0) {
                transform.Translate(Vector3.right*Time.deltaTime, Space.World);
                toMoveFramesX --;
            }
            if(toMoveFramesX < 0) {
                transform.Translate(-Vector3.right*Time.deltaTime, Space.World);
                toMoveFramesX ++;
            }
        }

        Vector3 avoidanceManeuver = AvoidanceManeuver(gameObject);
        transform.Translate(avoidanceManeuver*Time.deltaTime, Space.World);
        Quaternion lookRotation = Quaternion.LookRotation(avoidanceManeuver);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1);

        transform.Translate(player.velocity*Time.deltaTime, Space.World);
	}

    public Vector3 AvoidanceManeuver(GameObject ship) {
        GameObject[] toAvoid = GameObject.FindGameObjectsWithTag("Obstacle");
        Vector3 maneuver = new Vector3();
        foreach (GameObject otherShip in toAvoid) {
            if(otherShip == ship) continue;
            float dist = Vector3.Distance(ship.transform.position, otherShip.transform.position);
            if(dist < avoidanceDistance) {
                Vector3 positionDelta = -otherShip.transform.position + ship.transform.position;
                positionDelta.y = 0;
                Vector3 radiusVector = positionDelta.normalized * avoidanceDistance;
                maneuver += (radiusVector - positionDelta)/2 * avoidanceSpeed;
            }
        }
        maneuver.y = 0;
        return maneuver;
    }


    public bool IsOutsideOfMap() {
        // Game map is of size [-50, 50]x[-50, 50]
        return Mathf.Abs(transform.position.x) > 50.0f || Mathf.Abs(transform.position.z) > 50.0f;
    }

    public bool Delete() {
        Destroy(this.gameObject);
        return true; // intentional, allows for && with predicates
    }

    internal void ReactToSignal(HornControllerBehaviour.Signal s) {
        switch(s) {
            case HornControllerBehaviour.Signal.Left:
                toMoveFramesX -= 10;
                break;
            case HornControllerBehaviour.Signal.Right:
                // TODO turn right
                toMoveFramesX += 10;
                Debug.Log("Right");
                break;
            default: break;
        }
    }
}
