using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Scene_Conrtroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Start_Game);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Start_Game()
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("BGM"));
        SceneManager.LoadScene("Race_Scene");
    }
}
