using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  public GameObject mainMenuPanel;
  public GameObject optionsMenuPanel;

  // public AudioManager audioManager;

  private void Start()
  {
    // audioManager = Camera.main.GetComponentInChildren<AudioManager>();
  }

  public void PlayGame()
  {
    // audioManager.PlaySound("ButtonClick");
    SceneManager.LoadScene(2);
    SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
  }

  public void OpenSettingsMenu()
  {
    // audioManager.PlaySound("ButtonClick");
    optionsMenuPanel.SetActive(true);
    mainMenuPanel.SetActive(false);
  }

  public void GoBackToMainMenu()
  {
    // audioManager.PlaySound("ButtonClick");
    mainMenuPanel.SetActive(true);
    optionsMenuPanel.SetActive(false);
  }

  public void QuitGame()
  {
    // audioManager.PlaySound("ButtonClick");
    Debug.Log("Quit!");
    Application.Quit();
  }
}

