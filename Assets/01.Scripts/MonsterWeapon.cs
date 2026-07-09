using UnityEngine;

public abstract class MonsterWeapon : MonoBehaviour
{
    [SerializeField] protected int damage;

    [SerializeField] protected int range;

    [SerializeField] protected int delay;

    protected float distance;
    protected Vector2 dir;
    protected bool canAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDistance(float dis)
    {
        distance = dis;
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }
    public void CanAttack(bool canAtt)
    {
        canAttack = canAtt;
    }

    protected abstract void Attack();

}
