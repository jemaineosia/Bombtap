using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI timerText, hpText, scoreText;
    public ShipController ship;
    public GameManager gameManager;
    float elapsed;

    void Update() {
        elapsed += Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.FloorToInt(elapsed / 60).ToString("00") + ":" + Mathf.FloorToInt(elapsed % 60).ToString("00");

        if (hpText != null && ship != null)
            hpText.text = "HP: " + ship.HP;

        if (scoreText != null && gameManager != null)
            scoreText.text = "Score: " + gameManager.score;
    }
}
