using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cam_Control : MonoBehaviour {


    private GameObject thingFollowing;
    private bool camfollow = false;
    public GameObject cam;

    public GameObject eat, move, hunt, reproduce, sleep, age;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // cam work
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Triangle")
                {
                    GameObject tringle = hitInfo.transform.gameObject;
                    Tringle_work tr = tringle.GetComponent<Tringle_work>();

                    eat.GetComponent<Text>().enabled = true;
                    move.GetComponent<Text>().enabled = true;
                    hunt.GetComponent<Text>().enabled = true;
                    reproduce.GetComponent<Text>().enabled = true;
                    sleep.GetComponent<Text>().enabled = true;
                    age.GetComponent<Text>().enabled = true;


                    eat.GetComponent<Text>().text = "Eat Stats: " + tr.eat;
                    move.GetComponent<Text>().text = "Move Stats: " + tr.move;
                    hunt.GetComponent<Text>().text = "Hunt Stats: " + tr.brawl;
                    reproduce.GetComponent<Text>().text = "Reproduce Stats: " + tr.reproduce;
                    sleep.GetComponent<Text>().text = "Sleep Stats: " + tr.dontMove;
                    age.GetComponent<Text>().text = "Age: " + tr.age;

                    camfollow = true;
                    thingFollowing = hitInfo.transform.gameObject;

                }
                else if(hitInfo.transform.gameObject.tag == "BackGround")
                {
                    camfollow = false;
                    eat.GetComponent<Text>().enabled = false;
                    move.GetComponent<Text>().enabled = false;
                    hunt.GetComponent<Text>().enabled = false;
                    reproduce.GetComponent<Text>().enabled = false;
                    sleep.GetComponent<Text>().enabled = false;
                    age.GetComponent<Text>().enabled = false;


                }

            }
        }
        if (camfollow == true)
        {
            cam.transform.position = thingFollowing.transform.position - new Vector3(0, 0, 70);

        }
        else
        {
            cam.transform.position = new Vector3(0, 0, -500);
        }



    }
}
