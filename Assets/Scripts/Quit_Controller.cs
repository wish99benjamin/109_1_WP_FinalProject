using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Quit_Game);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Quit_Game()
    {
        Application.Quit();
    }
}
