using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    private GameObject _boss;
    private HealthHelper _bossHealth;
    private BossAttack _bossAttack;
    private BossData _bossData;
    private BossRoundDamage _bossRoundDamage;
    private Drop _bossDrop;
    private GameObject _player;
    private HealthHelper _playerHealth;

    private float _mapBordersMinX = -10;
    private float _mapBordersMaxX = 10;
    private float _mapBordersMinZ = 5;
    private float _mapBordersMaxZ = 38;

    private void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Enemy");
        _bossHealth = _boss.GetComponent<HealthHelper>();
        _bossAttack = _boss.GetComponent<BossAttack>();
        _bossRoundDamage = FindObjectOfType<BossRoundDamage>();
        _bossHealth = _boss.GetComponent<HealthHelper>();
        _bossData = FindObjectOfType<LevelUp>().GetComponent<BossData>();
        _bossDrop = _boss.GetComponent<Drop>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<HealthHelper>();

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

        if(_bossHealth.Dead && _bossAttack)
        {
            Destroy(gameObject.GetComponent<Boss>());
        }
    }

    public override void Tactic()
    {
        if (!_player || _playerHealth.Dead || !_boss || _bossHealth.Dead 
            || !_bossAttack || _bossAttack.ReloadingAttack) 
            return;

        _bossAttack.ReloadingAttack = true;
        Move();
        StartCoroutine(WateShootAttack());
    }

    private IEnumerator WateShootAttack()
    {
        yield return new WaitForSeconds(8);

        if (!_player || _playerHealth.Dead || !_boss || _bossHealth.Dead)
            yield break;

        ShootAttack();
    }

    public override void ShootAttack()
    {
        _bossAttack.Attack();
    }

    public override void LevelUp()
    {
        _bossData.InvokeEventLevelUpBoss();
    }

    public override void DropThings()
    {
        _bossDrop.InvokeEventScatterCoins();
        _bossDrop.InvokeEventScatterHealth();
    }

    public override void Move()
    {
        DisappearAppear(_boss);
        _bossRoundDamage.StartRoundDamage();
    }

    private void DisappearAppear(GameObject boss)
    {
       boss.SetActive(false);
       boss.transform.position = new Vector3(Random.Range(_mapBordersMinX,_mapBordersMaxX), 0, Random.Range(_mapBordersMinZ,_mapBordersMaxZ)); 
       boss.SetActive(true);
    }

    public override void RotateToPlayer()
    {
        if (!_boss || _bossHealth.Dead || !_player || _playerHealth.Dead)
            return;

        Vector3 direction = _player.transform.position - _boss.transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        _boss.transform.rotation = Quaternion.Lerp(_boss.transform.rotation, lookrotation, Time.deltaTime * 5);
    }
}
