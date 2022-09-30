using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolman_Controller : MonoBehaviour {
    static public int score;
    static public int ball;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Chan_Controller.IsDead == true)
        {
            score = Chan_Controller.score;
            ball = Chan_Controller.ball;
            transform.rotation = Quaternion.identity;
        }
	}
}
