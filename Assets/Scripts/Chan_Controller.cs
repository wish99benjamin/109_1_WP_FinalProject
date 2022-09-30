using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chan_Controller : MonoBehaviour {

    static public GameObject shield;
    [SerializeField] private float speed ;
    private float shift ;
    private float time ;
    static public int score;
    static public int ball;
    private int score_accumulator;//計算score的小數點
    private int log = 0;
    private int limit = 1000;//計算score tag的位置
    private int score_time;
    static public int rotate_time;
    private bool IsTurn;
    static public bool IsDead;//chan dead
    new Vector3 position_temp;
    Animator animator;
    Rigidbody rigidbody;
    Position position;
    enum Position
    {
        Left,
        Mid,
        Right
    }
	// Use this for initialization
	void Start () {
        speed = 5.0f ;
        shift = 1.2f ;
        time = 0;
        position = Position.Mid;
        IsTurn = false;
        animator = GetComponent<Animator>();
        animator.SetFloat("Player_Speed", speed);
        rigidbody = GetComponent<Rigidbody>();
        score = 0;
        IsDead = false;
        animator.SetBool("Die_Flag", IsDead);
        shield = gameObject.transform.Find("Shield").gameObject;
        shield.SetActive(false);
        score_time = 0;
        rotate_time = 0;
        position_temp = Vector3.zero;
        ball = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float veloctiy = shift / Time.deltaTime;
        //transform.localPosition += speed * Time.deltaTime * transform.forward;
        //Debug.Log("Time" + time.ToString());
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        animator.SetFloat("Player_Speed", speed);
        if (time > 0 )
        {
            float temp = Time.deltaTime*100;
            if(time - temp < 0)
            {
                temp = time;
            }
            //Debug.Log("TIME > 0");
            //transform.localPosition += new Vector3(0, 0, 0.024f);
            transform.Translate(Vector3.left * 0.024f *  temp, Space.Self);
            time-= temp ;
        }
        else if(time < 0)
        {
            float temp = Time.deltaTime*100;
            if (time + temp > 0)
            {
                temp = -time;
            }
            //Debug.Log("TIME < 0");
            //transform.localPosition += new Vector3(0, 0, -0.024f);
            transform.Translate(Vector3.right * 0.024f * temp, Space.Self);
            time+=temp;
        } 
        else if(Input.GetKey(KeyCode.A) && (!IsDead))
        {
            if (position != Position.Left)
            {
                position--;
                time = 50;
            }
        }
        else if (Input.GetKey(KeyCode.D)&&(!IsDead))
        {
            if (position != Position.Right)
            {
                position++;
                time = -50;
            }
        }

        if(transform.localPosition.y < 0.1)
        {
            if(Input.GetKeyDown(KeyCode.W) && (!IsDead))
            {
                rigidbody.AddForce(Vector3.up * 300);
                
            }
        }

        ++score_accumulator;
        if((score_accumulator == 7)&&(IsDead == false))
        {
            score_accumulator = 0;
            ++score;
        }

        if (transform.localPosition.y <=-0.25)
        {
            speed = 0;
            IsDead = true;
            animator.SetBool("Die_Flag", IsDead);
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
            shield.SetActive(false);
        }

        score_time++;
        if(score_time == 1500 && IsDead == false)
        {
            speed += 0.25f;
            score_time = 0;
        }

        //do rotate
        /*if(rotate_time < 0)
        {
            if (rotate_time < -5)
            {
                transform.Rotate(new Vector3(0, -5, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, -30, 0));
            }
            //position_temp += transform.rotation * new Vector3(0, 0, speed);
            if (rotate_time == -1)
            {
                //transform.Translate(20*Vector3.forward * speed, Space.Self);

                transform.position = position_temp;// + transform.rotation *  new Vector3(0, 0,2) ;
                
                if (position == Position.Right)
                {
                    transform.position += transform.rotation * new Vector3(1.2f, 0, 0);
                }
                else if (position == Position.Left)
                {
                    transform.position += transform.rotation * new Vector3(-1.2f, 0, 0);
                }
                time = 0;
            }
            rotate_time++;
        }
        else if (rotate_time > 0)
        {
            if (rotate_time > 5)
            {
                transform.Rotate(new Vector3(0, 5, 0));
            }
            else
            {
                transform.Rotate(new Vector3(0, 30, 0));
            }
            //position_temp += transform.rotation * new Vector3(0, 0, speed);
            if (rotate_time == 1)
            {
                // transform.position = position_temp;
                // transform.Translate(20 * Vector3.forward * speed, Space.Self);
                transform.position = position_temp;// + transform.rotation * new Vector3(0, 0, 2);
                if (position == Position.Right)
                {
                    transform.position += transform.rotation * new Vector3(1.2f, 0, 0);
                }
                else if (position == Position.Left)
                {
                    transform.position += transform.rotation * new Vector3(-1.2f, 0, 0);
                }
                time = 0;
            }
            rotate_time--;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Beachball")
        {
            Destroy(other.gameObject);
            ball++;
        }
        else if(other.gameObject.tag == "Shield")
        {
            Destroy(other.gameObject);
            shield.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Trigger_Door_Canturn")&&(!IsTurn)&&(!IsDead))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                /*for (int i = 0; i < 45; i++)
                {*/
                    transform.Rotate(new Vector3(0, -90, 0));
                //}
                rotate_time = -10;
                //Camera.main.transform.Rotate(new Vector3(0, 90, 0));
                transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, other.gameObject.transform.position.z);
                if(position == Position.Right)
                {
                    transform.position += transform.rotation * new Vector3(1.2f, 0, 0);
                }
                else if(position == Position.Left)
                {
                    transform.position += transform.rotation * new Vector3(-1.2f, 0, 0);
                }
                time = 0;
                IsTurn = true;
               //position_temp = new Vector3(other.gameObject.transform.position.x, transform.position.y, other.gameObject.transform.position.z);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                /*for (int i = 0; i < 45; i++)
                {*/
                    transform.Rotate(new Vector3(0, 90, 0));
                //}
                rotate_time = 10;
                //Camera.main.transform.Rotate(new Vector3(0, -90, 0));
                transform.position = new Vector3(other.gameObject.transform.position.x, transform.position.y, other.gameObject.transform.position.z);
                if (position == Position.Right)
                {
                    transform.position += transform.rotation * new Vector3(1.2f, 0, 0);
                }
                else if (position == Position.Left)
                {
                    transform.position += transform.rotation * new Vector3(-1.2f, 0, 0);
                }
                time = 0;
                IsTurn = true;
                //position_temp = new Vector3(other.gameObject.transform.position.x, transform.position.y, other.gameObject.transform.position.z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trigger_Door_Canturn")
        {
            IsTurn = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hazard")
        {
            if (!shield.active)
            {
                speed = 0;
                IsDead = true;
                animator.SetBool("Die_Flag", IsDead);
                transform.position = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.identity;
                shield.SetActive(false);
            }
            else if(shield.active)
            {
                Destroy(collision.gameObject);
                shield.SetActive(false);
            }
        }
    }

    void OnGUI()
    {       
        if(score > limit)
        {
            ++log;
            limit *= 10;
        }

        GUIStyle textStyle = new GUIStyle();
        textStyle.fontSize = 30;
        textStyle.normal.textColor = Color.white;
        //GUIStyle textStyle;
        GUI.Box (new Rect (Screen.width - 330, 20, 300, 150), "Score:",textStyle);
        GUI.Label (new Rect (Screen.width - 165-(log*10), 20, 300, 120), score.ToString(),textStyle);

        GUI.Box(new Rect(Screen.width - 330, 50, 300, 150), "Ball:", textStyle);
        GUI.Label(new Rect(Screen.width - 165 , 50, 300, 120), ball.ToString(), textStyle);
        /*GUI.Label (new Rect (Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
        GUI.Label (new Rect (Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
        GUI.Label (new Rect (Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
        GUI.Label (new Rect (Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
        GUI.Label (new Rect (Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");*/
    }
}
