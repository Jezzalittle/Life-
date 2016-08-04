using UnityEngine;
using System.Collections;

public class Cam_Control : MonoBehaviour {


    private GameObject thingFollowing;
    private bool camfollow = false;
    public GameObject cam;

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
                    camfollow = true;
                    thingFollowing = hitInfo.transform.gameObject;

                }
                else if(hitInfo.transform.gameObject.tag == "BackGround")
                {
                    camfollow = false;
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
