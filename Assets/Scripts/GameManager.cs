using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

  private int currentSceneIndex = 2;

  public PlayerStats playerStats;
  public CharacterCombat playerCombat;
  public CampSelection campSelect;

  public GameObject HP_Prefab;
  public GameObject dmgNr_Prefab;
  public GameObject healNr_Prefab;

  public GameObject combatUI;
  public GameObject playerHealthUI;
  public GameObject levelUpUI;
  public Slider manaBar_UI;

  public Text gold_text;
  public Text level_text;

  void Awake()
  {
    //Check if instance already exists
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    ShowLevelUpScreen();
  }

  public void UpdateUI(int _gold, int _lvl)
  {
    gold_text.text = _gold.ToString();
    level_text.text = _lvl.ToString();
  }

  public void ShowLevelUpScreen()
  {
    levelUpUI.SetActive(true);
  }

  public void RestartGame()
  {
    SceneManager.UnloadSceneAsync(currentSceneIndex);
    SceneManager.LoadScene(0);
  }

  public void LoadNewRoom()
  {
    campSelect.allowMovement = false;
    SceneManager.LoadSceneAsync(Random.Range(2, 3), LoadSceneMode.Additive);
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    SceneManager.sceneLoaded -= OnSceneLoaded;
    Debug.Log("Loaded new Room!");
    Transform spawnpoint = GameObject.Find("Spawnpoint").transform;
    GameManager.instance.playerStats.transform.position = spawnpoint.position;
    campSelect.agent.destination = spawnpoint.position;
    SceneManager.UnloadSceneAsync(currentSceneIndex);
    currentSceneIndex = scene.buildIndex;

    playerStats.Heal(playerStats.maxHealth - playerStats.currentHealth);
    playerStats.LevelUp();

    ShowLevelUpScreen();
  }

  IEnumerator ReturnMovement(float delay)
  {
    yield return new WaitForSeconds(delay);

    campSelect.allowMovement = true;
  }

  private void Update()
  {
  }
}
