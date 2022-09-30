using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rules_Img_Generator1 : MonoBehaviour {

    public Image rules_img;
    public Button close_bt;
    private Button this_bt;
    // Use this for initialization
    void Start () {
        this_bt = this.GetComponent<Button>();
        this_bt.onClick.AddListener(Rules_Img_Show);
        rules_img.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Rules_Img_Show()
    {
        rules_img.enabled = true;
        this_bt.gameObject.SetActive(false);
        close_bt.gameObject.SetActive(true);
    }
}
