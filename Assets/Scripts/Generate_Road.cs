using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Road : MonoBehaviour {

    public Transform Long_Straight_Road_3;
    public Transform Right_Turn;
    public Transform Left_Turn;
    public Transform Straight_Road_1;
    public Transform Bridge_Large;
    public Transform Bridge_Small_Right;
    public Transform Bridge_Small_Mid;
    public Transform Bridge_Small_Left;
    public Transform T_intersection;
    public Transform Final_Platform;
    public Transform Rock_1;
    public Transform Beachball;
    public Transform Shield;
    public Transform Coral_Rock1;
    public Transform Coral_Rock2;
    public Transform Coral_Rock3;
    public Transform Palm_4;

    public GameObject Rotater;

    public List<Transform> road;
    public List<Transform> road_left_temp;
    public List<Transform> road_right_temp;
    private Vector3 Current_Location;
    private Vector3 Current_Location_Right;
    private Vector3 Current_Location_Left;
    private int collide_times;
    private int generate_code;
    private int small_bridge_flag;
    private bool T_flag;
    private bool IsAlreadyDead;//Chan is already dead
    private int rock_flag;
    private int rock_pos;
    private int ynball; // if generate beachball

    //private Quaternion quat;
	// Use this for initialization
	void Start () {
        collide_times = 0;
        Rotater = GameObject.FindGameObjectWithTag("Rotater");
        Current_Location = new Vector3(0, 0, 0);
        small_bridge_flag = -2;
        T_flag = false;
        ynball = 0;
        IsAlreadyDead = false;

        Generate_Road_3(ref Current_Location);
        Generate_Road_3(ref Current_Location);
        Generate_Road_3(ref Current_Location);
    }
	
	// Update is called once per frame
	void Update () {
        if ((Chan_Controller.IsDead)&&(IsAlreadyDead == false))
        {
            IsAlreadyDead = true;
            while(road.Count != 0)
            {
                Destroy(road[0].gameObject);
                road.Remove(road[0]);
            }
            while(road_left_temp.Count!=0)
            {
                Destroy(road_left_temp[0].gameObject);
                road_left_temp.Remove(road_left_temp[0]);
            }
            while (road_right_temp.Count != 0)
            {
                Destroy(road_right_temp[0].gameObject);
                road_right_temp.Remove(road_right_temp[0]);
            }

            Transform final = Instantiate(Final_Platform);
            
        }
	}

    void Generate_Road_3(ref  Vector3  vec)
    {
        if (T_flag == false)
        {
            Transform temp = Instantiate(Long_Straight_Road_3);
            GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
            temp.parent = reference[0].transform;
            temp.localPosition = vec;

            if (collide_times < 1)
                vec += new Vector3(0, 0, 22.8f);
            else
            {
                temp.Rotate(Rotater.transform.eulerAngles);
                vec += (Rotater.transform.rotation) * new Vector3(0, 0, 22.8f);
            }
            road.Add(temp);

            //generate beachball

            Transform bb;
            ynball = Random.Range(1, 6);
            if (ynball >= 2)
            {
                int ball_x = Random.Range(-1, 2);
                for (int z = 1; z < 20; z += 2)
                {
                    bb = Instantiate(Beachball);
                    bb.parent = temp;
                    if (ball_x == -1)
                    {
                        bb.localPosition = new Vector3(-1.2f, 0.5f, z);
                    }
                    else if (ball_x == 0)
                    {
                        bb.localPosition = new Vector3(0, 0.5f, z);
                    }
                    else if (ball_x == 1)
                    {
                        bb.localPosition = new Vector3(1.2f, 0.5f, z);
                    }
                }
            }

            //generate nature beauty
            int nature_beauty_index = Random.Range(1, 3);
            if(nature_beauty_index == 1)
            {
                Generate_Coral_Rock(temp);
            }
            else if (nature_beauty_index == 2)
            {
                Generate_Palm(temp);
            }

             //generate_shield
             Generate_shield(temp);
        }
        else
        {
            Transform temp_l = Instantiate(Long_Straight_Road_3);
            Transform temp_r = Instantiate(Long_Straight_Road_3);
            GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
            temp_l.parent = reference[0].transform;
            temp_r.parent = reference[0].transform;
            temp_l.localPosition = Current_Location_Left;
            temp_r.localPosition = Current_Location_Right;
            if (collide_times < 1)
            {
                Current_Location_Left += new Vector3(0, 0, 22.8f);
                Current_Location_Right += new Vector3(0, 0, 22.8f);
            }
            else
            {
                temp_l.Rotate(Rotater.transform.eulerAngles);
                temp_r.Rotate(Rotater.transform.eulerAngles + new Vector3 (0,180,0));
                Current_Location_Left += (Rotater.transform.rotation) * new Vector3(0, 0, 22.8f);
                Current_Location_Right += (Rotater.transform.rotation * Quaternion.Euler(0, 180, 0)) * new Vector3(0, 0, 22.8f);
            }
            road_left_temp.Add(temp_l);
            road_right_temp.Add(temp_r);

            //generate nature beauty
            int nature_beauty_index = Random.Range(1, 3);
            if (nature_beauty_index == 1)
            {
                Generate_Coral_Rock(temp_l);
                Generate_Coral_Rock(temp_r);
            }
            else if (nature_beauty_index == 2)
            {
                Generate_Palm(temp_l);
                Generate_Palm(temp_r);
            }
        }       
    }

    void Generate_Right_Turn(ref Vector3 vec)
    {
        if (T_flag == false)
        {
            Transform temp = Instantiate(Right_Turn);
            GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
            temp.parent = reference[0].transform;
            temp.localPosition = vec;

            //generate beachball
            Transform bb;
            ynball = Random.Range(1, 6);
            if (ynball >= 2)
            {
                int ball_x = Random.Range(-1, 2);
                for (int z = 1; z < 6; z += 2)
                {
                    bb = Instantiate(Beachball);
                    bb.parent = temp;
                    if (ball_x == -1)
                    {
                        bb.localPosition = new Vector3(-1.2f, 0.5f, z);
                    }
                    else if (ball_x == 0)
                    {
                        bb.localPosition = new Vector3(0, 0.5f, z);
                    }
                    else if (ball_x == 1)
                    {
                        bb.localPosition = new Vector3(1.2f, 0.5f, z);
                    }
                }
            }

            if (collide_times < 1)
            {
                vec += new Vector3(1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
            else
            {
                temp.Rotate(Rotater.transform.eulerAngles);
                vec += Rotater.transform.rotation * new Vector3(1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
            road.Add(temp);

            //generate rock
            Transform rock;
            rock_flag = Random.Range(1, 4);
            if (rock_flag == 1)
            {
                rock_pos = Random.Range(-1, 2);
                rock = Instantiate(Rock_1);
                rock.parent = temp;
                if (rock_pos == -1)
                    rock.localPosition = new Vector3(-1.2f, 0, 1);
                else if (rock_pos == 0)
                    rock.localPosition = new Vector3(0, 0, 1);
                else if (rock_pos == 1)
                    rock.localPosition = new Vector3(1.2f, 0, 1);
            }

            //generate nature beauty
            int nature_beauty_index = Random.Range(1, 3);
            if (nature_beauty_index == 1)
            {
                Generate_Coral_Rock(temp);
            }
            else if (nature_beauty_index == 2)
            {
                Generate_Palm(temp);
            }

            //generate_shield
            Generate_shield(temp);
        }
        else
        {
            Transform temp_l = Instantiate(Right_Turn);
            Transform temp_r = Instantiate(Right_Turn);
            GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
            temp_l.parent = reference[0].transform;
            temp_r.parent = reference[0].transform;
            temp_l.localPosition = Current_Location_Left;
            temp_r.localPosition = Current_Location_Right;

            Transform rock_l;
            rock_flag = Random.Range(1, 3);
            if (rock_flag == 1)
            {
                rock_pos = Random.Range(-1, 2);
                rock_l = Instantiate(Rock_1);
                rock_l.parent = temp_l;
                if (rock_pos == -1)
                    rock_l.localPosition = new Vector3(-1.2f, 0, 1);
                else if (rock_pos == 0)
                    rock_l.localPosition = new Vector3(0, 0, 1);
                else if (rock_pos == 1)
                    rock_l.localPosition = new Vector3(1.2f, 0, 1);
            }

            Transform rock_r;
            rock_flag = Random.Range(1, 3);
            if (rock_flag == 1)
            {
                rock_pos = Random.Range(-1, 2);
                rock_r = Instantiate(Rock_1);
                rock_r.parent = temp_r;
                if (rock_pos == -1)
                    rock_r.localPosition = new Vector3(-1.2f, 0, 1);
                else if (rock_pos == 0)
                    rock_r.localPosition = new Vector3(0, 0, 1);
                else if (rock_pos == 1)
                    rock_r.localPosition = new Vector3(1.2f, 0, 1);
            }

            if (collide_times < 1)
            {
                Current_Location_Left += new Vector3(1.9f, 0, 5.7f);
                Current_Location_Right += new Vector3(1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
            else
            {
                temp_l.Rotate(Rotater.transform.eulerAngles);
                temp_r.Rotate(Rotater.transform.eulerAngles + new Vector3(0, 180, 0));
                Current_Location_Left += (Rotater.transform.rotation) * new Vector3(1.9f, 0, 5.7f);
                Current_Location_Right += (Rotater.transform.rotation * Quaternion.Euler(0, 180, 0)) * new Vector3(1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
            road_left_temp.Add(temp_l);
            road_right_temp.Add(temp_r);
        }
    }

    void Generate_Left_Turn(ref Vector3 vec)
    {
        if (T_flag == false)
        {
            Transform temp = Instantiate(Left_Turn);
            GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
            temp.parent = reference[0].transform;
            temp.localPosition = vec;

            //generate beachball
            Transform bb;
            ynball = Random.Range(1, 6);
            if (ynball >= 2)
            {
                int ball_x = Random.Range(-1, 2);
                for (int z = 1; z < 6; z += 2)
                {
                    bb = Instantiate(Beachball);
                    bb.parent = temp;
                    if (ball_x == -1)
                    {
                        bb.localPosition = new Vector3(-1.2f, 0.5f, z);
                    }
                    else if (ball_x == 0)
                    {
                        bb.localPosition = new Vector3(0, 0.5f, z);
                    }
                    else if (ball_x == 1)
                    {
                        bb.localPosition = new Vector3(1.2f, 0.5f, z);
                    }
                }
            }

           

            if (collide_times < 1)
            {
                vec += new Vector3(-1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
            else
            {
                temp.Rotate(Rotater.transform.eulerAngles);
                vec += Rotater.transform.rotation * new Vector3(-1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
            road.Add(temp);

            //generate rock
            Transform rock;
            rock_flag = Random.Range(1, 4);
            if (rock_flag == 1)
            {
                rock_pos = Random.Range(-1, 2);
                rock = Instantiate(Rock_1);
                rock.parent = temp;
                if (rock_pos == -1)
                    rock.localPosition = new Vector3(-1.2f, 0, 1);
                else if (rock_pos == 0)
                    rock.localPosition = new Vector3(0, 0, 1);
                else if (rock_pos == 1)
                    rock.localPosition = new Vector3(1.2f, 0, 1);
            }

            //generate nature beauty
            int nature_beauty_index = Random.Range(1, 3);
            if (nature_beauty_index == 1)
            {
                Generate_Coral_Rock(temp);
            }
            else if (nature_beauty_index == 2)
            {
                Generate_Palm(temp);
            }

            //generate_shield
            Generate_shield(temp);
        }
        else
        {
            Transform temp_l = Instantiate(Left_Turn);
            Transform temp_r = Instantiate(Left_Turn);
            GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
            temp_l.parent = reference[0].transform;
            temp_r.parent = reference[0].transform;
            temp_l.localPosition = Current_Location_Left;
            temp_r.localPosition = Current_Location_Right;

            Transform rock_l;
            rock_flag = Random.Range(1, 3);
            if (rock_flag == 1)
            {
                rock_pos = Random.Range(-1, 2);
                rock_l = Instantiate(Rock_1);
                rock_l.parent = temp_l;
                if (rock_pos == -1)
                    rock_l.localPosition = new Vector3(-1.2f, 0, 1);
                else if (rock_pos == 0)
                    rock_l.localPosition = new Vector3(0, 0, 1);
                else if (rock_pos == 1)
                    rock_l.localPosition = new Vector3(1.2f, 0, 1);
            }

            Transform rock_r;
            rock_flag = Random.Range(1, 3);
            if (rock_flag == 1)
            {
                rock_pos = Random.Range(-1, 2);
                rock_r = Instantiate(Rock_1);
                rock_r.parent = temp_r;
                if (rock_pos == -1)
                    rock_r.localPosition = new Vector3(-1.2f, 0, 1);
                else if (rock_pos == 0)
                    rock_r.localPosition = new Vector3(0, 0, 1);
                else if (rock_pos == 1)
                    rock_r.localPosition = new Vector3(1.2f, 0, 1);
            }

            if (collide_times < 1)
            {
                Current_Location_Left += new Vector3(-1.9f, 0, 5.7f);
                Current_Location_Right += new Vector3(-1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
            else
            {
                temp_l.Rotate(Rotater.transform.eulerAngles);
                temp_r.Rotate(Rotater.transform.eulerAngles + new Vector3(0, 180, 0));
                Current_Location_Left += (Rotater.transform.rotation) * new Vector3(-1.9f, 0, 5.7f);
                Current_Location_Right += (Rotater.transform.rotation * Quaternion.Euler(0, 180, 0)) * new Vector3(-1.9f, 0, 5.7f);
                Rotater.transform.rotation *= Quaternion.Euler(0, -90, 0);
            }
            road_left_temp.Add(temp_l);
            road_right_temp.Add(temp_r);
        }
    }

    void Generate_Straight_Road_1(ref Vector3 vec)
    {

        Transform temp = Instantiate(Straight_Road_1);      
        GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
        temp.parent = reference[0].transform;
        temp.localPosition = vec;

        //generate beachball
        Transform bb;
        ynball = Random.Range(1, 6);
        if (ynball >= 2)
        {
            int ball_x = Random.Range(-1, 2);
            for (int z = 1; z < 8; z += 2)
            {
                bb = Instantiate(Beachball);
                bb.parent = temp;
                if (ball_x == -1)
                {
                    bb.localPosition = new Vector3(-1.2f, 0.5f, z);
                }
                else if (ball_x == 0)
                {
                    bb.localPosition = new Vector3(0, 0.5f, z);
                }
                else if (ball_x == 1)
                {
                    bb.localPosition = new Vector3(1.2f, 0.5f, z);
                }
            }
        }

        //generate rock
        Transform rock;
        rock_flag = Random.Range(1, 3);
        if(rock_flag == 1)
        {
            rock_pos = Random.Range(-1, 2);
            rock = Instantiate(Rock_1);
            rock.parent = temp;
            if(rock_pos == -1)
                rock.localPosition =  new Vector3(-1.2f, 0, 3.54f); 
            else if(rock_pos == 0)
                rock.localPosition = new Vector3(0, 0, 3.54f);
            else if(rock_pos == 1)
                rock.localPosition = new Vector3(1.2f, 0, 3.54f);
        }

        if (collide_times < 1)
            vec += new Vector3(0, 0, 7.6f);
        else
        {
            temp.Rotate(Rotater.transform.eulerAngles);
            vec += (Rotater.transform.rotation) * new Vector3(0, 0, 7.6f);
        }
        road.Add(temp);

        //generate nature beauty
        int nature_beauty_index = Random.Range(1, 3);
        if (nature_beauty_index == 1)
        {
            Generate_Coral_Rock(temp);
        }
        else if (nature_beauty_index == 2)
        {
            Generate_Palm(temp);
        }

        //generate_shield
        Generate_shield(temp);
    }

    void Generate_Bridge_Large(ref Vector3 vec)
    {
        Transform temp = Instantiate(Bridge_Large);
        GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
        temp.parent = reference[0].transform;
        temp.localPosition = vec;

        //adjust location
        if (collide_times < 1)
            vec += new Vector3(0, 0, 7.6f);
        else
        {
            temp.Rotate(Rotater.transform.eulerAngles);
            vec += (Rotater.transform.rotation) * new Vector3(0, 0, 7.6f);
        }
        road.Add(temp);

        //generate Beachball
        Transform bb;
        ynball = Random.Range(1, 6);
        if (ynball >= 2)
        {
            int ball_x = Random.Range(-1, 2);
            for (int z = 1; z < 6; z += 2)
            {
                bb = Instantiate(Beachball);
                bb.parent = temp;
                if (ball_x == -1)
                {
                    bb.localPosition = new Vector3(-1.2f, 0.5f, z);
                }
                else if (ball_x == 0)
                {
                    bb.localPosition = new Vector3(0, 0.5f, z);
                }
                else if (ball_x == 1)
                {
                    bb.localPosition = new Vector3(1.2f, 0.5f, z);
                }
            }
        }

        //generate nature beauty
        int nature_beauty_index = Random.Range(1, 3);
        if (nature_beauty_index == 1)
        {
            Generate_Coral_Rock(temp);
        }
        else if (nature_beauty_index == 2)
        {
            Generate_Palm(temp);
        }

        //generate_shield
        Generate_shield(temp);
    }

    void Generate_Bridge_Small(ref Vector3 vec)
    {
        int small_bridge_flag_temp = Random.Range(-1,2);
        if ((small_bridge_flag_temp != small_bridge_flag) && (small_bridge_flag != -2))
            small_bridge_flag_temp = small_bridge_flag;

        small_bridge_flag = small_bridge_flag_temp;
        Transform temp;

        if(small_bridge_flag == 1)
            temp = Instantiate(Bridge_Small_Right);
        else if (small_bridge_flag == 0)
            temp = Instantiate(Bridge_Small_Mid);
        else
            temp = Instantiate(Bridge_Small_Left);

        GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
        temp.parent = reference[0].transform;
        temp.localPosition = vec;
        if (collide_times < 1)
            vec += new Vector3(0, 0, 7.6f);
        else
        {
            temp.Rotate(Rotater.transform.eulerAngles);
            vec += (Rotater.transform.rotation) * new Vector3(0, 0, 7.6f);
        }
        road.Add(temp);

        //generate beachball
        Transform bb;
        ynball = Random.Range(1, 6);
        if (ynball >= 2)
        {
            for (int z = 1; z < 6; z += 2)
            {
                bb = Instantiate(Beachball);
                bb.parent = temp;
                if (small_bridge_flag == -1)
                {
                    bb.localPosition = new Vector3(-1.2f, 0.5f, z);
                }
                else if (small_bridge_flag == 0)
                {
                    bb.localPosition = new Vector3(0, 0.5f, z);
                }
                else if (small_bridge_flag == 1)
                {
                    bb.localPosition = new Vector3(1.2f, 0.5f, z);
                }
            }
        }      
    }

    void Generate_T_intersection(ref Vector3 vec)
    {
        Transform temp = Instantiate(T_intersection);
        GameObject[] reference = GameObject.FindGameObjectsWithTag("Reference");
        temp.parent = reference[0].transform;
        temp.localPosition = vec;
        if (collide_times < 1)
        {
            Current_Location_Left = vec + new Vector3(-1.9f, 0, 5.7f);
            Current_Location_Right =  vec + new Vector3(1.9f, 0, 5.7f);
            Rotater.transform.rotation *= Quaternion.Euler(0, -90, 0);
        }
        else
        {
            temp.Rotate(Rotater.transform.eulerAngles);
            Current_Location_Left = vec + (Rotater.transform.rotation) * new Vector3(-1.9f, 0, 5.7f);
            Current_Location_Right = vec + (Rotater.transform.rotation ) * new Vector3(1.9f, 0, 5.7f);
            Rotater.transform.rotation *= Quaternion.Euler(0, -90, 0);
        }
        road.Add(temp);
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Trigger_Door") || (other.gameObject.tag == "Trigger_Door_Canturn"))
        {
            if (T_flag == false)
            {
                generate_code = Random.Range(1, 8);
                if (generate_code == 1)
                    Generate_Road_3(ref Current_Location);
                else if (generate_code == 2)
                    Generate_Right_Turn(ref Current_Location);
                else if (generate_code == 3)
                    Generate_Left_Turn(ref Current_Location);
                else if (generate_code == 4)
                    Generate_Straight_Road_1(ref Current_Location);
                else if (generate_code == 5)
                    Generate_Bridge_Large(ref Current_Location);
                else if (generate_code == 6)
                    Generate_Bridge_Small(ref Current_Location);
                else if (generate_code == 7)
                {
                    T_flag = true;
                    Generate_T_intersection(ref Current_Location);
                }
            }
            else
            {
                generate_code = Random.Range(1, 4);
                if (generate_code == 1)
                {
                    Generate_Road_3(ref Current_Location);
                }
                else if (generate_code == 2)
                {
                    Generate_Right_Turn(ref Current_Location);
                }
                else if (generate_code == 3)
                {
                    Generate_Left_Turn(ref Current_Location);
                }
            }

            if (generate_code != 6)
                small_bridge_flag = -2;

            if (collide_times < 1)
            {
                collide_times++;
            }
            else
            {
                Destroy(road[0].gameObject);
                road.Remove(road[0]);
            }
        }

        if (other.gameObject.tag == "Trigger_Door_L")
        {
            foreach (Transform t in road_right_temp)
            {
                Destroy(t.gameObject);
            }
            road_right_temp.Clear();
            Current_Location = Current_Location_Left;
            foreach (Transform t in road_left_temp)
            {
                road.Add(t);
            }
            road_left_temp.Clear();
            T_flag = false;
        }

        if (other.gameObject.tag == "Trigger_Door_R")
        {
            foreach (Transform t in road_left_temp)
            {
                Destroy(t.gameObject);
            }
            road_left_temp.Clear();
            Current_Location = Current_Location_Right;
            Rotater.transform.rotation *= Quaternion.Euler(0, 180, 0);
            foreach (Transform t in road_right_temp)
            {
                road.Add(t);
            }
            road_right_temp.Clear();
            T_flag = false;
        }       
    }

    void Generate_shield(Transform road)
    {
        int index = Random.Range(1, 11);
        if ((index == 2)&&(!Chan_Controller.shield.activeSelf )&&(collide_times>=1))
        {
            Transform shield = Instantiate(Shield);
            shield.parent = road;
            shield.localPosition = new Vector3(0, 0.6f, 1);
        }
    }

    void Generate_Coral_Rock(Transform road)
    {
        int index = Random.Range(1, 4);//choose rock type
        int position = Random.Range(5, 13);
        Transform rock;
        if (index == 1)
        {
            rock = Instantiate(Coral_Rock1);
        }
        else if(index == 2)
        {
            rock = Instantiate(Coral_Rock2);
        }
        else
        {
            rock = Instantiate(Coral_Rock3);
        }
        rock.parent = road;
        index = Random.Range(1, 3);//left or right
        if(index == 1)
        {
            rock.localPosition = new Vector3(-position, -3, 0);
        }
        else if(index == 2)
        {
            rock.localPosition = new Vector3(position, -3, 0);
        }
    }

    void Generate_Palm(Transform road)
    {
        Transform tree = Instantiate(Palm_4);
        tree.parent = road;
        int index = Random.Range(1, 3);//left or right
        if (index == 1)
        {
            tree.localPosition = new Vector3(-4, 0, 1);
        }
        else if (index == 2)
        {
            tree.localPosition = new Vector3(4, 0, 1);
        }             
    }
}


