using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
  public GameManager gameManager;
  public ConsoleManager consoleManager;
  public BoardManager boardManager;

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
    consoleManager.sendMessageToConsole("White is an agent? : " + options.isWhiteAnAgent());
    consoleManager.sendMessageToConsole("Black is an agent? : " + options.isBlackAnAgent());
    consoleManager.sendMessageToConsole("Color: " + options.GetColor());
    consoleManager.sendMessageToConsole("Layout: " + options.GetLayout());
    consoleManager.sendMessageToConsole("Time Limit: " + options.getTimeLimit());
    consoleManager.sendMessageToConsole("Turn Limit: " + options.getTurnLimit());
    gameEmpty.SetActive(true);
    gameManager.startGame();
  }

  public void exitGame()
  {
    Application.Quit();
  }

  // Update is called once per frame
}
