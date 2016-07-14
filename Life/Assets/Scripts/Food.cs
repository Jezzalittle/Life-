using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {


    public GameObject prefab;
    float timeLeft = 1.0f;

    void Update () {
        timeLeft = timeLeft - Time.deltaTime;
        if (timeLeft < 0)
        {
            Instantiate(prefab, new Vector3(Random.Range(25.0f, -25.0f), Random.Range(25.0f, -25.0f),-0.3f), Quaternion.identity);
            timeLeft = 1.0f;
        }
    }


}
