using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
  public ConsoleManager consoleManager;

  public OptionsForLaunch options;
  GameObject menu;


  GameObject gameEmpty;

  void Start()
  {
    menu = GameObject.Find("Main Menu Canvas");
    gameEmpty = GameObject.Find("GameEmpty");
    Debug.Log(gameEmpty);
    gameEmpty.SetActive(false);


  }

  public void closeMenuAndShowGame()
  {
    menu.SetActive(false);
    consoleManager.sendMessageToConsole("Game started with these options:");
    consoleManager.sendMessageToConsole("Players: " + options.GetPlayers());
    consoleManager.sendMessageToConsole("Color: " + options.GetColor());
    consoleManager.sendMessageToConsole("Layout: " + options.GetLayout());
    consoleManager.sendMessageToConsole("Time Limit: " + options.getTimeLimit());
    consoleManager.sendMessageToConsole("Turn Limit: " + options.getTurnLimit());
    gameEmpty.SetActive(true);
  }

  public void exitGame()
  {
    Application.Quit();
  }

  // Update is called once per frame
}
