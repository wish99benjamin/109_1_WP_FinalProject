using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Wave : MonoBehaviour {
    public Transform wave;
    public Rigidbody rb;
    private float chan_y;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        
        for(int x = -30; x <= 30; x++)
        {
            for(int z = -30; z <= 30; z++)
            {
                Transform temp = Instantiate(wave);
                temp.parent = transform;
                temp.localPosition = new Vector3(9.5f * x, -3, 9.5f * z);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		chan_y = transform.parent.position.y;
        transform.localPosition = new Vector3(0, -chan_y, 0);
    }
}
