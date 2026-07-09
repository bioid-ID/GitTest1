using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float lifeTime;
    float timer;

    Rigidbody2D rb;

    Vector2 dir;

    int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        lifeTime = 3f;
        timer = 0f;
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
    }
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= lifeTime)
        {
            ReturnPool();
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        rb.linearVelocity = dir * speed;

        // È¸Àü
        rb.MoveRotation(rb.rotation + -180f * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") )
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            ReturnPool();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
    }

    void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("Axe", this.gameObject);
        transform.rotation = Quaternion.identity;
    }
}
