using System.Diagnostics;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // 체력 관련. 움직임. 추적. 공격.

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    float moveSpeed;

    Transform target;

    SpriteRenderer sr;

    // 공격 사정거리
    [SerializeField]float range;

    MonsterWeapon mWeapon;

    HPBar hpBar;

    private void OnEnable()
    {
        nowHP = 10;
        maxHP = 10;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 3f;
        sr = GetComponent<SpriteRenderer>();
        mWeapon=GetComponent<MonsterWeapon>();
        //TestCode
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position); // 나하고 타겟 사이의 거리

        if (distance<range)
        {
            // 공격 -> 적 방향으로 공격
            mWeapon.SetDirection(GetDirection());
            mWeapon.SetDistance(distance);
            mWeapon.CanAttack(true);
        }
        else
        {
            // 추적 -> 적 방향으로 이동
            mWeapon.CanAttack(false);
            Trace();
        }
    }

    void Trace()
    {
        // 방향 체크 -> x축 플립
        sr.flipX = CheckFlip();

        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

    }

    Vector2 GetDirection()
    {
        return target.position - transform.position;
    }

    bool CheckFlip()
    {
        return transform.position.x > target.position.x ? true : false;
    }

    public void TakeDamage(int damage)
    {
        nowHP -= damage;
        hpBar.SetGauge(nowHP / maxHP);
        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }

    void Die()
    {
        StageManager.instance.RemoveMonster(this.gameObject);
        ReturnPool();
    }

    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("Monster", this.gameObject);
    }

    public void SetHPBar(HPBar bar)
    {
        hpBar= bar;
    }


}
