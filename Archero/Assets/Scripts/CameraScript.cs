using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Speed = 5;

    private GameObject target;
    private Vector3 different;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
        different = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = different + target.transform.position;
        Vector3 pos = new Vector3(target.transform.position.x,transform.position.y, target.transform.position.z-5);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position+different, Speed*Time.deltaTime);

        //transform.position = Vector3.MoveTowards(transform.position, pos, Speed *Time.deltaTime);
    }
}
