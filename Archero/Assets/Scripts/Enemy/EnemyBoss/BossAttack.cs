using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    private GameObject _boss;
    [SerializeField] private Animator _bossAnimator;
    [SerializeField] private HealthHelper _bossHealth;
    [SerializeField] private GameObject _bossFirstShell;
    [SerializeField] private GameObject _bossSecondShell;

    [Header("DescriptionAttack")]
    [SerializeField] private float _scatter = 20.0f;
    [SerializeField] private float _forceFirstShoot = 1500f;
    [SerializeField] private float _forceSecondShoot = 500f;
    [SerializeField] private float _forceClash = 20f;

    private bool _reloadingAttack;
    public bool ReloadingAttack { get { return _reloadingAttack; } set { _reloadingAttack = value; } }

    private void Start()
    {
        _boss = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
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
        if (!_boss || _bossHealth.Dead || !_player || _playerHealth.Dead)
            return;

        Vector3 directionAttack = new Vector3(_player.transform.position.x, _player.GetComponent<CapsuleCollider>().height, _player.transform.position.z);

        GameObject firstShell = Instantiate<GameObject>(_bossFirstShell, _boss.transform.GetChild(6).position,
            Quaternion.identity);
        firstShell.transform.LookAt(directionAttack);
        firstShell.GetComponent<Rigidbody>().AddForce(firstShell.transform.forward * _forceFirstShoot);
        Destroy(firstShell, 3f);
    }

    private void SecondAttack()
    {
        if (!_boss || _bossHealth.Dead || !_player || _playerHealth.Dead)
            return;

        Vector3 directionAttack = new Vector3(_player.transform.position.x, _player.GetComponent<CapsuleCollider>().height, _player.transform.position.z);

        for (int i = 0; i < 20; i++)
        {
            GameObject secondShell = Instantiate<GameObject>(_bossSecondShell, _boss.transform.GetChild(5).position,
                Quaternion.identity);
            secondShell.transform.LookAt(directionAttack + Random.insideUnitSphere * _scatter);
            secondShell.GetComponent<Rigidbody>().AddForce(secondShell.transform.forward * _forceSecondShoot);
            Destroy(secondShell, 3f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_forceClash);
        }
    }
}
