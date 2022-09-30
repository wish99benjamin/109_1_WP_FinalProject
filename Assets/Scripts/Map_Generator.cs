using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Generator : MonoBehaviour {

    public Transform Bridge_Large;
    public Transform Bridge_Small;
    public Transform Left_turn;
    public Transform Right_turn;
    public Transform Long_Straight_Road_3;
    public Transform Straight_Road_1;
    public Transform T_intersection;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Trigger_Door")
        {
            //Transform temp = Instantiate(Long_Straight_Road_3);
            Transform temp = Instantiate(Left_turn);
            temp.parent = transform;
            temp.localPosition = collider.transform.parent.position;
            //temp.Rotate(new Vector3(0,0,0));

        }
    }
}
