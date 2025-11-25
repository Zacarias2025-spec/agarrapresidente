using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public Text scoreText;
    public PlayerController player;
    private bool hasShield = false;
    private float speedBoostTimer = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0) player.forwardSpeed = 6f; // volta ao normal
        }
    }

    public void AddScore(int s)
    {
        score += s;
        if (scoreText) scoreText.text = "Score: " + score;
    }

    public void ApplySpeedBoost()
    {
        player.forwardSpeed = 12f;
        speedBoostTimer = 3.5f;
    }

    public void GiveShield()
    {
        hasShield = true;
        // animação/efeito opcional
    }

    public void PlayerHitObstacle(GameObject obstacle)
    {
        if (hasShield)
        {
            hasShield = false;
            Destroy(obstacle);
            return;
        }
        // fim de jogo
        Debug.Log("Game Over");
        // aqui podes enviar para tela de Game Over e gravar score
        Time.timeScale = 0f;
    }
}
