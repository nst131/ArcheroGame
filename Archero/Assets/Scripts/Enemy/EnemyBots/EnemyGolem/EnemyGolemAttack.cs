using System.Collections;
using UnityEngine;

public class EnemyGolemAttack : MonoBehaviour
{
    private GameObject _golem;
    private GameObject _player;
    private Animator _animGolem;

    [SerializeField] private float wateAnimation = 1.2f;
    [SerializeField] private float forceShoot = 500f;

    [HideInInspector] public bool ReloadingAttack = false;

    private void Start()
    {
        _golem = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _animGolem = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (!_player || _player.GetComponent<HealthHelper>().Dead || !_golem || _golem.GetComponent<HealthHelper>().Dead)
            return;

        _animGolem.SetBool("Damage", true);

        StartCoroutine(ExpactAnimAttack());
    }

    private IEnumerator ExpactAnimAttack()
    {
        ReloadingAttack = true;
        yield return new WaitForSeconds(wateAnimation);

        if (!_player || _player.GetComponent<HealthHelper>().Dead || !_golem || _golem.GetComponent<HealthHelper>().Dead)
            yield break;

        int g = 2;
        for (int i = 0; i < 3 ; i++)
        {
            GameObject rock = Instantiate<GameObject>(Resources.Load<GameObject>("EnemyGolemShell"), transform.GetChild(g).position,
            Quaternion.LookRotation(transform.GetChild(g).position - transform.position));
            rock.transform.Rotate(30, 0, 0);
            Destroy(rock.gameObject, 3);
            g += 1;

            rock.GetComponent<Rigidbody>().AddForce(rock.transform.forward * forceShoot);
        }

        ReloadingAttack = false;
        _animGolem.SetBool("Damage", false);
    }
}


