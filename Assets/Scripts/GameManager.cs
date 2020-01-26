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

  public GameObject combatUI;
  public GameObject playerHealthUI;
  public GameObject levelUpUI;
  public Slider manaBar_UI;

  public Text gold_text;
  public Text level_text;
  public Text xp_text;

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

  public void UpdateUI(int _gold, int _lvl, int _xp)
  {
    gold_text.text = _gold.ToString();
    level_text.text = _lvl.ToString();
    xp_text.text = _xp.ToString();
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
    SceneManager.LoadSceneAsync(Random.Range(2, 4), LoadSceneMode.Additive);
    SceneManager.sceneLoaded += OnSceneLoaded;
    playerStats.currentHealth = playerStats.maxHealth;
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    Transform spawnpoint = GameObject.Find("Spawnpoint").transform;
    GameManager.instance.playerStats.transform.position = spawnpoint.position;
    campSelect.agent.destination = spawnpoint.position;
    SceneManager.UnloadSceneAsync(currentSceneIndex);
    currentSceneIndex = scene.buildIndex;

    playerStats.Heal(playerStats.maxHealth - playerStats.currentHealth);

    if (playerStats.skill_points > 0)
    {
      ShowLevelUpScreen();
    }
    else
    {
      StartCoroutine(ReturnMovement(.6f));
    }
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
