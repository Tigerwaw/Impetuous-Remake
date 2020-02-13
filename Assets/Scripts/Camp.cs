using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
  public GameObject[] enemy_prefabs;
  [HideInInspector]
  public List<GameObject> enemies = new List<GameObject>();
  private bool campAlive = true;

  void Start()
  {
    Spawn_Mobs();
  }

  private void Spawn_Mobs()
  {
    BoxCollider col = GetComponent<BoxCollider>();
    for (int i = 0; i < enemy_prefabs.Length; i++)
    {
      Vector3 _spawnPos = new Vector3(
                          Random.Range(col.bounds.min.x + 0.5f, col.bounds.max.x - 0.5f),
                          Random.Range(col.bounds.min.y, col.bounds.max.y),
                          Random.Range(col.bounds.min.z + 0.5f, col.bounds.max.z - 0.5f));

      GameObject _enemy = Instantiate(enemy_prefabs[i], _spawnPos, transform.rotation, transform);
      _enemy.GetComponentInChildren<EnemyStats>().camp = this;
      enemies.Add(_enemy);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player") && campAlive)
    {
      GameManager.instance.combatUI.SetActive(true);
      CharacterStats playerStats = other.gameObject.GetComponent<CharacterStats>();
      BeginCombat(playerStats);
    }
  }

  void BeginCombat(CharacterStats playerStats)
  {
    GameManager.instance.campSelect.allowMovement = false;
    SwitchTarget();

    for (int i = 0; i < enemies.Count; i++)
    {
      CharacterCombat enemyCombat = enemies[i].GetComponent<CharacterCombat>();
      enemyCombat.Attack(playerStats);
    }
  }

  //Makes the player attack another target within the camp.
  public void SwitchTarget()
  {
    int _randTarget = Random.Range(0, enemies.Count);

    //Randomize players target.
    while (enemies[_randTarget].GetComponent<CharacterStats>() == GameManager.instance.playerCombat.opponentStats)
    {
      _randTarget = Random.Range(0, enemies.Count);

      if (enemies.Count < 2)
      {
        break;
      }
    }

    GameManager.instance.playerCombat.Attack(enemies[_randTarget].GetComponent<CharacterStats>());
  }

  public void CampCleared()
  {
    GameManager.instance.combatUI.SetActive(false);
    Debug.Log("Camp cleared!");
    GameManager.instance.playerCombat.InCombat = false;
    GameManager.instance.campSelect.allowMovement = true;
    campAlive = false;
  }

  void Update()
  {
        
  }
}
