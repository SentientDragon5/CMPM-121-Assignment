using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

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
    
    public SpellUI reward;
    public Button acceptReward;
    public TextMeshProUGUI rewardText;
    public Button[] acceptRelics = new Button[3];
    public TextMeshProUGUI[] relicTexts = new TextMeshProUGUI[3];
    private List<Relic> relicReward;

    void HandleWaveEnd()
    {
        if (!rewardUI.activeSelf)
        {
            rewardUI.SetActive(true);
            float waveDuration = Time.time - GameManager.Instance.waveStartTime;
            bool thirdWave = ((spawner.wave + 1) % 3 == 0 && spawner.wave > 0);
            
            var pc = GameManager.Instance.player.GetComponent<PlayerController>();
            pc.RollReward();
            reward.SetSpell(pc.Reward);
            acceptReward.interactable = pc.CanCarryMoreSpells;
            acceptReward.gameObject.SetActive(true);
            rewardText.text = pc.Reward.GetName() + "\n" + pc.Reward.GetDescription();
            pc.onDropSpell.AddListener(() => acceptReward.interactable = true);

            if (rewardStatsText != null)
            {
                rewardStatsText.text = $"Wave Complete!\n" +
                               $"Time: {waveDuration:F1} seconds\n" +
                               $"Damage Dealt: {GameManager.Instance.totalDamageDealt}\n" +
                               $"Damage Taken: {GameManager.Instance.totalDamageTaken}\n" +
                               "Claim your Reward:";
            }

            //needs to be last in this method (cuz guard if statement ykwm)
            for (int i = 0; i < 3; i++)
                if (acceptRelics[i] == null || relicTexts[i] == null)
                    return;
            pc.RollRelic();
            for (int i = 0; i < 3; i++){
                acceptRelics[i].gameObject.SetActive(thirdWave);
                relicTexts[i].gameObject.SetActive(thirdWave);
                acceptRelics[i].interactable = pc.Relic.Count > i;
                var specificButton = acceptRelics[i]; //i miss pointers
                pc.onTakeRelic.AddListener(() => specificButton.interactable = false);
                if (pc.Relic.Count > i)
                    relicTexts[i].text = pc.Relic[i].name + "\n" + pc.Relic[i].trigger.description + " " + pc.Relic[i].effect.description;
                else
                    relicTexts[i].text = "Nothing?\nWhen you're out (or almost out) of possible relic rewards this appears...";
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

        Unit[] allUnits = FindObjectsByType<Unit>(FindObjectsSortMode.None);
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