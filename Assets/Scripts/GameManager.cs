using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;
using System.Threading;

public class GameManager : MonoBehaviour
{
  public BoardBuilder boardBuilder;
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

  float totalMilisecondsPassed;

  float blackMilisecondsPassed;

  float whiteMilisecondsPassed;
  float milisecondsSinceGameStarted;

  float blackTotalTime;
  float whiteTotalTime;
  public int blackLostPieces;
  public int whiteLostPieces;
  public int whiteTurnsLeft;
  public int blackTurnsLeft;

  public static bool agentTakingTurnCurrently = false;



  public static Turn currentTurn;

  public Agent agent;
  private void startTimer()
  {
    consoleManager.sendMessageToConsole("Starting!");
  }

  private void FixedUpdate()
  {



    if (gameOptions.isBlackAnAgent() && currentTurn == Turn.BLACK)
    {
      agentTurn();
    }
    if (gameOptions.isWhiteAnAgent() && currentTurn == Turn.WHITE)
    {
      agentTurn();
    }

    updateTotalTimer();

    if (currentTurn == Turn.BLACK)
    {
      updateBlackTimer();
    }
    else
    {
      updateWhiteTimer();
    }

  }

  public void agentTurn()
  {
    State currentState = boardManager.convertBoardToState();
    agent.setState(currentState);
    State newState = agent.turn(currentState);

    Node[,] newBoard = boardManager.convertStateToBoard(newState);

    Abalone.boardState = newBoard;
    abalone.boardBuilder.generateAllNeighbors(newBoard);
    abalone.updateUIBoard();
    cycleTurn();
    agentTakingTurnCurrently = false;
  }

  public void updateTotalTimer()
  {
    totalMilisecondsPassed += Time.time - milisecondsSinceGameStarted;
    totalTimer.SetText(formatMiliseconds(totalMilisecondsPassed));
  }

  public void updateBlackTimer()
  {

    blackTotalTime += Time.time - milisecondsSinceGameStarted - whiteTotalTime;
    blacktimeUI.SetText(formatMiliseconds(blackTotalTime));

  }

  public void updateWhiteTimer()
  {
    //whiteMilisecondsPassed += Time.deltaTime;
    whiteTotalTime = Time.time - milisecondsSinceGameStarted - blackMilisecondsPassed;
    whiteTimeUI.SetText(formatMiliseconds(whiteTotalTime));

  }

  public string formatMiliseconds(float miliseconds)
  {
    string min = Mathf.Floor(miliseconds / 60).ToString("00");
    float secFloat = miliseconds % 60;
    string sec = Mathf.Floor(miliseconds % 60).ToString("00");
    string mil = Mathf.Floor((miliseconds * 100) % 100).ToString("00");
    string timeString = string.Format("{0}:{1}:{2}", min, sec, mil);
    return timeString;
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
    totalMilisecondsPassed = 0;
    blackMilisecondsPassed = 0;
    whiteMilisecondsPassed = 0;
    whiteLostPiecesUI.SetText("0");
    blackLostPiecesUI.SetText("0");
    whiteLostPieces = 0;
    blackLostPieces = 0;
    whiteTotalTime = 0;
    blackTotalTime = 0;
    milisecondsSinceGameStarted = Time.time;
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
    cycleTurnsRemaining(currentTurn);
    if (currentTurn == Turn.BLACK)
    {
      currentTurn = Turn.WHITE;
    }
    else
    {
      currentTurn = Turn.BLACK;
    }
    consoleManager.sendMessageToConsole("Current turn: " + GameManager.getCurrentTurn().ToString());
    checkForWinCondition();
  }

  public void checkForWinCondition()
  {
    if (whiteLostPieces >= 6 || blackLostPieces >= 6)
    {
      showEndGame(currentTurn);
    }
  }

  public void showEndGame(Turn winningPlayer)
  {

  }

  public int getTurnAsInt()
  {
    if (currentTurn == Turn.BLACK)
    {
      return 1;
    }
    return 2;
  }

  public void updateLostPieces()
  {
    whiteLostPiecesUI.SetText(whiteLostPieces.ToString());
    blackLostPiecesUI.SetText(blackLostPieces.ToString());
  }

  public void cycleTurnsRemaining(Turn player)
  {
    if (!blackTurnsUI.text.Equals("N/A"))
    {
      switch (player)
      {
        case Turn.BLACK:
          blackTurnsLeft--;
          blackTurnsUI.SetText(blackTurnsLeft.ToString());
          break;
        case Turn.WHITE:
          whiteTurnsLeft--;
          whiteTurnsUI.SetText(whiteTurnsLeft.ToString());
          break;
      }
    }

  }
  public void getTime()
  {

  }

  public void setWhiteLostPieces(int newPieceCount)
  {
    whiteLostPieces = newPieceCount;
  }
  public void setBlackLostPieces(int newPieceCount)
  {
    blackLostPieces = newPieceCount;
  }

}


public enum Turn
{
  BLACK,
  WHITE
}
