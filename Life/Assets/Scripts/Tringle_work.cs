using UnityEngine;
using System.Collections;

public class Tringle_work : MonoBehaviour {

    public int[] aiMovement;
    public GameObject spices;
    private int Desition = 5;
    private bool foodFound = false;
    private Quaternion rotNow, rotSoon;
    private Vector3 posNow, posSoon;
    public float movementSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;

    GameObject closest = null;
    
    public int decisionMaking(int eat, int move, int brawl, int reproduce, int dontMove)
    {
       int tempRand = Random.Range(1, 100);
       if(tempRand < eat)
        {
            return 0;
        }
       else if(tempRand < eat + move && tempRand > eat)
        {
            return 1;
        }
        else if (tempRand < eat + move + brawl && tempRand > move + eat)
        {
            return 2;
        }
        else if (tempRand < eat + move + brawl + reproduce && tempRand > brawl + eat + move)
        {
            return 3;
        }
        else if (tempRand < eat + move + brawl + reproduce + dontMove && tempRand > reproduce + eat + move + brawl)
        {
            return 4;
        }
       
        return 9;
    }

    void Start()
    {

    posNow = transform.position;
        posSoon = transform.position;
        rotNow = transform.rotation;
        rotSoon = transform.rotation;
    }


    void Update()
    {
 
        if (Desition == 5)
        {
            Desition = decisionMaking(20, 20, 20, 20, 20);
        }
       else if(Desition == 0)
        {
            //Eat
            if(foodFound == false)
            {
                findFood();
                foodFound = true;
            }
        }
        else if (Desition == 1)
        {
            //Move
            Desition = 5;
        }
      else  if (Desition == 2)
        {
            //Brawl
            Desition = 5;
        }
       else if (Desition == 3)
        {
            //Reproduce
            Desition = 5;
        }
      else if (Desition == 4)
        {
            //Dont Move
            Desition = 5;
        }

        Debug.Log(posSoon);
        Debug.Log(posNow);


       // transform.rotation = Quaternion.Lerp(rotNow, rotSoon, movementSpeed * Time.time);
      transform.position = Vector3.SmoothDamp(transform.position , posSoon, ref velocity, movementSpeed,1); ;
    }


    void findFood ()
    { 
        GameObject []  gos;
        gos = GameObject.FindGameObjectsWithTag("Food");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance<distance)
            {
                closest = go;
                distance = curDistance;
            }

        }

        float totx = closest.transform.position.x - spices.transform.position.x;
        float toty = closest.transform.position.y - spices.transform.position.y;
        rotSoon.Set(0, 0, Mathf.Atan2(toty, totx), 0);
        rotNow = transform.rotation;
        posSoon = closest.transform.position;
        posNow = transform.position;
        Debug.Log(posSoon);
        Debug.Log(rotSoon);
    }


    void OnTriggerEnter(Collider other)
    {

        Destroy(other);
        Desition = 5;
        foodFound = false;

    }

}
