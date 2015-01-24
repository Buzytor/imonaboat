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

    public float speed = 1.0f;

    public float avoidanceDistance = 1.0f;
    public float avoidanceSpeed = 0.5f;

    private PlayerShipBehaviour player;
    private Wobbler wob;

    static Vector3 generateStartPosition() {
        float x, y, z;
        y = 0;
        do {
            x = Random.Range( posRanXm, posRanXM );
        } while(x < excRanXm || x > excRanXM);
        do {
            z = Random.Range( posRanZm, posRanZM );
        } while(z < excRanZm || z > excRanZM);
        return new Vector3(x, y, z);
    }

	// Use this for initialization
    void Start() {
        player = GameObject.Find("PlayerShip").GetComponent<PlayerShipBehaviour>();

        startPosition = generateStartPosition();

        float alpha = Random.Range(-180, 180);
        Quaternion directionQuat = Quaternion.AngleAxis(alpha, Vector3.up);

        this.gameObject.transform.position = startPosition;
        this.gameObject.transform.rotation = directionQuat;
        wob = new Wobbler();
    }
	
	// Update is called once per frame
	void Update() {
        float w = wob.GetWobWob();
        transform.Translate(Vector3.up*w*Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward*speed*Time.deltaTime, Space.Self);
        transform.Translate(player.velocity*Time.deltaTime, Space.World);
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject ship in ships){
            if(ship != this.gameObject)
                transform.Translate(AvoidanceManeuver(ship)*Time.deltaTime, Space.World);
        }
	}

    public Vector3 AvoidanceManeuver(GameObject ship) {
        GameObject[] toAvoid = GameObject.FindGameObjectsWithTag("Obstacle");
        Vector3 maneuver = new Vector3();
        foreach (GameObject otherShip in toAvoid) {
            float dist = Vector3.Distance(ship.transform.position, otherShip.transform.position);
            if(dist < avoidanceDistance) {
                Vector3 positionDelta = otherShip.transform.position - ship.transform.position;
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
    
}
