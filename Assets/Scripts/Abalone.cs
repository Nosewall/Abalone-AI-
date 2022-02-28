using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abalone : MonoBehaviour
{
  public List<Node> boardState;

  public void generateBoard()
  {
    boardState = BoardBuilder.createBoard();
  }
  public void updateBoard()
  {

  }

}

//The digital representation of a single board tile
public class Node
{
  private int xPosition; // X position 1 - 21
  private int yPosition; // Y position 1 - 11
  private BoardColor color; // Current state
  private List<Node> adjacentNodes; // All adjacent Nodes
  private string name; //ID name of the node on the board
  private bool deadZone; // Is this a tile off the board?


  public Node(string name, BoardColor startingColor, bool isDead, int x, int y)
  {
    this.name = name;
    this.color = startingColor;
    this.deadZone = isDead;
    adjacentNodes = new List<Node>();
  }

  public void addNodeToAdjacent(Node newNode)
  {
    adjacentNodes.Add(newNode);
  }
}


