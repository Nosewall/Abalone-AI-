using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
  public OptionsForLaunch options;
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

    Debug.Log("Game started with these options:");
    Debug.Log("Players: " + options.GetPlayers());
    Debug.Log("Color: " + options.GetColor());
    Debug.Log("Layout: " + options.GetLayout());
    Debug.Log("Time Limit: " + options.getTimeLimit());
    Debug.Log("Turn Limit: " + options.getTurnLimit());
  }

  public void exitGame()
  {
    Application.Quit();
  }

  // Update is called once per frame
}
