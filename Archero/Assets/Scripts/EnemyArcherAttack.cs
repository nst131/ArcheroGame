using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherAttack : MonoBehaviour
{
    private GameObject _Enemy;
    private Animator _animator;
    private GameObject _Player;
    private bool _Reloading = false;


    public float _Wate = 1.3f;
    public float Shoot = 2000;

    void Start()
    {
        _Enemy = GetComponentInParent<EnemyArcherMove>().gameObject;
        _Player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        //Debug.DrawRay(transform.position,transform.forward*1000, Color.red);  //Линия просмотра Атаки !!!
    }
   public void Damage()
    {
        if (!_Player || _Player.GetComponent<HealthHelper>().Dead  || _Enemy.GetComponent<HealthHelper>().Dead)
            return;

        _animator.SetBool("Damage", true);
        _Enemy.GetComponent<EnemyArcherMove>()._lineRenderer.enabled = true;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        _Reloading = true;
        yield return new WaitForSeconds(_Wate);

        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("EnemyArrow"), transform.position,
            Quaternion.Euler(-7, 0, 0));
        arrow.transform.LookAt(_Player.transform);
        arrow.transform.Rotate(-7, 0, 0);

        _Enemy.GetComponent<EnemyArcherMove>()._lineRenderer.enabled =false;

        arrow.AddComponent<Rigidbody>().AddForce(arrow.transform.forward * Shoot);
        Destroy(arrow.gameObject, 3);
        _Reloading = false;
        _animator.SetBool("Damage", false);
    }
   public void RotatetoTarget()
    {
        if (_Reloading || _Enemy.GetComponent<EnemyArcherMove>().Moving || _Enemy.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _Player.transform.position - _Enemy.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _Enemy.transform.rotation = Quaternion.Lerp(_Enemy.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
