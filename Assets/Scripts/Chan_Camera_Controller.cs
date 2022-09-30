using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chan_Camera_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Chan_Controller.IsDead)
        {
            transform.RotateAround(new Vector3(0,0,0),new Vector3(0,1,0), 0.5f);
        }
        /*if(Chan_Controller.rotate_time > 0)
        {
            transform.Rotate(new Vector3(0, 9, 0));
            Chan_Controller.rotate_time--;
        }
        else if (Chan_Controller.rotate_time < 0)
        {
            transform.Rotate(new Vector3(0, -9, 0));
            Chan_Controller.rotate_time++;
        }*/
    }
}
