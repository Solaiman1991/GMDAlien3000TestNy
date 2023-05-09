using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public int totalEnemies;
    public TextMeshProUGUI enemiesRemainingText;
    public GameObject winMessage;

    private int enemiesKilled;

    private void Start()
    {
        enemiesKilled = 0;
        winMessage.SetActive(false);
        UpdateEnemiesRemainingText();
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateEnemiesRemainingText();

        if (enemiesKilled == totalEnemies)
        {
            winMessage.SetActive(true);
        }
    }

    private void UpdateEnemiesRemainingText()
    {
        enemiesRemainingText.text = $"Enemies Remaining: {totalEnemies - enemiesKilled}";
    }
    
    public void IncrementTotalEnemies()
    {
        totalEnemies++;
        UpdateEnemiesRemainingText();
    }
}