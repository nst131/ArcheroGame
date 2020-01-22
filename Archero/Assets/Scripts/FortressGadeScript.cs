using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FortressGadeScript : MonoBehaviour
{
    private HealthHelper [] IamAlone;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        IamAlone = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead).ToArray();
        if(IamAlone.Length==1)
        {
            _anim.SetBool("Open", true);
        }
    }
}
