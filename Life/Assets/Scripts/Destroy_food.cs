using UnityEngine;
using System.Collections;

public class Destroy_food : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        Destroy(this.gameObject);

    }
}