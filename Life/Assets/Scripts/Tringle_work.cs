using UnityEngine;
using System.Collections;

public class Tringle_work : MonoBehaviour {

    public int[] aiMovement;
    public GameObject spices;
    private int decision = 5;
    private bool foodFound = false;
    private Vector3 posNow, posSoon;
    public float movementSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;
    private int continueCommitingChance = 100, StopCommitingChance = 0;
    private float gameTimer = 3.0f;

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

    public int desistionCommitment(int stay, int stop)
    {
        int tempRand = Random.Range(1, 100);
        if(tempRand < stay)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    void Start()
    {

        posNow = transform.position;
        posSoon = transform.position;


    }

    void Update()
    {
        if (gameTimer > 0)
        {
            if (decision == 5)
            {
                decision = decisionMaking(20, 20, 20, 20, 20);
            }
            else if (decision == 0)
            {
                //Eat
                if (foodFound == false)
                {
                    findFood();
                    foodFound = true;
                }
            }
            else if (decision == 1)
            {
                //Move
                decision = 5;
            }
            else if (decision == 2)
            {
                //Brawl
                decision = 5;
            }
            else if (decision == 3)
            {
                //Reproduce
                decision = 5;
            }
            else if (decision == 4)
            {
                //Dont Move
                decision = 5;
            }

            if (decision != 5)
            {
                int commitmentCheck = desistionCommitment(continueCommitingChance, StopCommitingChance);
                if (commitmentCheck == 0)
                {
                    continueCommitingChance =- 1;
                    StopCommitingChance =+ 1;
                }
                else
                {
                    decision = 5;
                }
            }
            Debug.Log(decision);
            gameTimer = 3.0f;
        }
        Debug.Log(gameTimer);
      gameTimer =- Time.deltaTime;
      transform.position = Vector3.SmoothDamp(transform.position , posSoon, ref velocity, movementSpeed,4);


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



        posSoon = closest.transform.position;
        posNow = transform.position;

    }

    void OnTriggerEnter(Collider other)
    {

        Destroy(other.gameObject);
        decision = 5;
        foodFound = false;

    }

}
