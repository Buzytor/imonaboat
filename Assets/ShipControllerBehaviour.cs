using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControllerBehaviour : MonoBehaviour {

    public List<Transform> shipPrefabs;

    private List<GameObject> ships = new List<GameObject>();

    public void CreateShip() {
        if(shipPrefabs.Count > 0) {
            Transform g = (Transform)Instantiate(shipPrefabs[Random.Range(0, shipPrefabs.Count-1)]);
            ships.Add(g.gameObject);
        }
    }

    public void CheckAndDeleteShips() {
        ships.RemoveAll(ship =>
                            ship.GetComponent<OtherShipBehaviour>().IsOutsideOfMap()
                            && ship.GetComponent<OtherShipBehaviour>().Delete()
                        );
    }	

	// Use this for initialization
	void Start () {
        for(int i=0; i<5; i++) {
            CreateShip();
        }
	}
	
	// Update is called once per frame
	void Update () {
        CheckAndDeleteShips();	
	}
}
