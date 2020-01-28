using UnityEngine;
using System.Linq;

public class FortressGadeScript : MonoBehaviour
{
    private HealthHelper [] _everbodyDied;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _everbodyDied = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead).ToArray();
        if(_everbodyDied.Length==1)
        {
            _anim.SetBool("Open", true);
        }
    }
}
