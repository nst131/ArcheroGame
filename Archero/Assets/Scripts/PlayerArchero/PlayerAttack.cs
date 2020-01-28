using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    private GameObject _player;
    private GameObject _enemy;
    private Animator _anim;
    private Vector3 _currentPos;

    private bool stayPos;
    private bool reloadingAttack;
    private bool firstAttack;

    [SerializeField] private float waitingAnimFirstAttack = 1.0f;
    [SerializeField] private float waitingAnimSecondAttack = 1.3f;
    [SerializeField] private float forceShoot = 1500;

    private void Start()
    {
        _player = gameObject;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ChooseEnemy();
        Aim();
        ReadyToShoot();
    }

    private void ChooseEnemy()
    {
         float MinInterval = 0;
         int IndexEnemy = 0;
         HealthHelper[] _enemies = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead && p.gameObject.tag=="Enemy").ToArray();
        
         for (int i = 0; i < _enemies.Length; i++)
         {
             if (Vector3.Distance(_player.transform.position,_enemies[i].transform.position)<=MinInterval)
             {
                 MinInterval = Vector3.Distance(_player.transform.position, _enemies[i].transform.position);
                 IndexEnemy = i;
             }
         }

         if(_enemies.Length>0)
         {
             _enemy = _enemies[IndexEnemy].gameObject;
         }
         else
         {
             _enemy = null;
         }
    }

    private void Aim()
    {
        if (_currentPos == _player.transform.position)
        {
           if (!_enemy)
               return;

            stayPos = true;
            RotateToTarget(_enemy);
        }
        else
        {
            _currentPos = _player.transform.position;
            stayPos = false;
            firstAttack = false;
            reloadingAttack = false;
        }
    }

    private void ReadyToShoot()
    {
        if (stayPos && _enemy && !_enemy.GetComponent<HealthHelper>().Dead && !reloadingAttack)
        {
            ShootAttack();
        }
        else if(!_enemy || _enemy.GetComponent<HealthHelper>().Dead)
        {
            _anim.SetBool("Damage", false);
        }
    }

    private void ShootAttack()
    {
        if (!_player || _player.GetComponent<HealthHelper>().Dead || !_enemy)
            return;

        _anim.SetBool("Damage",true);
        
        StartCoroutine(ExpactAnimAttack(_enemy));
    }

    private IEnumerator ExpactAnimAttack(GameObject Enemy)
    {
        reloadingAttack = true;

        if (!firstAttack)
        {
            yield return new WaitForSeconds(waitingAnimFirstAttack);
        }
        else
        {
            yield return new WaitForSeconds(waitingAnimSecondAttack);
        }

        RaycastHit hit;
        Ray ray = new Ray(_player.transform.GetChild(2).position, _player.transform.GetChild(2).forward);

        if (Physics.Raycast(ray, out hit, 1000))
        {
           if (hit.collider.tag == "Enemy")
           {
                GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("PlayerShell"), _player.transform.GetChild(2).position,
                    Quaternion.identity);
                arrow.transform.LookAt(Enemy.transform);
                arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * forceShoot);
                Destroy(arrow.gameObject, 3);
                firstAttack = true;
                reloadingAttack = false;
           }
        }
    }

    void RotateToTarget(GameObject Enemy)
    {
        Vector3 direction = Enemy.transform.position - _player.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, lookrotation, Time.deltaTime*5);
    }   
}
