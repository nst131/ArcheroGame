using System.Collections;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    [SerializeField] private GameObject _golem;
    [SerializeField] private Animator _golemAnim;
    [SerializeField] private HealthHelper _golemHealth;

    [Header ("DesctiptionAttack")]
    [SerializeField] private float _wateAnimation = 1.8f;
    [SerializeField] private float _forceShoot = 500f;
    [HideInInspector] public bool ReloadingAttack = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
    }

    public void Attack()
    {
        if (!_player || _playerHealth.Dead || !_golem || _golemHealth.Dead)
            return;

        _golemAnim.SetBool("Damage", true);

        StartCoroutine(ExpactAnimAttack());
    }

    private IEnumerator ExpactAnimAttack()
    {
        ReloadingAttack = true;
        yield return new WaitForSeconds(_wateAnimation);

        if (!_player || _playerHealth.Dead || !_golem || _golemHealth.Dead)
            yield break;

        int g = 2;
        for (int i = 0; i < 3 ; i++)
        {
            GameObject rock = Instantiate<GameObject>(Resources.Load<GameObject>("BotsShell/GolemShell"), transform.GetChild(g).position,
            Quaternion.LookRotation(transform.GetChild(g).position - transform.position));
            rock.transform.Rotate(30, 0, 0);
            Destroy(rock.gameObject, 3);
            g += 1;

            rock.GetComponent<Rigidbody>().AddForce(rock.transform.forward * _forceShoot);
        }

        ReloadingAttack = false;
        _golemAnim.SetBool("Damage", false);
    }
}


