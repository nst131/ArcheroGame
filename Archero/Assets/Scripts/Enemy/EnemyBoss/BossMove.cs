using UnityEngine;
using System.Collections;

public class BossMove : Enemy
{
    private GameObject _boss;
    private GameObject _player;
    private BossRoundDamage _bossRoundDamage;

    private float mapBordersMinX = -10;
    private float mapBordersMaxX = 10;
    private float mapBordersMinZ = 5;
    private float mapBordersMaxZ = 38;

    private void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
        _bossRoundDamage = FindObjectOfType<BossRoundDamage>();
    }

    private void Update()
    {
        RotateToPlayer();
        Tactic();
        RemoveScript();
    }

    private void RemoveScript()
    {
        if (!_boss)
            return;

        if(_boss.GetComponent<HealthHelper>().Dead && _boss.GetComponent<BossAttack>())
        {
            Destroy(gameObject.GetComponent<BossMove>());
        }
    }

    public override void Tactic()
    {
        if (!_player || _player.GetComponent<HealthHelper>().Dead || !_boss || _boss.GetComponent<HealthHelper>().Dead 
            || !_boss.GetComponent<BossAttack>() || _boss.GetComponent<BossAttack>().ReloadingAttack) 
            return;

        _boss.GetComponent<BossAttack>().ReloadingAttack = true;
        Move();
        StartCoroutine(WateShootAttack());
    }

    private IEnumerator WateShootAttack()
    {
        yield return new WaitForSeconds(8);

        if (!_player || _player.GetComponent<HealthHelper>().Dead || !_boss || _boss.GetComponent<HealthHelper>().Dead)
            yield break;

        ShootAttack();
    }

    public override void ShootAttack()
    {
        _boss.GetComponent<BossAttack>().Attack();
    }

    public override void Move()
    {
        DisappearAppear(_boss);
        _bossRoundDamage.StartRoundDamage();
    }

    private void DisappearAppear(GameObject boss)
    {
       boss.SetActive(false);
       boss.transform.position = new Vector3(Random.Range(mapBordersMinX,mapBordersMaxX), 0, Random.Range(mapBordersMinZ,mapBordersMaxZ)); 
       boss.SetActive(true);
    }

    public override void RotateToPlayer()
    {
        if (!_boss || _boss.GetComponent<HealthHelper>().Dead || !_player || _player.GetComponent<HealthHelper>().Dead)
            return;

        Vector3 direction = _player.transform.position - _boss.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _boss.transform.rotation = Quaternion.Lerp(_boss.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
