using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoardBuilder : MonoBehaviour
{
  public Abalone abalone;
  //Function that creates all the nodes and gives them the proper coordinates
  public static List<Node> createBoard()
  {
    List<Node> boardState = new List<Node>();

    //Row J
    int Jindex = 4;
    for (int x = 6; x <= 16; x = x + 2)
    {
      Node newNode = new Node("J" + (Jindex), BoardColor.EMPTY, true, x, 11);
      Jindex++;
      boardState.Add(newNode);
    }

    //Row I
    int iIndex = 5;
    boardState.Add(new Node("I4", BoardColor.EMPTY, true, 5, 10));
    for (int x = 7; x <= 15; x = x + 2)
    {
      Node newNode = new Node("I" + (iIndex), BoardColor.EMPTY, false, x, 10);
      iIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("I10", BoardColor.EMPTY, true, 17, 10));

    //Row H
    int hIndex = 4;
    boardState.Add(new Node("H3", BoardColor.EMPTY, true, 4, 9));
    for (int x = 6; x <= 16; x = x + 2)
    {
      Node newNode = new Node("H" + (hIndex), BoardColor.EMPTY, false, x, 9);
      hIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("H10", BoardColor.EMPTY, true, 18, 9));

    //Row G 
    int gIndex = 3;
    boardState.Add(new Node("G2", BoardColor.EMPTY, true, 3, 8));
    for (int x = 5; x <= 17; x = x + 2)
    {
      Node newNode = new Node("G" + (gIndex), BoardColor.EMPTY, false, x, 8);
      gIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("G10", BoardColor.EMPTY, true, 19, 8));

    //Row F
    int fIndex = 2;
    boardState.Add(new Node("F1", BoardColor.EMPTY, true, 2, 7));
    for (int x = 4; x <= 18; x = x + 2)
    {
      Node newNode = new Node("F" + (fIndex), BoardColor.EMPTY, false, x, 7);
      fIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("F10", BoardColor.EMPTY, true, 20, 7));

    //Row E
    int eIndex = 1;
    boardState.Add(new Node("E0", BoardColor.EMPTY, true, 1, 6));
    for (int x = 3; x <= 19; x = x + 2)
    {
      Node newNode = new Node("E" + (eIndex), BoardColor.EMPTY, false, x, 6);
      eIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("E10", BoardColor.EMPTY, true, 21, 6));

    //Row D
    int dIndex = 1;
    boardState.Add(new Node("D0", BoardColor.EMPTY, true, 2, 5));
    for (int x = 4; x <= 18; x = x + 2)
    {
      Node newNode = new Node("D" + (dIndex), BoardColor.EMPTY, false, x, 5);
      dIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("D9", BoardColor.EMPTY, true, 20, 5));

    //Row C
    int cIndex = 1;
    boardState.Add(new Node("C0", BoardColor.EMPTY, true, 2, 4));
    for (int x = 5; x <= 17; x = x + 2)
    {
      Node newNode = new Node("C" + (cIndex), BoardColor.EMPTY, false, x, 4);
      cIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("C8", BoardColor.EMPTY, true, 19, 4));

    //Row B
    int bIndex = 1;
    boardState.Add(new Node("B0", BoardColor.EMPTY, true, 4, 3));
    for (int x = 6; x <= 16; x = x + 2)
    {
      Node newNode = new Node("B" + (bIndex), BoardColor.EMPTY, false, x, 3);
      bIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("B7", BoardColor.EMPTY, true, 18, 3));

    //Row A
    int aIndex = 1;
    boardState.Add(new Node("A0", BoardColor.EMPTY, true, 5, 2));
    for (int x = 7; x <= 15; x = x + 2)
    {
      Node newNode = new Node("A" + (aIndex), BoardColor.EMPTY, false, x, 2);
      aIndex++;
      boardState.Add(newNode);
    }
    boardState.Add(new Node("A6", BoardColor.EMPTY, true, 17, 2));

    //Row Zero
    int ZeroIndex = 0;
    for (int x = 6; x < 17; x = x + 2)
    {
      Node newNode = new Node("0" + (ZeroIndex), BoardColor.EMPTY, true, x, 1);
      ZeroIndex++;
      boardState.Add(newNode);
    }


    return boardState;
  }


  public void generateAllNeighbors(List<Node> nodes)
  {
    //First get all the tiles that aren't in a deadzone. These are the ones that will have neighbors.
    List<Node> tilesThatArentDead = new List<Node>();
    foreach (Node node in nodes)
    {
      if (!node.getDeadZone())
      {
        tilesThatArentDead.Add(node);
      }
    }

    //Then get the coordinates of each live tile, and use them to generate the list of neighbors.
    foreach (Node node in tilesThatArentDead)
    {
      int xcoord = node.getX();
      int ycoord = node.getY();

      //get NW neighbor
      node.addNodeToAdjacent(abalone.getNode(xcoord - 1, ycoord + 1));
      //get NE neighbor
      node.addNodeToAdjacent(abalone.getNode(xcoord + 1, ycoord + 1));
      //get E neighbor
      node.addNodeToAdjacent(abalone.getNode(xcoord + 2, ycoord));
      //get SE neighbor
      node.addNodeToAdjacent(abalone.getNode(xcoord + 1, ycoord - 1));
      //get SW neighbor
      node.addNodeToAdjacent(abalone.getNode(xcoord - 1, ycoord - 1));
      //get W neighbor
      node.addNodeToAdjacent(abalone.getNode(xcoord - 2, ycoord));
    }

  }

}