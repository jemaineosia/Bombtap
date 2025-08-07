using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public static BombSpawner Instance;

    [Header("Lane Settings")]
    public int laneCount = 5;
    private float[] laneX;

    [Header("Prefabs")]
    public GameObject normalBomb;
    public GameObject smokeBomb;
    public GameObject scatterBomb;
    public GameObject superBomb;

    [Header("Spawn Settings")]
    public float baseInterval = 1f;    // starting time between spawns
    public float minInterval = 0.3f;  // fastest time between spawns
    public float rampDuration = 120f;  // how long to ease into max difficulty
    private float timer;
    private float currentInterval;

    [Header("Weighted Spawn Weights")]
    [Range(0, 1)] public float normalWeightStart = 0.85f;
    [Range(0, 1)] public float smokeWeightStart = 0.10f;
    [Range(0, 1)] public float scatterWeightStart = 0.04f;
    [Range(0, 1)] public float superWeightStart = 0.01f;

    [Range(0, 1)] public float normalWeightEnd = 0.60f;
    [Range(0, 1)] public float smokeWeightEnd = 0.15f;
    [Range(0, 1)] public float scatterWeightEnd = 0.15f;
    [Range(0, 1)] public float superWeightEnd = 0.10f;

    [ContextMenu("Reset Weights Values")]
    private void ResetWeights()
    {
        normalWeightStart = 0.85f;
        smokeWeightStart = 0.10f;
        scatterWeightStart = 0.04f;
        superWeightStart = 0.01f;

        normalWeightEnd = 0.60f;
        smokeWeightEnd = 0.15f;
        scatterWeightEnd = 0.15f;
        superWeightEnd = 0.10f;
    }

    void Awake()
    {
        Instance = this;

        // Precompute lane X positions based on camera width
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Screen.width / Screen.height;

        laneX = new float[laneCount];
        for (int i = 0; i < laneCount; i++)
        {
            // evenly space from -halfWidth to +halfWidth
            float t = i / (float)(laneCount - 1);
            laneX[i] = Mathf.Lerp(-halfWidth, halfWidth, t);
        }
    }

    void Start()
    {
        currentInterval = baseInterval;
    }

    void Update()
    {
        // 1) Ramp down spawn interval over time
        float ti = Mathf.Clamp01(Time.timeSinceLevelLoad / rampDuration);
        currentInterval = Mathf.Lerp(baseInterval, minInterval, ti);

        // 2) Spawn when timer passes interval
        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            timer = 0f;
            SpawnBombInRandomLane();
        }
    }

    void SpawnBombInRandomLane()
    {
        float t = Mathf.Clamp01(Time.timeSinceLevelLoad / rampDuration);

        float wN = Mathf.Lerp(normalWeightStart, normalWeightEnd, t);
        float wS = Mathf.Lerp(smokeWeightStart, smokeWeightEnd, t);
        float wC = Mathf.Lerp(scatterWeightStart, scatterWeightEnd, t);
        float wX = Mathf.Lerp(superWeightStart, superWeightEnd, t);

        float total = wN + wS + wC + wX;
        float roll = Random.value * total;

        GameObject prefab;
        if (roll < wN) prefab = normalBomb;
        else if (roll < wN + wS) prefab = smokeBomb;
        else if (roll < wN + wS + wC) prefab = scatterBomb;
        else prefab = superBomb;

        int laneIndex = Random.Range(0, laneX.Length);
        Vector3 spawnPos = new Vector3(laneX[laneIndex], transform.position.y, 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
