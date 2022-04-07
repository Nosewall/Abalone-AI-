using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoardBuilder : MonoBehaviour
{
  public ConsoleManager consoleManager;
  public Abalone abalone;
  //Function that creates all the nodes and gives them the proper coordinates
  public static Node[,] createBoard()
  {
    char firstLetterOfName = 'I';
    Node[,] nodeList = new Node[9, 9];
    for (int i = 0; i < 9; i++)
    {
      switch (i)
      {
        case 0:
          firstLetterOfName = 'I';
          break;
        case 1:
          firstLetterOfName = 'H';
          break;
        case 2:
          firstLetterOfName = 'G';
          break;
        case 3:
          firstLetterOfName = 'F';
          break;
        case 4:
          firstLetterOfName = 'E';
          break;
        case 5:
          firstLetterOfName = 'D';
          break;
        case 6:
          firstLetterOfName = 'C';
          break;
        case 7:
          firstLetterOfName = 'B';
          break;
        case 8:
          firstLetterOfName = 'A';
          break;
      }
      for (int j = 0; j < 9; j++)
      {
        if (checkIfNullCoordinates(i, j))
        {
          nodeList[i, j] = null;
        }
        else
        {
          Node newNode = new Node("" + firstLetterOfName + (j + 1), BoardColor.EMPTY, false, j, i);
          nodeList[i, j] = newNode;
        }
      }
    }
    return nodeList;
  }

  //Quick reference function to check if we're at null coordinates for game board generation
  public static bool checkIfNullCoordinates(int i, int j)
  {
    return i == 0 && j < 4 || i == 1 && j < 3 || i == 2 && j < 2 || i == 3 && j < 1 || i == 5 && j > 7 || i == 6 && j > 6 || i == 7 && j > 5 || i == 8 && j > 4;
  }


  //Generate all the neighbor nodes for each Node.
  public void generateAllNeighbors(Node[,] nodes)
  {
    foreach (Node node in nodes)
    {
      if (node != null)
      {
        int x = node.getX();
        int y = node.getY();
        node.getNeighbors();

        //Get the coordinates
        //Get all nodes around it, as long as their coordinates aren't < 0 or > 8

        //NW Neighbor
        if (y - 1 >= 0 && !abalone.isNodeNull(x, y - 1))
        {
          node.getNeighbors()[0] = (abalone.getNode(x, y - 1));
        }

        //NE Neighbor
        if (y - 1 >= 0 && x + 1 <= 8 && !abalone.isNodeNull(x + 1, y - 1))
        {
          node.getNeighbors()[1] = (abalone.getNode(x + 1, y - 1));
        }

        //E Neighbor
        if (x + 1 <= 8 && !abalone.isNodeNull(x + 1, y))
        {
          node.getNeighbors()[2] = (abalone.getNode(x + 1, y));
        }

        //SE Neighbor
        if ((y + 1 < 9) && !abalone.isNodeNull(x, y + 1))
        {
          node.getNeighbors()[3] = (abalone.getNode(x, y + 1));
        }

        //SW Neighbor
        if (x - 1 >= 0 && y + 1 <= 8 && !abalone.isNodeNull(x - 1, y + 1))
        {
          node.getNeighbors()[4] = abalone.getNode(x - 1, y + 1);
        }
        //W Neighbor
        if (x - 1 >= 0 && !abalone.isNodeNull(x - 1, y))
        {
          node.getNeighbors()[5] = abalone.getNode(x - 1, y);
        }
      }

    }
  }

}