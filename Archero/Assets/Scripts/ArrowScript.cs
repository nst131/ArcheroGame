using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float DamageArrow = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay (Collider other)
    {
        if(other.tag=="Enemy")
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(DamageArrow);
            Destroy(gameObject);
        }
    }
}
