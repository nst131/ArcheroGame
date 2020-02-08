using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    private GameObject _boss;
    private Animator _bossAnimator;
    private GameObject _player;

    [SerializeField] private float scatter = 2.0f;
    [SerializeField] private float forceFirstShoot = 1500f;
    [SerializeField] private float forceSecondShoot = 500f;

    private bool reloadingAttack;
    public bool ReloadingAttack { get { return reloadingAttack; } set { reloadingAttack = value; } }

    private void Start()
    {
        _boss = gameObject;
        _bossAnimator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Attack()
    {
        _bossAnimator.SetTrigger("Attack");
        FirstAttack();
        StartCoroutine(WateSecondAttack());
    }

    private IEnumerator WateSecondAttack()
    {
        yield return new WaitForSeconds(3);

        _bossAnimator.SetTrigger("Attack");
        SecondAttack();

        yield return new WaitForSeconds(2);
        ReloadingAttack = false;
    }

    private void FirstAttack()
    {
        if (!_boss || _boss.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 directionAttack = new Vector3(_player.transform.position.x, _player.GetComponent<CapsuleCollider>().height, _player.transform.position.z);

        GameObject firstShell = Instantiate<GameObject>(Resources.Load<GameObject>("BossFirstShell"), _boss.transform.GetChild(6).position,
            Quaternion.identity);
        firstShell.transform.LookAt(directionAttack);
        firstShell.GetComponent<Rigidbody>().AddForce(firstShell.transform.forward * forceFirstShoot);
        Destroy(firstShell, 3f);
    }

    private void SecondAttack()
    {
        if (!_boss || _boss.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 directionAttack = new Vector3(_player.transform.position.x, _player.GetComponent<CapsuleCollider>().height, _player.transform.position.z);

        for (int i = 0; i < 20; i++)
        {
            GameObject secondShell = Instantiate<GameObject>(Resources.Load<GameObject>("BossSecondShell"), _boss.transform.GetChild(5).position,
                Quaternion.identity);
            secondShell.transform.LookAt(directionAttack + Random.insideUnitSphere * scatter);
            secondShell.GetComponent<Rigidbody>().AddForce(secondShell.transform.forward * forceSecondShoot);
            Destroy(secondShell, 3f);
        }
    }
}
