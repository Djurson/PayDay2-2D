using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public float damage;

    [SerializeField] private GameObject WoodBulletImpact;
    [SerializeField] private GameObject BloodParticleEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if(collision.contacts[0].normal.x == 1)
            {
                var instatiatedBulletImpact = Instantiate(WoodBulletImpact, transform.position, Quaternion.Euler(0f, 0f, 90f));
            } else if (collision.contacts[0].normal.x == -1)
            {
                var instatiatedBulletImpact = Instantiate(WoodBulletImpact, transform.position, Quaternion.Euler(0f, 0f, -90f));
            } else if (collision.contacts[0].normal.y == 1)
            {
                var instatiatedBulletImpact = Instantiate(WoodBulletImpact, transform.position, Quaternion.Euler(0f, 0f, 180f));
            }
            else
            {
                var instatiatedBulletImpact = Instantiate(WoodBulletImpact, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Civilian"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var instatiatedBulletImpact = Instantiate(BloodParticleEffect, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<localCivilianHandler>().DealDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("DeadCivilian"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var instatiatedBulletImpact = Instantiate(BloodParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Guard"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var instatiatedBulletImpact = Instantiate(BloodParticleEffect, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<LocalGuardHandler>().DealDamage(damage * 0.8f);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("DeadGuard"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var instatiatedBulletImpact = Instantiate(BloodParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
