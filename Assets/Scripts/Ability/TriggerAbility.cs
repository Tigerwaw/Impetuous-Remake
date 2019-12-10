using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAbility : MonoBehaviour
{
  private CharacterStats myStats;

  private void Start()
  {
    myStats = GetComponent<CharacterStats>();
  }

  public void UseAbility(Ability _ability, CharacterStats _myStats, CharacterStats _targetStats)
  {
    switch (_ability.abilityType)
    {
      case AbilityType.Targeted:
        Targeted(_ability, _targetStats);
        break;
      case AbilityType.AOE:
        break;
    }
  }

  public void Targeted(Ability _ability, CharacterStats _targetStats)
  {
    _targetStats.TakeDamage(_ability.damage, myStats);

    if (_ability.appliesDebuff)
    {

      switch (_ability.debuffType)
      {
        case DebuffType.Armor:
          _targetStats.armor -= _ability.debuffAmount;
          Debug.Log(_targetStats.gameObject.name + " just lost " + _ability.debuffAmount + " armor!");
          break;

        case DebuffType.AttackSpeed:
          _targetStats.attackSpeed -= _ability.debuffAmount;
          Debug.Log(_targetStats.gameObject.name + " just lost " + _ability.debuffAmount + " attack speed!");
          break;

        case DebuffType.Damage:
          _targetStats.damage -= _ability.debuffAmount;
          Debug.Log(_targetStats.gameObject.name + " just lost " + _ability.debuffAmount + " damage!");
          break;

        case DebuffType.ManaRegen:
          _targetStats.manaRegen -= _ability.debuffAmount;
          Debug.Log(_targetStats.gameObject.name + " just lost " + _ability.debuffAmount + " mana regen!");
          break;

        case DebuffType.MaxHealth:
          _targetStats.maxHealth -= _ability.debuffAmount;
          Debug.Log(_targetStats.gameObject.name + " just lost " + _ability.debuffAmount + " max health!");
          break;
      }

      StartCoroutine(ReturnStats(_targetStats, _ability.debuffType, _ability.debuffAmount, _ability.debuffDuration));
    }
  }

  IEnumerator ReturnStats(CharacterStats _targetStats, DebuffType _debuffType, int _debuffAmount, float _duration)
  {
    yield return new WaitForSeconds(_duration);

    // Checks if enemy is dead before returning the stats to it.
    if (_targetStats == null)
      yield break;

    switch (_debuffType)
    {
      case DebuffType.Armor:
        _targetStats.armor += _debuffAmount;
        Debug.Log(_targetStats.gameObject.name + " regained " + _debuffAmount + " armor!");
        break;
      case DebuffType.AttackSpeed:
        _targetStats.attackSpeed += _debuffAmount;
        Debug.Log(_targetStats.gameObject.name + " regained " + _debuffAmount + " attack speed!");
        break;
      case DebuffType.Damage:
        _targetStats.damage += _debuffAmount;
        Debug.Log(_targetStats.gameObject.name + " regained " + _debuffAmount + " damage!");
        break;
      case DebuffType.ManaRegen:
        _targetStats.manaRegen += _debuffAmount;
        Debug.Log(_targetStats.gameObject.name + " regained " + _debuffAmount + " mana regen!");
        break;
      case DebuffType.MaxHealth:
        _targetStats.maxHealth += _debuffAmount;
        Debug.Log(_targetStats.gameObject.name + " regained " + _debuffAmount + " max health!");
        break;
    }
  }
}
