using UnityEngine;
using System.Collections;

public class Tringle_work : MonoBehaviour {

    public int[] aiMovement;
    public GameObject spices;
    private int decision = 5;
    private bool foodFound = false;
    private Vector3 posSoon;
    public float movementSpeed = 1;
    private Vector3 velocity = Vector3.zero;
    private float gameTimer = 3.0f;
    public float age = 0;
    public GameObject cam;
    private float energyMax = 1, energyCur;
    GameObject closest = null;
    public Renderer rend;
    public float eat = 20, move = 20, brawl = 20, reproduce = 20, dontMove = 20;

    public bool getEnergy = false;

    public float perantDebuff;

    


    public int decisionMaking(float eat, float move, float brawl, float reproduce, float dontMove, int total)
    {
        int tempRand = Random.Range(1, total);

        if (tempRand < eat)
        {
            return 0;
        }
        else if (tempRand < eat + move && tempRand > eat)
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
        if (tempRand < stay)
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

        energyMax = -0.5f * (Mathf.Pow((age - 8), 2.0f)) + 200;
        posSoon = transform.position;

        getEnergy = true;

        energyCur = energyMax;
        
    }

    void Update()
    {

        if (gameTimer <= 0.0f)
            {

            transform.localScale += new Vector3(0.01f, 0.01f, 0);


            if (energyCur > energyMax)
            {
                energyCur = energyMax;
            }

            energyCur = (energyCur - 1) - perantDebuff;


            age = age + 1;
            energyMax = (-0.5f*(Mathf.Pow((age - 8), 2.0f)) + 200);
            if (energyMax <= 0 || energyCur <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("DEAD!");
            }


            
           rend.material.SetColor("_Color",Color.Lerp(Color.red, Color.green, energyCur / energyMax));

           

            if (decision == 5)
                {
                float i = eat + move + brawl + reproduce + dontMove;
                int j = (int)i;
    
                decision = decisionMaking(eat, move, brawl, reproduce, dontMove, j);
                print(decision);
            }

            print(energyCur);
            print(energyMax);
            print(age);

                if (decision == 0)
                {

                    //Eat
                    if (foodFound == false)
                    {
                        findFood();
                        foodFound = true;
                        energyCur = energyCur - 20;
                    eat = Mathf.Pow(eat, 1.005f);
                    decision = 5;

                    }
                }


                if (decision == 1)
                {
                    //Move
                  //  Debug.Log("1");
                decision = 5;
                posSoon = new Vector3(Random.Range(600, -600), Random.Range(300, -300), -0.3f);
                
                energyCur = energyCur - 10;

                move = Mathf.Pow(move, 1.005f);
                decision = 5;
            }


            if (decision == 2)
                {
                    //Brawl
                    //Debug.Log("2");
                    decision = 5;
                }


            if (decision == 3)
                {
                
                
                //Reproduce
                    Vector3 ogPos = transform.position;
                    Vector3 newPos = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 0);
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                   
                
                //  Debug.Log("3");
                    energyCur = energyCur - 50;
                    Instantiate(spices, ogPos + newPos, rot);
                    


                //incress chance
                reproduce = Mathf.Pow(reproduce, 1.005f);
                decision = 5;


                }
                if (decision == 4)
                {
                    //Dont Move
                    posSoon = transform.position;
                energyCur = energyCur - 10;
                //  Debug.Log("4");
                decision = 5;
                }

                if (decision == 9)
                {
                Debug.Log("error");
                decision = 5;
              }

                 /* while (decision != 5)
                  {
                      int continueCommitingChance = 70;
                      int StopCommitingChance = 30;
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
                  }*/

                gameTimer = 3.0f;

            }
            gameTimer = gameTimer - Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, posSoon, Time.deltaTime * movementSpeed);

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
    }

    void OnTriggerEnter(Collider other)
    {
        if (getEnergy == true)
        {
            if(other.gameObject.tag == "Triangle")
            {
                eat = other.gameObject.GetComponent<Tringle_work>().eat;
                move = other.gameObject.GetComponent<Tringle_work>().move;
                brawl = other.gameObject.GetComponent<Tringle_work>().brawl;
                reproduce = other.gameObject.GetComponent<Tringle_work>().reproduce;
                dontMove = other.gameObject.GetComponent<Tringle_work>().dontMove;

                perantDebuff = other.gameObject.GetComponent<Tringle_work>().energyCur;
                
                getEnergy = false;
            }

        }
            Debug.Log("hit!");
            Destroy(other.gameObject);
            decision = 5;
            foodFound = false;
            energyCur = energyCur + 5;



        if (other.gameObject != GameObject.FindGameObjectWithTag("Food") || other.gameObject != GameObject.FindGameObjectWithTag(gameObject.tag))
        {
          if(energyCur > other.GetComponent<Tringle_work>().energyCur)
            {
                Destroy(other.gameObject);
                energyCur = energyCur + 20;

            }
        }


    }

}
