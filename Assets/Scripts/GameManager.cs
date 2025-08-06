using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int pts)
    {
        score += pts;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        // TODO: show UI
        Time.timeScale = 0f;
    }
}
