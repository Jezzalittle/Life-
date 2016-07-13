using UnityEngine;
using System.Collections;

public class Tringle_work : MonoBehaviour {

    public int[] aiMovement;
    public GameObject spices;
    public float startEatTime = 5.0f;

    public int decisionMaking(int eat, int move, int brawl, int reproduce, int dontMove)
    {
       int tempRand = Random.Range(0, 99);
       if(tempRand < eat)
        {
            return 0;
        }
       else if(tempRand < eat + move && tempRand > eat)
        {
            return 1;
        }
        else if (tempRand < eat + move + brawl && tempRand > move)
        {
            return 2;
        }
        else if (tempRand < eat + move + brawl + reproduce && tempRand > brawl)
        {
            return 3;
        }
        else if (tempRand < eat + move + brawl + reproduce + dontMove && tempRand > reproduce)
        {
            return 4;
        }
        return 9;
    }


    void Update () {



        startEatTime = startEatTime - Time.deltaTime;
        
        //nearest food
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Food");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;

                    Quaternion rotation = Quaternion.LookRotation
                    (closest.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                    Quaternion soon = new Quaternion(0, 0, rotation.z, rotation.w);
                    Quaternion now = this.transform.rotation;
                    transform.rotation = Quaternion.Lerp(now, soon, 0.1f);
                    transform.position = Vector3.Lerp(transform.position, closest.transform.position, 0.1f);
                
            }
        }

    }
}
