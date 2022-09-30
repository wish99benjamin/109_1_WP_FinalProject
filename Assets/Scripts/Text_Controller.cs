using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Text_Controller : MonoBehaviour {

    public GameObject lose_text;
    public GameObject toolman;
    private bool can_show;

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(3.79f, -10000, 0.67f);
        can_show = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Chan_Controller.IsDead == true)
        {
            transform.localPosition = new Vector3(2.53f, 4f, 0.67f);
            can_show = true;
        }
        if(can_show == true)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                DontDestroyOnLoad(GameObject.FindGameObjectWithTag("BGM"));
                DontDestroyOnLoad(toolman);
                SceneManager.LoadScene(2);
            }
        }

	}

    private void OnGUI()
    {
        if(can_show == true)
        {
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 30;
            textStyle.normal.textColor = Color.white;

            GUI.Box(new Rect(Screen.width - 420, 900, 410, 50), "", textStyle);
            GUI.Label(new Rect(Screen.width - 390, 910, 380, 30), "Press 'Space' to continue !", textStyle);
        }
    }
}
