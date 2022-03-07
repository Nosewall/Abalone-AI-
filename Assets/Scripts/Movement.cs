using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Movement : MonoBehaviour
{
  public ConsoleManager consoleManager;
  public Abalone abalone;

  //Checks if the tiles are in a valid line for movement
  public bool checkIfTilesAreInLine()
  {
    List<Node> nodeList = new List<Node>();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    nodeList = nodeList.OrderBy(x => x.getX()).ToList();

    //If there is only 1 piece, then the pieces are in a line.
    if (nodeList.Count <= 1)
    {
      return true;
    }
    bool tilesAreInLine = false;

    //Strait West -> East or East -> West
    bool eastWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      eastWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 0
                     && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!eastWestPillar)
      {
        break;
      }
    }

    //SW -> NE pillar
    bool southWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      southWestPillar = nodeList[i].getY() - nodeList[i + 1].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!southWestPillar)
      {
        break;
      }
    }

    nodeList = nodeList.OrderBy(x => x.getY()).ToList();
    //NW -> SE pillar
    bool northWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      northWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 0;
      if (!northWestPillar)
      {
        break;
      }
    }

    tilesAreInLine = eastWestPillar || northWestPillar || southWestPillar;
    return tilesAreInLine;
  }


  //Checks if move is a side step. Returns false if pillar
  public Boolean checkIfSideStep(Direction direction)
  {
    List<Node> nodeList = new List<Node>();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    nodeList = nodeList.OrderBy(x => x.getX()).ToList();

    //If there is only 1 piece, then the pieces are in a line.
    if (nodeList.Count <= 1)
    {
      return true;
    }

    //Strait West -> East or East -> West
    bool eastWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      eastWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 0
                     && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!eastWestPillar)
      {
        break;
      }
    }

    //SW -> NE pillar
    bool southWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      southWestPillar = nodeList[i].getY() - nodeList[i + 1].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 1;
      if (!southWestPillar)
      {
        break;
      }
    }

    nodeList = nodeList.OrderBy(x => x.getY()).ToList();
    //NW -> SE pillar
    bool northWestPillar = false;
    for (int i = 0; i < nodeList.Count - 1; i++)
    {
      northWestPillar = nodeList[i + 1].getY() - nodeList[i].getY() == 1
                   && nodeList[i + 1].getX() - nodeList[i].getX() == 0;
      if (!northWestPillar)
      {
        break;
      }
    }

    if (direction == Direction.E || direction == Direction.W)
    {
      return !eastWestPillar;
    }
    else if (direction == Direction.NE || direction == Direction.SW)
    {
      return !southWestPillar;
    }
    else
    {
      return !northWestPillar;
    }

  }

  //Checks if all spaces are empty for a Side Step.
  public Boolean checkIsSpaceEmptySideStep(Direction d)
  {
    Boolean spaceIsEmpty = false;
    List<Node> nodeList = new List<Node>();

    nodeList = nodeList.OrderBy(x => x.getX()).ToList();
    nodeList = nodeList.OrderBy(x => x.getY()).ToList();

    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    //NW
    if (d == Direction.NW)
    {
      foreach (Node node in nodeList)
      {
        spaceIsEmpty = node.getNWNeighbor() != null && node.getNWNeighbor().getColor() == BoardColor.EMPTY;
        if (!spaceIsEmpty)
        {
          break;
        }
      }
    }
    //NE
    if (d == Direction.NE)
    {
      foreach (Node node in nodeList)
      {
        spaceIsEmpty = node.getNENeighbor() != null && node.getNENeighbor().getColor() == BoardColor.EMPTY;
        if (!spaceIsEmpty)
        {
          break;
        }
      }
    }
    //E
    if (d == Direction.E)
    {
      foreach (Node node in nodeList)
      {
        spaceIsEmpty = node.getENeighbor() != null && node.getENeighbor().getColor() == BoardColor.EMPTY;
        if (!spaceIsEmpty)
        {
          break;
        }
      }
    }
    //SE
    if (d == Direction.SE)
    {
      foreach (Node node in nodeList)
      {
        spaceIsEmpty = node.getSENeighbor() != null && node.getSENeighbor().getColor() == BoardColor.EMPTY;
        if (!spaceIsEmpty)
        {
          break;
        }
      }
    }
    //SW
    if (d == Direction.SW)
    {
      foreach (Node node in nodeList)
      {
        spaceIsEmpty = node.getSWNeighbor() != null && node.getSWNeighbor().getColor() == BoardColor.EMPTY;
        if (!spaceIsEmpty)
        {
          break;
        }
      }
    }
    //W
    if (d == Direction.W)
    {
      foreach (Node node in nodeList)
      {
        spaceIsEmpty = node.getWNeighbor() != null && node.getWNeighbor().getColor() == BoardColor.EMPTY;
        if (!spaceIsEmpty)
        {
          break;
        }
      }
    }
    return spaceIsEmpty;
  }

  //Checks if all spaces are empty for a column move.
  public Boolean checkIsSpaceEmptyColumn(Direction d)
  {
    Boolean spaceIsEmpty = false;
    List<Node> nodeList = new List<Node>();



    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }
    List<Node> nodeListByX = nodeList.OrderBy(x => x.getX()).ToList();
    List<Node> nodeListByY = nodeList.OrderBy(x => x.getY()).ToList();
    //NW
    if (d == Direction.NW)
    {
      spaceIsEmpty = nodeListByY[0].getNWNeighbor() != null
      && nodeListByY[0].getNWNeighbor().getColor() == BoardColor.EMPTY;
    }
    //NE
    if (d == Direction.NE)
    {
      spaceIsEmpty = nodeListByY[0].getNENeighbor() != null
      && nodeListByY[0].getNENeighbor().getColor() == BoardColor.EMPTY;
    }
    //E
    if (d == Direction.E)
    {
      spaceIsEmpty = nodeListByX[nodeList.Count - 1].getENeighbor() != null
      && nodeListByX[nodeList.Count - 1].getENeighbor().getColor() == BoardColor.EMPTY;
    }
    //SE
    if (d == Direction.SE)
    {
      spaceIsEmpty = nodeListByY[nodeList.Count - 1].getSENeighbor() != null
      && nodeListByY[nodeList.Count - 1].getSENeighbor().getColor() == BoardColor.EMPTY;
    }
    //SW
    if (d == Direction.SW)
    {
      spaceIsEmpty = nodeListByY[nodeList.Count - 1].getSWNeighbor() != null
      && nodeListByY[nodeList.Count - 1].getSWNeighbor().getColor() == BoardColor.EMPTY;
    }
    //W
    if (d == Direction.W)
    {
      spaceIsEmpty = nodeListByX[0].getWNeighbor() != null
      && nodeListByX[0].getWNeighbor().getColor() == BoardColor.EMPTY;
    }
    return spaceIsEmpty;
  }

  public Boolean checkIfCanPush(Direction direction, int columnSize)
  {


    List<Node> nodeList = new List<Node>();
    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }

    List<Node> nodeListByX = nodeList.OrderBy(x => x.getX()).ToList();
    List<Node> nodeListByY = nodeList.OrderBy(x => x.getY()).ToList();

    Boolean canPush = false;
    BoardColor color = nodeList[0].getColor();

    //NW
    if (direction == Direction.NW)
    {
      Node head = nodeListByY[0];
      Node toPush = abalone.getNode(head.getX(), head.getY() - 1);
      if (toPush == null)
      {
        consoleManager.sendMessageToConsole("Trying to 'push' empty space. Invalid move.");
      }
      else if (toPush.getColor() == color)
      {
        consoleManager.sendMessageToConsole("You can't push your own color. Invalid move.");
      }
      else
      {
        consoleManager.sendMessageToConsole("Attempting to build a push move to the NW -- MOVEMENT.CHECKIFPUSH");
        List<Node> OnesToPush = new List<Node>();
        BoardColor opponentColor = toPush.getColor();
        Boolean continueSearching = true;
        OnesToPush.Add(toPush);
        Node next = abalone.getNode(toPush.getX(), toPush.getY() + 1);


      }

    }
    //NE
    if (direction == Direction.NE)
    {
      Node head = nodeListByY[0];
    }
    //E
    if (direction == Direction.E)
    {
      Node head = nodeListByX[nodeList.Count - 1];
    }
    //SE
    if (direction == Direction.SE)
    {
      Node head = nodeListByY[nodeList.Count - 1];
    }
    //SW
    if (direction == Direction.SW)
    {
      Node head = nodeListByY[nodeList.Count - 1];
    }
    //W
    if (direction == Direction.W)
    {
      Node head = nodeListByX[0];
    }
    return canPush;

  }

  public void validateMovement(Direction direction)
  {
    //Check to make sure tiles are selected
    if (InputScript.selectedTiles[0] == null)
    {
      consoleManager.sendMessageToConsole("Tried to  move with no tiles selected. Invalid move.");
      return;
    }
    //Check to make sure Tiles selected are actually in a moveable position
    consoleManager.sendMessageToConsole("Attempting to move " + direction);
    if (!checkIfTilesAreInLine())
    {
      consoleManager.sendMessageToConsole("Tiles selected are not in a column. Invalid move.");
      return;
    }

    //Check to see if the tiles are Sidestepping, or moving in a column
    if (checkIfSideStep(direction))
    {
      if (checkIsSpaceEmptySideStep(direction))
      {
        consoleManager.sendMessageToConsole("SideStep to an empty tile. Moving tile");
        move(direction);
        abalone.updateUIBoard();
        InputScript.deselectAllTiles();
        return;
      }
      else
      {
        consoleManager.sendMessageToConsole("Tried to side step into a non-empty position. Invalid move.");
      }
    }
    else
    {
      if (checkIsSpaceEmptyColumn(direction))
      {
        move(direction);
        abalone.updateUIBoard();
        InputScript.deselectAllTiles();
        return;
      }
      else
      {
        checkIfCanPush(direction, InputScript.selectedTiles.Count);
      }
    }

  }

  //Once movement is validated, this can be called to move tiles
  public void move(Direction direction)
  {
    List<Node> nodeList = new List<Node>();
    foreach (GameObject tile in InputScript.selectedTiles)
    {
      nodeList.Add(abalone.getNode(tile));
    }
    List<Node> neighbors = new List<Node>();
    BoardColor color = nodeList[0].getColor();

    //NW
    if (direction == Direction.NW)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getNWNeighbor());
      }
    }
    //NE
    if (direction == Direction.NE)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getNENeighbor());
      }
    }
    //E
    if (direction == Direction.E)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getENeighbor());
      }
    }
    //SE
    if (direction == Direction.SE)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getSENeighbor());
      }
    }
    //SW
    if (direction == Direction.SW)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getSWNeighbor());
      }
    }
    //W
    if (direction == Direction.W)
    {
      foreach (Node node in nodeList)
      {
        neighbors.Add(node.getWNeighbor());
      }
    }

    //Neighbors assigned. Now Swap empties with Color.
    foreach (Node node in nodeList)
    {
      node.setColor(BoardColor.EMPTY);
    }
    foreach (Node node in neighbors)
    {
      node.setColor(color);
    }

  }

  //Once pushing is confirmed, this can be called to push
  public void push(Direction direction)
  {

  }
  //Check if space in empty
  //sidestep -> if empty: move.
  //pillar - > if empty, move. If not, check advantage. If advantage, push. If not, no push.
  //Once move complete, Deselect all tiles, cycle turns
}


public enum Direction
{
  NE,
  E,
  SE,
  SW,
  W,
  NW

}
