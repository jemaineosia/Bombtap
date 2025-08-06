using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public static ShipController Instance;
    public float speed = 5f;
    public int HP = 3;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Add tide rise effect to ship
        if (TideController.Instance != null)
        {
            transform.Translate(Vector3.up * TideController.Instance.GetCurrentRiseSpeed() * Time.deltaTime);
        }

        float move = Input.GetAxis("Horizontal");
#if UNITY_ANDROID || UNITY_IOS
      move = Input.acceleration.x;
#endif
        Vector3 pos = transform.position;
        pos.x += move * speed * Time.deltaTime;
        transform.position = pos;
    }

    public void TakeDamage(float amount)
    {
        HP -= Mathf.CeilToInt(amount);
        StartCoroutine(FlashRoutine());
        if (HP <= 0) GameManager.Instance.GameOver();
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bomb"))
        {
            TakeDamage(1);
            Destroy(col.gameObject);
        }
    }

}
