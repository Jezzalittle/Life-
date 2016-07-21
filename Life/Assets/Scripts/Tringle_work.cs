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
    private float age;

    GameObject closest = null;
    
    public int decisionMaking(float eat, float move, float brawl, float reproduce, float dontMove)
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
        if (gameTimer <= 0.0f)
        {
            if (decision == 5)
            {
                decision = decisionMaking(42.5f, 5, 5, 42.5f, 5);
            }
            else if (decision == 0)
            {
                Debug.Log("0");
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
                Debug.Log("1");
                decision = 5;
            }
            else if (decision == 2)
            {
                //Brawl
                Debug.Log("2");
                decision = 5;
            }
            else if (decision == 3)
            {
                //Reproduce
                Vector3 ogPos = transform.position;
                Vector3 newPos = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 0);
                Quaternion rot = new Quaternion(0, 0, 0, 0);
                Debug.Log("3");
                Instantiate(spices, ogPos + newPos, rot);
                decision = 5;
            }
            else if (decision == 4)
            {
                //Dont Move
                Debug.Log("4");
                decision = 5;
            }

          /*  if (decision != 5)
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
*/
            gameTimer = 3.0f;

        }

        gameTimer = gameTimer - Time.deltaTime;
      transform.position = Vector3.SmoothDamp(transform.position , posSoon, ref velocity, movementSpeed,4);
        age = age + Time.deltaTime;

        
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
        Debug.Log("reset");
        Debug.Log(gameTimer);

    }

}
