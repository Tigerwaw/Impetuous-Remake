using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaRegeneration : MonoBehaviour
{
  private Slider manaBar_UI;
  private CharacterStats myStats;

  private float regenTickTime;

  private CharacterRole charRole;

  private void Start()
  {
    myStats = GetComponent<CharacterStats>();
    manaBar_UI = GameManager.instance.manaBar_UI;

    charRole = myStats.role;
  }

  // How the character regains mana depends on its type (Predator, Scavenger, Forager)
  public void Regeneration()
  {
    switch (charRole)
    {
      // Mana Regeneration logic for the Predator role.
      case CharacterRole.Predator:
        break;

      // Mana Regeneration logic for the Scavenger role.
      case CharacterRole.Scavenger:
        break;

      // Mana Regeneration logic for the Forager role.
      case CharacterRole.Forager:

        if (regenTickTime < 0)
        {
          myStats.currentMana += myStats.manaRegen;
          myStats.currentMana = Mathf.Clamp(myStats.currentMana, 0, myStats.maxMana);

          // Reset the timer for the mana regeneration.
          regenTickTime = 3f;
        }
        break;
    }

    UpdateManaBar();
  }

  public void UpdateManaBar()
  {
    float manaPercent = (float)myStats.currentMana / myStats.maxMana;
    manaBar_UI.value = manaPercent;
  }

  private void Update()
  {
    if (charRole == CharacterRole.Forager)
    {
      regenTickTime -= Time.deltaTime;
      Regeneration();
    }
  }
}
