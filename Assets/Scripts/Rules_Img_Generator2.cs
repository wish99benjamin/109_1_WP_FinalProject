using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rules_Img_Generator2 : MonoBehaviour {

    public Image rules_img;
    public Button rules_bt;
    private Button this_bt;
    // Use this for initialization
    void Start () {
        this_bt = this.GetComponent<Button>();
        this_bt.onClick.AddListener(Rules_Img_Close);
        this_bt.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Rules_Img_Close()
    {
        rules_img.enabled = false;
        rules_bt.gameObject.SetActive(true);
        this_bt.gameObject.SetActive(false);
    }
}
