using System.Collections;
using UnityEngine;

public class MonsterRangedWeapon : MonsterWeapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 데미지
        // 레인지 초기화


    }

    private void OnEnable()
    {
        Coroutine attackCoroutine = StartCoroutine(AttackDelay());
        
    }

    public void StartAttack()
    {

    }

    IEnumerator AttackDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while(true)
        {
            // 사거리 안에 들어올때까지 공격 대기
            // 사거리 안에 들어오면 WaitUntil 넘어감
            // 공격
            // 쿨타임 대기
            // 쿨타임 종료 후 사거리 안에 들어올때까지 대기
            // 반복
            yield return new WaitUntil(() => canAttack);
            Attack();
            yield return wait;
        }
    }

    protected override void Attack()
    {
        // 투사체 생성
        // 투사체는 자동으로 날아감
        GameObject axe = ObjectPoolManager.instance.GetObject("Axe");
        axe.transform.position = transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        axe.GetComponent<Axe>().SetDirection(dir);
        axe.GetComponent<Axe>().SetDamage(3);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
