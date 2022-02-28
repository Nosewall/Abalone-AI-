using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoardBuilder : MonoBehaviour
{
  public static List<Node> createBoard()
  {
    List<Node> boardState = new List<Node>();

    //Row J
    for (int x = 6; x <= 16; x = x + 2)
    {
      Node newNode = new Node("J" + (x - 1), BoardColor.EMPTY, true, x, 11);
      boardState.Add(newNode);
    }

    //Row I
    boardState.Add(new Node("I4", BoardColor.EMPTY, true, 5, 10));
    for (int x = 7; x <= 15; x = x + 2)
    {
      Node newNode = new Node("I" + (x - 2), BoardColor.EMPTY, false, x, 10);
    }
    boardState.Add(new Node("I10", BoardColor.EMPTY, true, 17, 10));

    //Row H
    boardState.Add(new Node("H3", BoardColor.EMPTY, true, 4, 9));
    for (int x = 6; x <= 16; x = x + 2)
    {
      Node newNode = new Node("I" + (x - 2), BoardColor.EMPTY, false, x, 9);
    }
    boardState.Add(new Node("H10", BoardColor.EMPTY, true, 18, 9));

    //Row G 
    boardState.Add(new Node("G2", BoardColor.EMPTY, true, 3, 8));
    for (int x = 5; x <= 17; x = x + 2)
    {
      Node newNode = new Node("G" + (x - 2), BoardColor.EMPTY, false, x, 8);
    }
    boardState.Add(new Node("G10", BoardColor.EMPTY, true, 19, 8));

    //Row F
    boardState.Add(new Node("F1", BoardColor.EMPTY, true, 2, 7));
    for (int x = 4; x <= 18; x = x + 2)
    {
      Node newNode = new Node("F" + (x - 2), BoardColor.EMPTY, false, x, 7);
    }
    boardState.Add(new Node("F10", BoardColor.EMPTY, true, 20, 7));

    //Row E
    boardState.Add(new Node("E0", BoardColor.EMPTY, true, 1, 6));
    for (int x = 3; x <= 19; x = x + 2)
    {
      Node newNode = new Node("E" + (x - 2), BoardColor.EMPTY, false, x, 6);
    }
    boardState.Add(new Node("E10", BoardColor.EMPTY, true, 21, 6));

    //Row D
    boardState.Add(new Node("D0", BoardColor.EMPTY, true, 2, 5));
    for (int x = 4; x <= 18; x = x + 2)
    {
      Node newNode = new Node("D" + (x - 3), BoardColor.EMPTY, false, x, 5);
    }
    boardState.Add(new Node("D9", BoardColor.EMPTY, true, 20, 5));

    //Row C
    boardState.Add(new Node("C0", BoardColor.EMPTY, true, 2, 4));
    for (int x = 5; x <= 17; x = x + 2)
    {
      Node newNode = new Node("C" + (x - 4), BoardColor.EMPTY, false, x, 4);
    }
    boardState.Add(new Node("C8", BoardColor.EMPTY, true, 19, 4));

    //Row B
    boardState.Add(new Node("B0", BoardColor.EMPTY, true, 4, 3));
    for (int x = 6; x <= 16; x = x + 2)
    {
      Node newNode = new Node("B" + (x - 5), BoardColor.EMPTY, false, x, 3);
    }
    boardState.Add(new Node("B7", BoardColor.EMPTY, true, 18, 3));

    //Row A
    boardState.Add(new Node("A0", BoardColor.EMPTY, true, 5, 2));
    for (int x = 7; x <= 15; x = x + 2)
    {
      Node newNode = new Node("A" + (x - 6), BoardColor.EMPTY, false, x, 2);
    }
    boardState.Add(new Node("A6", BoardColor.EMPTY, true, 17, 2));

    //Row Zero
    for (int x = 6; x < 17; x = x + 2)
    {
      boardState.Add(new Node("0" + (x - 5), BoardColor.EMPTY, true, x, 1));
    }
    return boardState;
  }

}