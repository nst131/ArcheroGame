using System.Collections;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    private GameObject _player;
    private HealthHelper _playerHealth;
    [SerializeField] private GameObject _archer;
    [SerializeField] private HealthHelper _archerHealth;
    [SerializeField] private Animator _archerAnim;
    [SerializeField] private LineRenderer _archerLineRenderer;
    [SerializeField] private GameObject _archerShell;

    [Header("DescriptionAttack")]
    [SerializeField] private float _wateTimeAnimation = 1.3f;
    [SerializeField] private float _wateTimeAiming = 2f;
    [SerializeField] private float _forceShoot = 2000;
    [SerializeField] private float _forceClash = 20;
    [HideInInspector] public bool ReloadingAttack = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();
    }

    public void Attack()
    {
        if (!_player || _playerHealth.Dead  || !_archer ||_archerHealth.Dead)
            return;

        StartCoroutine(ExplainAnimAttack());
    }

    IEnumerator ExplainAnimAttack()
    {
        _archerLineRenderer.enabled = true;

        yield return new WaitForSeconds(_wateTimeAiming);

        _archerAnim.SetBool("Damage", true);
        ReloadingAttack = true;

        yield return new WaitForSeconds(_wateTimeAnimation);

        if (!_player || _playerHealth.Dead || !_archer ||_archerHealth.Dead)
            yield break;

        GameObject arrow = Instantiate<GameObject>(_archerShell, transform.GetChild(2).position,
            Quaternion.identity);
        arrow.transform.rotation = Quaternion.LookRotation(arrow.transform.position - _archer.transform.position);
        arrow.transform.Rotate(55, 0, 0);

        _archerLineRenderer.enabled = false;

        arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * _forceShoot);
        Destroy(arrow.gameObject, 3);

        ReloadingAttack = false;
        _archerAnim.SetBool("Damage", false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthHelper>().TakeAwayHP(_forceClash);
        }
    }
}
