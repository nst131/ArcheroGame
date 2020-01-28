using System.Collections;
using UnityEngine;

public class EnemyGolemAttack : MonoBehaviour
{ 
    private GameObject _player;
    private Animator _anim;

    [SerializeField] private float wateAnimation = 1.2f;
    [SerializeField] private float forceShoot = 500f;

    [HideInInspector] public bool ReloadingAttack = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    public void ShootAttack()
    {
        if (!_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        _anim.SetBool("Damage", true);

        StartCoroutine(ExpactAnimAttack());
    }

    private IEnumerator ExpactAnimAttack()
    {
        ReloadingAttack = true;
        yield return new WaitForSeconds(wateAnimation);

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
        _anim.SetBool("Damage", false);
    }
}


