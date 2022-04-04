using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Abalone : MonoBehaviour
{
  public BoardBuilder boardBuilder;
  public ConsoleManager consoleManager;
  public BoardManager boardManager;
  public static Node[,] boardState;

  public GameManager gameManager;

  public void generateBoard()
  {
    boardState = BoardBuilder.createBoard();
    boardBuilder.generateAllNeighbors(boardState);
    updateGameStateBoard();
  }

  //Take the gamestate from the backEnd, and push it to the front end.
  //Use for each state change.
  public void updateUIBoard()
  {
    int whitePieces = 0;
    int blackPieces = 0;
    foreach (GameObject UIBoardTile in BoardManager.BoardTiles)
    {
      string UITileName = UIBoardTile.name;
      foreach (Node backEndTile in boardState)
      {
        if (backEndTile != null)
        {
          string backEndTileName = backEndTile.getName();
          if (backEndTileName.Equals(UITileName))
          {
            boardManager.changeTileColor(UIBoardTile, backEndTile.getColor());
            if (backEndTile.getColor() == BoardColor.BLACK)
            {
              blackPieces++;
            }
            else if (backEndTile.getColor() == BoardColor.WHITE)
            {
              whitePieces++;
            }
            break;
          }
        }

      }
    }

    gameManager.setBlackLostPieces(14 - blackPieces);
    gameManager.setWhiteLostPieces(14 - whitePieces);
    gameManager.updateLostPieces();

  }

  //Take game state from the front end and push it to the back end
  //INNEFICIENT. Only use for initial Board creation.
  public void updateGameStateBoard()
  {
    foreach (Node backEndTile in boardState)
    {
      if (backEndTile != null)
      {
        string backEndTileName = backEndTile.getName();
        foreach (GameObject UIBoardTile in BoardManager.BoardTiles)
        {
          string UITileName = UIBoardTile.name;
          if (backEndTileName.Equals(UITileName))
          {
            backEndTile.setColor(UIBoardTile.GetComponent<SingleTileScript>().getTileColor());
          }
        }
      }

    }
  }

  //get node by name
  public Node getNode(string name)
  {
    Node nodeToReturn = new Node("ERROR FINDING NODE", BoardColor.EMPTY, true, 0, 0);
    string firstChar = name.Substring(0, 1);
    int secondInt = Int32.Parse(name.Substring(1, 1));
    int firstInt = 0;
    switch (firstChar)
    {
      case "A":
        firstInt = 8;
        break;
      case "B":
        firstInt = 7;
        break;
      case "C":
        firstInt = 6;
        break;
      case "D":
        firstInt = 5;
        break;
      case "E":
        firstInt = 4;
        break;
      case "F":
        firstInt = 3;
        break;
      case "G":
        firstInt = 2;
        break;
      case "H":
        firstInt = 1;
        break;
      case "I":
        firstInt = 0;
        break;

    }
    nodeToReturn = boardState[firstInt, secondInt - 1];
    return nodeToReturn;
  }

  public Node getNode(int x, int y)
  {
    // if (isNodeNull(x, y))
    // {
    //   return null;
    // }
    return boardState[y, x];
  }

  public Boolean isNodeNull(int x, int y)
  {
    return getNode(x, y) == null;
  }

  //Overloaded helper method, get node by tile
  public Node getNode(GameObject tile)
  {
    string tileName = tile.name;
    return getNode(tileName);
  }

}

//The digital representation of a single board tile
public class Node
{
  private BoardColor color; // Current state
  private string name; //ID name of the node on the board
  private bool deadZone; // Is this a tile off the board?
  private Node[] adjacentNodes; // All adjacent Nodes

  private int xcoord;
  private int ycoord;

  public Node(string name, BoardColor startingColor, bool isDead, int x, int y)
  {
    this.name = name;
    this.color = startingColor;
    this.deadZone = isDead;
    adjacentNodes = new Node[6];
    this.xcoord = x;
    this.ycoord = y;

  }

  public string getName()
  {
    return name;
  }

  public BoardColor getColor()
  {
    return color;
  }

  public void setColor(BoardColor color)
  {
    this.color = color;
  }

  public bool getDeadZone()
  {
    return deadZone;
  }

  public Node[] getNeighbors()
  {
    return adjacentNodes;
  }

  public int getX()
  {
    return xcoord;
  }

  public int getY()
  {
    return ycoord;
  }

  public Node getNWNeighbor()
  {
    return adjacentNodes[0];
  }

  public Node getNENeighbor()
  {
    return adjacentNodes[1];
  }

  public Node getENeighbor()
  {
    return adjacentNodes[2];
  }

  public Node getSENeighbor()
  {
    return adjacentNodes[3];
  }

  public Node getSWNeighbor()
  {
    return adjacentNodes[4];
  }

  public Node getWNeighbor()
  {
    return adjacentNodes[5];
  }
}


