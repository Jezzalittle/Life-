using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {


    public GameObject prefab;
    float timeLeft = 1.0f;

    void Update () {
        timeLeft = timeLeft - Time.deltaTime;
        if (timeLeft < 0)
        {
            Instantiate(prefab, new Vector3(Random.Range(600, -600), Random.Range(300, -300),-0.3f), Quaternion.identity);
            timeLeft = 1.0f;
        }
    }


}
