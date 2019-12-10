using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterRole
{
  Predator,
  Scavenger,
  Forager
};

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
  public CharacterRole role;
  private HealthUI healthUI;
  private ManaRegeneration manaRegenFunction;

  [Header("Attributes")]
  // Attributes
  public int currentHealth;
  public int maxHealth;
  public int currentMana;
  public int maxMana;
  public int manaRegen;
  [Space]
  public int damage;
  public int attackSpeed;
  public int armor;
  // public int spirit;


  public virtual void Start()
  {
    currentHealth = maxHealth;

    healthUI = GetComponent<HealthUI>();

    manaRegenFunction = GetComponent<ManaRegeneration>();
  }

  public void TakeDamage(int _damage, CharacterStats enemyStats)
  {
    // Damage multiplier based on armor value.
    float _multiplier = 100f / (100f + armor);
    float _dmgFloat = _damage;

    // Multiply the damage with the multiplier and round to an even integer.
    _damage = Mathf.RoundToInt(_dmgFloat * _multiplier);

    //Make sure damage doesn't go below 1.
    _damage = Mathf.Clamp(_damage, 1, int.MaxValue);

    // Subtract damage from health
    currentHealth -= _damage;

    // Visual stuff.
    Debug.Log(transform.name + " takes " + _damage + " damage.");
    healthUI.OnHealthChanged(maxHealth, currentHealth, _damage);
    healthUI.SpawnDamageNumber(_damage);


    // Mana regeneration for being attacked.
    if (role == CharacterRole.Predator)
    {
      manaRegenFunction.Regeneration();
    }

    // If the unit hits 0hp, it dies.
    if (currentHealth <= 0)
    {
      // Give Mana regen to the unit that killed you if they are of the Scavenger character role.
      if (enemyStats.role == CharacterRole.Scavenger)
      {
        enemyStats.manaRegenFunction.Regeneration();
      }

      Die();
    }
  }

  public void SetHealthUIActive()
  {
    healthUI.gameObject.SetActive(true);
  }

  // Heal the character.
  public void Heal(int amount)
  {
    currentHealth += amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    healthUI.UpdateHealthBar();
  }

  public virtual void Die()
  {
    // Code for when the unit dies.
  }
}
