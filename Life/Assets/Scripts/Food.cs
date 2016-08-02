using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {


    public GameObject prefab;
    float timeLeft = 1.0f;

    void Update () {
        timeLeft = timeLeft - Time.deltaTime;
        if (timeLeft < 0)
        {
            Instantiate(prefab, new Vector3(Random.Range(200.5f, -200.5f), Random.Range(200.0f, -200.0f),-0.3f), Quaternion.identity);
            timeLeft = 1.0f;
        }
    }


}
