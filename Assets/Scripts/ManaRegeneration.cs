using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaRegeneration : MonoBehaviour
{
  private Slider manaBar_UI;
  private Text manaBar_Text;
  private CharacterStats myStats;

  private CharacterRole charRole;

  private void Start()
  {
    myStats = GetComponent<CharacterStats>();
    manaBar_UI = GameManager.instance.manaBar_UI;
    manaBar_Text = manaBar_UI.GetComponentInChildren<Text>();

    charRole = myStats.role;
  }

  // How the character regains mana depends on its type (Predator, Scavenger, Forager)
  public void Regeneration()
  {
    switch (charRole)
    {
      // Predators regenerate mana when dealing damage.
      case CharacterRole.Predator:
        break;

      // Scavengers regenerate mana on a successful kill.
      case CharacterRole.Scavenger:
        break;

      // Foragers regenerate mana when taking damage. (Placeholder?)
      case CharacterRole.Forager:
        break;
    }

    UpdateManaBar();
  }

  public void UpdateManaBar()
  {
    float manaPercent = (float)myStats.currentMana / myStats.maxMana;
    manaBar_UI.value = manaPercent;
    manaBar_Text.text = myStats.currentMana.ToString();
  }

  private void Update()
  {
  }
}
