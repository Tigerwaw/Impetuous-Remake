using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
  CharacterStats myStats;
  [HideInInspector]
  public CharacterStats opponentStats;
  NavMeshAgent agent;

  private ManaRegeneration manaRegen;

  private float defaultAttackSpeed = 200f;
  private float attackCooldown = 0f;
  const float combatCooldown = 5f;
  float lastAttackTime;
  public float attackDelay = .6f;
  public bool InCombat;

  public event System.Action OnAttack;

  public void Start()
  {
    myStats = GetComponent<CharacterStats>();
    agent = GetComponent<NavMeshAgent>();
    manaRegen = GetComponent<ManaRegeneration>();
  }

  public void Attack(CharacterStats targetStats)
  {
    if (targetStats.currentHealth <= 0)
    {
      return;
    }

    opponentStats = targetStats;

    if (attackCooldown > 0f)
    {
      return;
    }

    myStats.SetHealthUIActive();
    targetStats.SetHealthUIActive();

    StartCoroutine(DoDamage(opponentStats, .6f));

    if (OnAttack != null)
      OnAttack();

    attackCooldown = defaultAttackSpeed / (100f + myStats.attackSpeed);
    InCombat = true;
    lastAttackTime = Time.time;
  }

  IEnumerator DoDamage(CharacterStats stats, float delay)
  {
    yield return new WaitForSeconds(delay);
    opponentStats.TakeDamage(myStats.damage, myStats);

    if (myStats.role == CharacterRole.Predator)
    {
      manaRegen.Regeneration();
    }
  }

  void Update()
  {
    attackCooldown -= Time.deltaTime;

    if (InCombat)
    {
      Attack(opponentStats);
    }
  }
}
