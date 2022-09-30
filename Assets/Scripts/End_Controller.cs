using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Controller : MonoBehaviour {

    private int score_num;
    private int score_temp;
    private float score_accumulator_1;
    private int score_accumulator_2;
    private int pause_count;
    private string score_string;
    private int ball_num;
    private string ball_string;
    private int score_finish;
	// Use this for initialization
	void Start () {
        score_num = Toolman_Controller.score;
        score_temp = 0;
        score_finish = 0;
        score_accumulator_1= 0f;
        score_accumulator_2 = 0;
        pause_count = 0;
        ball_num = Toolman_Controller.ball;
    }
	
	// Update is called once per frame
	void Update () {
        if (score_finish == 0)
        {
            if (score_temp < score_num)
            {
                if (score_num - score_temp > 1000)
                {
                    score_temp += (score_num - score_temp) / 13;
                }
                else if (score_num - score_temp > 600)
                {
                    score_temp += (score_num - score_temp) / 11;
                }
                else if (score_num - score_temp > 100)
                {
                    score_temp += 2;
                }
                if (score_num - score_temp > 30)
                {
                    score_temp++;
                }
                else if (score_num - score_temp > 10)
                {
                    score_accumulator_1 += 0.5f;
                    if (score_accumulator_1 == 1)
                    {
                        score_temp++;
                        score_accumulator_1 = 0;
                    }
                }
                else if (score_num - score_temp > 2)
                {
                    score_accumulator_1 += 0.2f;
                    if (score_accumulator_1 >= 1)
                    {
                        score_temp++;
                        score_accumulator_1 = 0;
                    }
                }
                else
                {
                    score_accumulator_1 += 0.05f;
                    if (score_accumulator_1 >= 1)
                    {
                        score_temp++;
                        score_accumulator_1 = 0;
                        if(score_temp == score_num)
                        {
                            score_finish = 1;
                        }
                    }
                }             
            }
        }
        else if(score_finish == 2)
        {
            if(ball_num >= 10000)
            {
                score_temp += 69 * (ball_num - 10001);
                ball_num = 10001;
            }
            else if(ball_num >= 1000)
            {
                score_temp += 62100;
                ball_num -= 900;
            }
            else if(ball_num >= 100)
            {
                score_temp += 207;
                ball_num -= 3;
            }
            else if(ball_num >= 60)
            {
                score_temp += 69;
                ball_num -= 1;
            }
            else if(ball_num >= 45)
            {
                if(score_accumulator_2 == 0)
                {
                     score_accumulator_2++;
                    score_temp += 35;
                }
                else
                {
                    score_accumulator_2 = 0;
                    score_temp += 34;
                    ball_num--;
                }
            }
            else if (ball_num >= 20)
            {
                if (score_accumulator_2 <= 4)
                {
                    score_accumulator_2++;
                    score_temp += 9;
                }
                else if(score_accumulator_2 <= 7)
                {
                    if (score_accumulator_2 == 7)
                        score_accumulator_2 = 0;
                    else
                        score_accumulator_2++;
                    score_temp += 8;
                    ball_num--;
                }
            }
            else if (ball_num >= 5)
            {
                if (score_accumulator_2 < 23)
                {
                    score_accumulator_2++;
                    score_temp += 3;
                }
                else if (score_accumulator_2 == 23)
                {
                    score_accumulator_2 = 0;
                    score_temp += 3;
                    ball_num--;
                }
            }
            else if (ball_num >= 3)
            {
                if (score_accumulator_2 < 69)
                {
                    score_accumulator_2++;
                    score_temp += 1;
                }
                else if (score_accumulator_2 == 69)
                {
                    score_accumulator_2 = 0;           
                    score_temp += 1;
                    ball_num--;
                }                
            }
            else if (ball_num > 0)
            {
                if (score_accumulator_2 < 128)
                {
                    score_accumulator_2++;
                    if(score_accumulator_2%2 == 0)
                    {
                        score_temp += 1;
                    }
                }
                else if (score_accumulator_2 == 128)
                {
                    score_accumulator_2 = 0;
                    score_temp += 1;
                    ball_num--;
                    if(ball_num == 0)
                    {
                        score_finish = 3;
                    }
                }
            }
        }

        if(score_finish == 1)
        {
            if (pause_count == 0)
            {
                score_num += 69 * ball_num;
                //score_finish = 1;
            }
            pause_count++;
            //Debug.Log(pause_count);
            if (pause_count == 150)
            {
                score_finish = 2;
            }           
        }


        score_string = "Score :" + score_temp.ToString();
        ball_string = "Ball :" + ball_num.ToString();
        if(Input.GetKey(KeyCode.Space))
        {           
            SceneManager.LoadScene(0);
            Destroy(GameObject.FindGameObjectWithTag("BGM"));
        }
	}

    void OnGUI()
    {
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 60;
        titleStyle.normal.textColor = Color.white;

        GUIStyle textStyle = new GUIStyle();
        textStyle.fontSize = 30;
        textStyle.normal.textColor = Color.white;


        GUI.Box(new Rect(Screen.width - 800, 200, 300, 400), "Result",titleStyle);
        GUI.Label(new Rect(Screen.width - 800 , 280, 300, 100), score_string, textStyle);
        GUI.Label(new Rect(Screen.width - 800, 330, 300, 100), ball_string, textStyle);
        GUI.Label(new Rect(Screen.width - 800, 380, 300, 100), "Press 'Space' to continue", textStyle);
        /*GUI.Label (new Rect (Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
        GUI.Label (new Rect (Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
        GUI.Label (new Rect (Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
        GUI.Label (new Rect (Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
        GUI.Label (new Rect (Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");*/
    }
}
