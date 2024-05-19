using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void LevelSelect()
  {
    SceneManager.LoadScene("LoadLevel");
  }

  public void WalletSelect()
  {
    SceneManager.LoadScene("WSScene");
  }

  public void Menu()
  {
    SceneManager.LoadScene("MenuScene");
  }

}
