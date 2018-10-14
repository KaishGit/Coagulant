using UnityEngine;

public class EnemyShotControl : MonoBehaviour
{
    public float Speed;
    public float Damage;

    void Start()
    {
        Destroy(gameObject, 5);
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
        if (collision.CompareTag("White"))
        {
            collision.GetComponent<WhiteControl>().CauseDamage(Damage);
            Destroy(gameObject);
        }
    }
}