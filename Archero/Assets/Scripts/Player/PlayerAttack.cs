using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private HealthHelper _playerHealth;
    [SerializeField] private Animator _playerAnim;
    private GameObject _enemy;
    private HealthHelper _enemyHealth;
    private Vector3 _currentPos;

    private bool _stayPos;
    private bool _reloadingAttack;
    private bool _firstAttack;

    [Header("DescriptionAttack")]
    [SerializeField] private float _forceShoot = 1500;
    [HideInInspector] public float _waitingAnimFirstAttack = 1.0f;
    [HideInInspector] public float _waitingAnimSecondAttack = 1.3f;

    private void Update()
    {
        ChooseEnemy();
        Aim();
        ReadyToShoot();
    }

    private void ChooseEnemy()
    {
         float MinInterval = 100;
         int IndexEnemy = 0;
         HealthHelper[] _enemies = GameObject.FindObjectsOfType<HealthHelper>().Where<HealthHelper>(p => !p.Dead && p.gameObject.tag=="Enemy").ToArray();

         for (int i = 0; i < _enemies.Length; i++)
         {
             if (Vector3.Distance(_player.transform.position,_enemies[i].transform.position) <= MinInterval)
             {
                 MinInterval = Vector3.Distance(_player.transform.position, _enemies[i].transform.position);
                 IndexEnemy = i;
             }
         }

         if(_enemies.Length>0)
         {
             _enemy = _enemies[IndexEnemy].gameObject;
            _enemyHealth = _enemies[IndexEnemy];
         }
         else
         {
             _enemy = null;
            _enemyHealth = null;
         }
    }

    private void Aim()
    {
        if (_currentPos == _player.transform.position)
        {
           if (!_enemy)
               return;

            _stayPos = true;
            RotateToTarget(_enemy);
        }
        else
        {
            _currentPos = _player.transform.position;
            _stayPos = false;
            _firstAttack = false;
            _reloadingAttack = false;
        }
    }

    private void ReadyToShoot()
    {
        if (_stayPos && !_reloadingAttack)
        {
            ShootAttack();
        }
        else if(!_enemy || _enemyHealth.Dead)
        {
            _playerAnim.SetBool("Damage", false);
        }
    }

    private void ShootAttack()
    {
        if (!_player || _playerHealth.Dead || !_enemy || _enemyHealth.Dead || _reloadingAttack)
            return;

        _playerAnim.SetBool("Damage",true);
        
        StartCoroutine(ExpactAnimAttack());
    }

    private IEnumerator ExpactAnimAttack()
    {
        _reloadingAttack = true;

        if (!_firstAttack)
        {
            yield return new WaitForSeconds(_waitingAnimFirstAttack);
        }
        else
        {
            yield return new WaitForSeconds(_waitingAnimSecondAttack);
        }

        if (!_player || _playerHealth.Dead || !_enemy || _enemyHealth.Dead)
            yield break;

        RaycastHit hit;
        Ray ray = new Ray(_player.transform.GetChild(2).position, _player.transform.GetChild(2).forward);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if(hit.collider.tag == "Enemy")
            {
                GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("PlayerShell/PlayerShell"), _player.transform.GetChild(2).position,
                       Quaternion.identity);
                arrow.transform.LookAt(_enemy.transform);
                arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * _forceShoot);
                Destroy(arrow.gameObject, 3);
                _firstAttack = true;
                _reloadingAttack = false;
            }
            else
            {
                _playerAnim.SetBool("Damage", false);
            }
        }
    }

    void RotateToTarget(GameObject Enemy)
    {
        if (!_player || _playerHealth.Dead)
            return;

        Vector3 direction = Enemy.transform.position - _player.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, lookrotation, Time.deltaTime*5);
    }   
}
