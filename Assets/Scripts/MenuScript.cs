using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

  GameObject menu;
  GameObject game;

  // Start is called before the first frame update
  void Start()
  {
    menu = GameObject.Find("Main Menu Canvas");
    game = GameObject.Find("Game Canvas");
    game.SetActive(false);
  }

  public void closeMenuAndShowGame()
  {
    menu.SetActive(false);
    game.SetActive(true);
  }

  public void exitGame()
  {
    Application.Quit();
  }

  // Update is called once per frame
}
