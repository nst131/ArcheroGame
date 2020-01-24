using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoisonScript : MonoBehaviour
{
    public float DamagePoison = 10; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(DamagePoison);
            Destroy(gameObject);
        }
    }
}
