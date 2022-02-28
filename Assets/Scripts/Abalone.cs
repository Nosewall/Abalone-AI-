using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Abalone : MonoBehaviour
{
  public BoardBuilder builder;
  public ConsoleManager consoleManager;

  public BoardManager boardManager;
  public static List<Node> boardState;

  public void generateBoard()
  {
    boardState = BoardBuilder.createBoard();
    generateAllNeighbors(boardState);
  }

  public void generateAllNeighbors(List<Node> board)
  {
    builder.generateAllNeighbors(board);
  }

  //Take the gamestate from the backEnd, and push it to the front end.
  //Use for each state change.
  public void updateUIBoard()
  {
    foreach (GameObject UIBoardTile in BoardManager.BoardTiles)
    {
      string UITileName = UIBoardTile.name;
      foreach (Node backEndTile in boardState)
      {
        string backEndTileName = backEndTile.getName();
        if (backEndTileName.Equals(UITileName))
        {
          boardManager.changeTileColor(UIBoardTile, backEndTile.getColor());
          break;
        }
      }
    }
  }

  //Take game state from the front end and push it to the back end
  //INNEFICIENT. Only use for initial Board creation.
  public void updateGameStateBoard()
  {
    foreach (Node backEndTile in boardState)
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

  //get node by name
  public Node getNode(string name)
  {
    Node nodeToReturn = new Node("ERROR FINDING NODE", BoardColor.EMPTY, true, 0, 0);
    foreach (Node newNode in boardState)
    {
      if (newNode.getName().Equals(name))
      {
        nodeToReturn = newNode;
        break;
      }
    }
    return nodeToReturn;
  }

  //Overloaded helper method, get node by tile
  public Node getNode(GameObject tile)
  {
    string tileName = tile.name;
    return getNode(tileName);
  }

  //Overloaded helper method, get node by coordinates
  public Node getNode(int x, int y)
  {
    Node nodeToReturn = new Node("ERROR FINDING NODE BY COORDINATES", BoardColor.EMPTY, true, 0, 0);
    foreach (Node node in boardState)
    {
      if (node.getX() == x)
      {
        if (node.getY() == y)
        {
          nodeToReturn = node;
          break;
        }
      }
    }
    return nodeToReturn;
  }

  public bool checkIfTilesAreInLine(List<Node> nodeList)
  {
    //If there is only 1 piece, then the pieces are in a line.
    if (nodeList.Count <= 1)
    {
      return true;
    }
    bool tilesAreInLine = false;

    //Make a list of all coordinates
    int[] xList = new int[nodeList.Count];
    int[] yList = new int[nodeList.Count];
    for (int i = 0; i < nodeList.Count; i++)
    {
      xList[i] = nodeList[i].getX();
      yList[i] = nodeList[i].getY();
    }

    //sort Arrays for Validation
    Array.Sort(xList);
    Array.Sort(yList);



    //Strait West -> East or East -> West
    bool eastWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      eastWestPillar = xList[i + 1] - xList[i] == 2;
      if (!eastWestPillar)
      {
        break;
      }
    }

    //SW -> NE pillar
    bool southWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      southWestPillar = xList[i + 1] - xList[i] == 1 && yList[i + 1] - yList[i] == 1;
      if (!southWestPillar)
      {
        break;
      }
    }

    //NW -> SE pillar !!!!Actually this one might be redundant with a sorted array. Hmmmm.
    bool northWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      northWestPillar = xList[i + 1] - xList[i] == 1 && yList[i + 1] - yList[i] == 1;
      if (!northWestPillar)
      {
        break;
      }
    }

    tilesAreInLine = eastWestPillar || northWestPillar || southWestPillar;
    return tilesAreInLine;
  }

  //Checks if nodes are in a horizontal pillar. Because we only call this after we know that the Nodes are already in a valid pillar, this will return false only if the nodes are in a diagonal Pillar
  public bool areNodesInHorizontalPillar(List<Node> nodeList)
  {
    if (nodeList.Count <= 1)
    {
      return true;
    }

    int[] xList = new int[nodeList.Count];
    for (int i = 0; i < nodeList.Count; i++)
    {
      xList[i] = nodeList[i].getX();
    }

    Array.Sort(xList);

    return xList[1] - xList[0] == 2;
  }

  //Check if the tile to the east of a proposed pillar move is empty
  public bool checkIfEastIsEmptyPillar(List<Node> nodeList)
  {
    int[] xList = getXCoordsFromNodes(nodeList);
    int xCoordOfNeighbor = xList[xList.Length - 1] + 2;
    int yCoordOfNeighbor = nodeList[0].getY();
    Node neighborNode = getNode(xCoordOfNeighbor, yCoordOfNeighbor);
    return neighborNode.getColor() == BoardColor.EMPTY;
  }

  public bool checkIfEastIsEmptySideStep(List<Node> nodeList)
  {
    int[] xList = getXCoordsFromNodes(nodeList);
    int[] yList = getYCoordsFromNodes(nodeList);

    List<Node> neighborList;
    return true; //TEMP
  }

  public int[] getXCoordsFromNodes(List<Node> nodeList)
  {
    int[] xList = new int[nodeList.Count];
    for (int i = 0; i < nodeList.Count; i++)
    {
      xList[i] = nodeList[i].getX();
    }

    Array.Sort(xList);
    return xList;
  }

  public int[] getYCoordsFromNodes(List<Node> nodeList)
  {
    int[] yList = new int[nodeList.Count];
    for (int i = 0; i < nodeList.Count; i++)
    {
      yList[i] = nodeList[i].getY();
    }

    Array.Sort(yList);
    return yList;
  }


}

//The digital representation of a single board tile
public class Node
{
  private int xPosition; // X position 1 - 21
  private int yPosition; // Y position 1 - 11
  private BoardColor color; // Current state
  private string name; //ID name of the node on the board
  private bool deadZone; // Is this a tile off the board?
  private List<Node> adjacentNodes; // All adjacent Nodes

  public Node(string name, BoardColor startingColor, bool isDead, int x, int y)
  {
    this.name = name;
    this.color = startingColor;
    this.deadZone = isDead;
    this.xPosition = x;
    this.yPosition = y;
    adjacentNodes = new List<Node>();
  }

  public void addNodeToAdjacent(Node newNode)
  {
    adjacentNodes.Add(newNode);
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

  public int getX()
  {
    return xPosition;
  }
  public int getY()
  {
    return yPosition;
  }

  public bool getDeadZone()
  {
    return deadZone;
  }

  public List<Node> getNeighbors()
  {
    return adjacentNodes;
  }
}


