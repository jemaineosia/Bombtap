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
    public float xRange        = 2.5f;    // half-width of screen in world units
    public float baseInterval  = 1f;      // starting time between spawns
    public float minInterval   = 0.3f;    // fastest time between spawns
    public float rampDuration  = 120f;    // time to fully transition weights

    [Header("Weighted Spawn Weights")]
    [Range(0,1)] public float normalWeightStart  = 0.85f;
    [Range(0,1)] public float smokeWeightStart   = 0.10f;
    [Range(0,1)] public float scatterWeightStart = 0.04f;
    [Range(0,1)] public float superWeightStart   = 0.01f;

    [Range(0,1)] public float normalWeightEnd    = 0.60f;
    [Range(0,1)] public float smokeWeightEnd     = 0.15f;
    [Range(0,1)] public float scatterWeightEnd   = 0.15f;
    [Range(0,1)] public float superWeightEnd     = 0.10f;

    private float timer;
    private float currentInterval;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentInterval = baseInterval;
    }

    void Update()
    {
        // 1) Calculate dynamic interval ramping down over time
        float tInterval = Mathf.Clamp01(Time.timeSinceLevelLoad / rampDuration);
        currentInterval = Mathf.Lerp(baseInterval, minInterval, tInterval);

        // 2) Spawn when timer exceeds interval
        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            timer = 0f;
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        // 1) Interpolation factor for weights (0 → 1 over rampDuration)
        float t = Mathf.Clamp01(Time.timeSinceLevelLoad / rampDuration);

        // 2) Lerp each weight
        float wNormal  = Mathf.Lerp(normalWeightStart,  normalWeightEnd,  t);
        float wSmoke   = Mathf.Lerp(smokeWeightStart,   smokeWeightEnd,   t);
        float wScatter = Mathf.Lerp(scatterWeightStart, scatterWeightEnd, t);
        float wSuper   = Mathf.Lerp(superWeightStart,   superWeightEnd,   t);

        // 3) Roll a random value up to sum of weights
        float total   = wNormal + wSmoke + wScatter + wSuper;
        float roll    = Random.value * total;

        // 4) Pick prefab by where 'roll' falls
        GameObject prefab;
        if (roll < wNormal)
            prefab = normalBomb;
        else if (roll < wNormal + wSmoke)
            prefab = smokeBomb;
        else if (roll < wNormal + wSmoke + wScatter)
            prefab = scatterBomb;
        else
            prefab = superBomb;

        // 5) Instantiate at random X within range
        float x = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(x, transform.position.y, 0f);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
