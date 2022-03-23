using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

public class GameManager : MonoBehaviour
{
  private static Timer gameTimer;
  public ConsoleManager consoleManager;
  public BoardManager boardManager;
  public OptionsForLaunch gameOptions;
  public Abalone abalone;

  public TextMeshProUGUI whiteTurnsUI;
  public TextMeshProUGUI blackTurnsUI;
  public TextMeshProUGUI whiteLostPiecesUI;
  public TextMeshProUGUI blackLostPiecesUI;
  public TextMeshProUGUI whiteTimeUI;
  public TextMeshProUGUI blacktimeUI;
  public TextMeshProUGUI totalTimer;


  public float blackTotalTime;
  public float whiteTotalTime;
  public int blackLostPieces;
  public int whiteLostPieces;
  public int whiteTurnsLeft;
  public int blackTurnsLeft;

  public static Turn currentTurn;

  private void startTimer()
  {
    consoleManager.sendMessageToConsole("Starting!");
    gameTimer = new Timer(100);
    gameTimer.Elapsed += updateTimers;
    gameTimer.AutoReset = true;
    gameTimer.Enabled = true;
  }

  private void updateTimers(object source, ElapsedEventArgs e)
  {
    Debug.Log("In update");
    string time = e.SignalTime.ToLongTimeString();
    Debug.Log(time);
    totalTimer.SetText(time);
    consoleManager.sendMessageToConsole(time);
  }

  public void startGame()
  {

    //Set up board
    currentTurn = Turn.BLACK;
    boardManager.referenceAllBoardTiles();
    InputScript.deselectAllTiles();
    switch (gameOptions.GetLayout())
    {
      case OptionsForLaunch.Layout.Default:
        consoleManager.sendMessageToConsole("Setting layout to default");
        boardManager.setBoardToDefaultLayout();
        break;

      case OptionsForLaunch.Layout.Belgian:
        consoleManager.sendMessageToConsole("Setting layout to Belgian Daisy");
        boardManager.setBoardToBelgianLayout();
        break;

      case OptionsForLaunch.Layout.German:
        consoleManager.sendMessageToConsole("Setting layout to German Daisy");
        boardManager.setBoardToGermanLayout();
        break;
    }
    abalone.generateBoard();
    abalone.updateGameStateBoard();
    startTimer();


    if (gameOptions.getTurnLimit() == 0)
    {
      whiteTurnsUI.SetText("N/A");
      blackTurnsUI.SetText("N/A");
    }
    else
    {
      whiteTurnsUI.SetText(gameOptions.getTurnLimit().ToString());
      blackTurnsUI.SetText(gameOptions.getTurnLimit().ToString());
    }

    whiteLostPiecesUI.SetText("0");
    blackLostPiecesUI.SetText("0");
    whiteLostPieces = 0;
    blackLostPieces = 0;
    whiteTotalTime = 0;
    blackTotalTime = 0;
    whiteTimeUI.SetText("00:00:00");
    blacktimeUI.SetText("00:00:00");


  }

  public static Turn getCurrentTurn()
  {
    return currentTurn;
  }

  public static void setCurrentTurn(Turn newTurn)
  {
    currentTurn = newTurn;
  }

  public void cycleTurn()
  {
    if (currentTurn == Turn.BLACK)
    {
      currentTurn = Turn.WHITE;
    }
    else
    {
      currentTurn = Turn.BLACK;
    }
    consoleManager.sendMessageToConsole("Current turn: " + GameManager.getCurrentTurn().ToString());
  }

  public void addLostPiece(Turn player)
  {

  }

  public void cycleTimer(Turn player)
  {

  }

  public void cycleTurnsRemaining(Turn player)
  {

  }
  public void getTime()
  {

  }

}


public enum Turn
{
  BLACK,
  WHITE
}
