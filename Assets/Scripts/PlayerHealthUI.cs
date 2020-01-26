using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : HealthUI
{
  [HideInInspector] public GameObject playerHealth_ui;

  public override void Start()
  {
    base.Start();

    playerHealth_ui = GameManager.instance.playerHealthUI;
    ui = playerHealth_ui.transform;
    healthLostSlider = ui.GetChild(1).GetComponent<Slider>();
    healthSlider = ui.GetChild(2).GetComponent<Slider>();
  }
}
