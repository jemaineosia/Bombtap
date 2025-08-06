using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public static BombSpawner Instance;

    [Header("Prefabs")]
    public GameObject normalBomb;
    public GameObject smokeBomb;
    public GameObject scatterBomb;
    public GameObject superBomb;

    [Header("Spawn Settings")]
    public float xRange         = 2.5f;
    public float baseInterval   = 1f;
    public float minInterval    = 0.3f;
    public int   cycleCount     = 0;

    [Header("Storm Settings")]
    public float stormDuration  = 5f;
    public float stormRate      = 0.15f;

    private float timer;
    private bool  inStorm;
    private float stormTimer;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (inStorm)
        {
            stormTimer += Time.deltaTime;
            if (timer >= stormRate)
            {
                timer = 0f;
                SpawnBomb();
            }
            if (stormTimer >= stormDuration)
                inStorm = false;
        }
        else
        {
            // decrease interval each cycle
            float interval = Mathf.Max(minInterval, baseInterval - cycleCount * 0.1f);
            if (timer >= interval)
            {
                timer = 0f;
                SpawnBomb();
            }
        }
    }

    void SpawnBomb()
    {
        float x = Random.Range(-xRange, xRange);
        GameObject prefab;

        // unlock by cycleCount
        if (cycleCount >= 3)      prefab = superBomb;
        else if (cycleCount == 2) prefab = scatterBomb;
        else if (cycleCount == 1) prefab = smokeBomb;
        else                      prefab = normalBomb;

        Instantiate(prefab, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    // Called by TideController at each surge peak
    public void TriggerStorm()
    {
        inStorm    = true;
        stormTimer = 0f;
        cycleCount = Mathf.Min(3, cycleCount + 1);  // advance bomb unlocks once per surge
    }
}
