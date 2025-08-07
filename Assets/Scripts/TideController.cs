using UnityEngine;

public class TideController : MonoBehaviour
{
    public static TideController Instance;
    [Header("Surge Settings")]
    public float surgeInterval    = 20f;    // seconds between surges
    public float surgeDuration    = 4f;     // total time: rise+recede
    public float initialSurgePeak = 2f;     // how high above startY
    public float surgePeakIncr    = 0.5f;   // increase peak each cycle

    private float startY;
    private float timer;
    private bool  inSurge;
    private float surgeTimer;
    private float currentPeak;
    private bool  stormTriggered;

    void Start()
    {
        startY      = transform.position.y;
        currentPeak = initialSurgePeak;
    }

    public float GetCurrentRiseSpeed()
    {
        if (!inSurge) return 0f;

        // Calculate rise speed based on surge progress
        float t = surgeTimer / surgeDuration;
        return (t <= 0.5f) ? (currentPeak / (surgeDuration * 0.5f)) : (-currentPeak / (surgeDuration * 0.5f));
    }

    void Update()
    {
        timer += Time.deltaTime;

        // begin surge
        if (!inSurge && timer >= surgeInterval)
        {
            inSurge          = true;
            surgeTimer       = 0f;
            timer            = 0f;
            stormTriggered   = false;
        }

        if (inSurge)
        {
            surgeTimer += Time.deltaTime;
            float t = surgeTimer / surgeDuration;

            // linear up for first half, down for second half
            float targetY = (t <= 0.5f)
                ? Mathf.Lerp(startY, startY + currentPeak, t * 2f)
                : Mathf.Lerp(startY + currentPeak, startY, (t - 0.5f) * 2f);

            transform.position = new Vector3(
                transform.position.x,
                targetY,
                transform.position.z
            );

            // end surge
            if (surgeTimer >= surgeDuration)
            {
                inSurge      = false;
                currentPeak += surgePeakIncr;
            }
        }
    }
}
