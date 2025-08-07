using UnityEngine;

public enum BombType { Normal, Smoke, Scatter, Super }

[RequireComponent(typeof(Collider2D))]
public class Bomb : MonoBehaviour
{
    public BombType bombType;
    public LayerMask smokeLayer;
    public float scatterSpeed = 2f;
    public GameObject smokeCloud;
    public GameObject miniBombPrefab;
    public float    superRadius     = 2f;
    public float    explosionDamage = 1f;
    public float fallSpeed = 1f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    void OnMouseDown()
    {
        Vector2 tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Physics2D.OverlapPoint(tapPos, smokeLayer) != null)
            return;

        switch (bombType)
        {
            case BombType.Normal:
                GameManager.Instance.AddScore(5);
                Destroy(gameObject);
                break;

            case BombType.Smoke:
                Instantiate(smokeCloud, transform.position, Quaternion.identity);
                if (smokeCloud != null)
                {
                    Instantiate(smokeCloud, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
                break;

            case BombType.Scatter:
                for (int i = 0; i < 3; i++)
                {
                    var mini = Instantiate(
                        miniBombPrefab,
                        transform.position,
                        Quaternion.identity
                    );
                    var dir = Quaternion.Euler(0, 0, i * 120f) * Vector2.up;
                    mini.GetComponent<Rigidbody2D>().linearVelocity = dir * scatterSpeed;
                }
                Destroy(gameObject);
                break;

            case BombType.Super:
                // find bombs within radius
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, superRadius);
                foreach (var hit in hits)
                {
                    var b = hit.GetComponent<Bomb>();
                    if (b != null && b != this)
                    {
                        Destroy(b.gameObject);
                        GameManager.Instance.AddScore(20);
                    }
                }
                // self‐damage if explosion near ship
                float distToShip = Vector2.Distance(
                    transform.position,
                    ShipController.Instance.transform.position
                );
                if (distToShip <= superRadius)
                    ShipController.Instance.TakeDamage(explosionDamage);

                Destroy(gameObject);
                break;
        }
    }

    // (Optional) visualize super radius in-editor
    void OnDrawGizmosSelected()
    {
        if (bombType == BombType.Super)
            Gizmos.DrawWireSphere(transform.position, superRadius);
    }
}
