using System.Collections;
using UnityEngine;

public class EnemyArcherAttack : MonoBehaviour
{
    private GameObject _player;
    private GameObject _archer;
    private Animator _anim;
    private LineRenderer _lineRenderer;

    [HideInInspector] public bool ReloadingAttack = false;
    [SerializeField] private float wateAnimation = 1.3f;
    [SerializeField] private float forceShoot = 2000;

    private void Start()
    {
        _archer = gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Damage()
    {
        if (!_player || _player.GetComponent<HealthHelper>().Dead  || _archer.GetComponent<HealthHelper>().Dead)
            return;

        _anim.SetBool("Damage", true);
        _lineRenderer.enabled = true;
        StartCoroutine(ExplainAnimAttack());
    }

    IEnumerator ExplainAnimAttack()
    {
        ReloadingAttack = true;
        yield return new WaitForSeconds(wateAnimation);

        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("EnemyArcherShell"), transform.GetChild(2).position,
            Quaternion.identity);
        arrow.transform.LookAt(_player.transform);
        arrow.transform.Rotate(-7, 0, 0);

        _lineRenderer.enabled =false;

        arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * forceShoot);
        Destroy(arrow.gameObject, 3);

        ReloadingAttack = false;
        _anim.SetBool("Damage", false);
    }
}
