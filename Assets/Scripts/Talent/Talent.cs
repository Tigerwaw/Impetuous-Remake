using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Talent")]
public class Talent : ScriptableObject
{
  [Header("Talent Information")]
  [Space]
  public Texture icon;
  new public string name;
  [TextArea]
  public string description;
  [Space]
  [Space]
  [Space]

  [Header("Ability Information")]
  [Space]

  // Percentual gold increase
  public int goldMod;
}
