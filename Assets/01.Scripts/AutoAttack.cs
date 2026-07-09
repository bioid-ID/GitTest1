using System.Collections;
using System.Linq;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    float range;
    float delay = 2f;

    int damage;

    [SerializeField] Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        range = 4f;
        StartCoroutine(StartAutoAttack());
    }

    IEnumerator StartAutoAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while(true)
        {
            yield return new WaitUntil(() => DetectEnemy());// 적이 범위 안에 들어올 때까지)
            // 공격 
            // 타겟 방향 설정

            Attack();
            yield return wait;
        }
    }

    bool DetectEnemy()
    {
        int layer = LayerMask.NameToLayer("Monster");   // 9
        int targetLayer = 1 << layer;                   // 1<<9
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);

        if(collider.Length==0)
        {
            return false;
        }
        else
        {
            //float min = 0;
            
            //foreach(var col in collider)
            //{
            //    if(Vector2.Distance(col.transform.position, transform.position) < min)
            //    {
            //        min = Vector2.Distance(col.transform.position, transform.position);
            //        target = col.transform;
            //    }
            //}

            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;

            return true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.yellow;
        Gizmos.DrawSphere(transform.position, range);
    }

    void Attack()
    {
        // 투사체 생성
        GameObject arr = ObjectPoolManager.instance.GetObject("arrow");
        arr.transform.position = transform.position;

        Vector2 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        Quaternion rotate= Quaternion.Euler(0,0,angle);

        arr.transform.rotation = rotate;
        arr.GetComponent<Arrow>().SetDamage(5);
    }





}

// overlap
// 
// overlapcircleAll
// overlapboxAll
// 
// overlapsphere
// 
// 
// 
// 