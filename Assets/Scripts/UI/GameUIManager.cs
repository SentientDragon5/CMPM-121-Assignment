using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public GameObject rewardUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    
    public TextMeshProUGUI rewardStatsText;
    public TextMeshProUGUI gameOverStatsText;
    public TextMeshProUGUI victoryStatsText;
    
    private EnemySpawner spawner;
    private GameManager.GameState lastState;
    
    void Start()
    {
        spawner = FindFirstObjectByType<EnemySpawner>();
        lastState = GameManager.Instance.state;
        
        rewardUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
    }
    
    void Update()
    {
        CheckDebugShortcuts();

        if (lastState != GameManager.Instance.state)
        {
            rewardUI.SetActive(false);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(false);
            
            lastState = GameManager.Instance.state;
        }
        
        switch (GameManager.Instance.state)
        {
            case GameManager.GameState.WAVEEND:
                HandleWaveEnd();
                break;
            case GameManager.GameState.GAMEOVER:
                HandleGameOver();
                break;
            case GameManager.GameState.VICTORY:
                HandleVictory();
                break;
        }
    }
    
    void HandleWaveEnd()
    {
        if (!rewardUI.activeSelf)
        {
            rewardUI.SetActive(true);
            float waveDuration = Time.time - GameManager.Instance.waveStartTime;
            
            if (rewardStatsText != null)
            {
                rewardStatsText.text = $"Wave Complete!\n" +
                               $"Time: {waveDuration:F1} seconds\n" +
                               $"Damage Dealt: {GameManager.Instance.totalDamageDealt}\n" +
                               $"Damage Taken: {GameManager.Instance.totalDamageTaken}\n";
            }
        }
    }
    
    void HandleGameOver()
    {
        if (!gameOverUI.activeSelf)
        {
            gameOverUI.SetActive(true);
            
            if (gameOverStatsText != null)
            {
                gameOverStatsText.text = "Game Over!\nYou were defeated!";
                Debug.Log("Set game over text to: " + gameOverStatsText.text);
            }
            else
            {
                Debug.LogError("gameOverStatsText is null!");
            }
        }
    }
    
    void HandleVictory()
    {
        if (!victoryUI.activeSelf)
        {
            victoryUI.SetActive(true);
            
            if (victoryStatsText != null)
            {
                victoryStatsText.text = "Victory!\nYou completed all waves!";
                Debug.Log("Set victory text to: " + victoryStatsText.text);
            }
            else
            {
                Debug.LogError("victoryStatsText is null!");
            }
        }
    }
    
    public void NextWave()
    {
        spawner.NextWave();
    }
    
    public void ReturnToMenu()
    {

        GameManager.Instance.state = GameManager.GameState.PREGAME;
        
        lastState = GameManager.GameState.PREGAME;

        rewardUI.SetActive(false);
        victoryUI.SetActive(false);
        gameOverUI.SetActive(false);

        spawner.StopAllCoroutines();
        spawner.wave = 0;
        spawner.level_selector.gameObject.SetActive(true);

        Unit[] allUnits = FindObjectsOfType<Unit>();
        foreach (Unit unit in allUnits)
        {
            if (unit.gameObject == GameManager.Instance.player)
                continue;
                
            Destroy(unit.gameObject);
        }
        GameManager.Instance.Reset();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void CheckDebugShortcuts()
    {
        // Press O key to lose the game
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Debug: Force Game Over");
            GameManager.Instance.GameOver();
        }
        
        // Press P key to win the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Debug: Force Victory");
            GameManager.Instance.Victory();
        }
    }
}