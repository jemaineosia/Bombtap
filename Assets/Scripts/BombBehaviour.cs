using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
