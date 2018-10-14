using UnityEngine;

public class WhiteShotControl : MonoBehaviour
{
    public float Speed;
    public float Damage;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyControl>().CauseDamage(Damage);
            Destroy(gameObject);
        }
    }
}