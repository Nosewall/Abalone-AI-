using System;
using System.Collections;
using Unity;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Generator
{

  static bool listContains(ref List<int[,]> boardList, int[,] inputBoard)
  {
    // checks if a list contains a specific board
    foreach (int[,] board in boardList)
    {
      var equal = board.Cast<int>().SequenceEqual(inputBoard.Cast<int>());
      if (equal)
      {
        return true;
      }

    }
    return false;
  }






  public static List<State> generate(State state)
  //generates all possible moves 

  {
    int[,] board = state.getBoard();
    int opponentInt;
    int turnInt = state.getTurn();

    if (turnInt == 1)
    {
      opponentInt = 2;
    }
    else
    {
      opponentInt = 1;
    }
    List<State> states = new List<State>();

    List<int[,]> BoardList = new List<int[,]>();
    List<string> MoveList = new List<string>();

    //iterates over the state representation
    for (int i = 0; i < 9; i++)
    {
      for (int j = 0; j < 9; j++)
      {
        if (board[i, j] == turnInt)
        {
          checkSurrounding(board, i, j, turnInt, opponentInt, ref BoardList, ref MoveList, states, state);
        }
      }

    }

    //Console.WriteLine(BoardList.Count);
    //Console.WriteLine(state.getNextStates().Count);


    return states;
  }

  static List<string> boardToString(List<int[,]> boardList)
  {
    //converts the board to a string 
    List<string> stringList = new List<string>();
    foreach (int[,] board in boardList)
    {
      string tempB = "";
      string tempW = "";
      for (int i = 8; i >= 0; i--)
      {
        for (int j = 0; j < 9; j++)
        {

          if (board[i, j] == 1)
          {
            tempB = tempB + positionClassifier(i, j) + "b,";
          }
          else if (board[i, j] == 2)
          {
            tempW = tempW + (positionClassifier(i, j) + "w,");
          }
        }
      }

      string temp = tempB + tempW;

      stringList.Add(temp.Substring(0, temp.Length - 1));
    }
    return stringList;
  }

  static Tuple<List<int[,]>, List<string>> checkSurrounding(int[,] board, int x, int y, int turnInt, int opponentInt, ref List<int[,]> BoardList, ref List<string> MoveList, List<State> states, State currentState)
  {

    try
    {
      //checks the surrounding spaces of a marble to determine if there are possible moves and generate them
      List<State> stateList = states;
      List<int[,]> nextPossibleBoards = BoardList;
      List<string> nextMoves = MoveList;

      // goes through the 6 possible movement directions excluding 
      for (int i = -1; i <= 1; i++)
      {
        for (int j = -1; j <= 1; j++)
        {
          if (i != j)
          {
            int nextX = x + i;
            int nextY = y + j;
            if (inBounds(nextX, nextY, board) && board[x, y] == turnInt)
            {
              if (board[nextX, nextY] == 0) // checks if the next position on the board is free
              {

                singlemoves(board, nextX, nextY, x, y, i, j, ref nextPossibleBoards, ref nextMoves, ref stateList, currentState);

                //if a single move into a blank space is done then checks if there are other marbles that can be used to side step
                for (int i1 = -1; i1 <= 1; i1++)
                {
                  for (int j1 = -1; j1 <= 1; j1++)
                  {
                    if (i1 != j1)
                    {
                      if (inBounds(x + i1, y + j1, board) && inBounds(x + i1 + i, y + j1 + j, board) && board[x + i1, y + j1] == turnInt && board[x + i1 + i, y + j1 + j] == 0)
                      {
                        //checks if a double side step is possible and generates the move and board
                        int[,] newDoubleBoard = new int[9, 9];
                        Array.Copy(board, 0, newDoubleBoard, 0, board.Length);

                        newDoubleBoard[nextX, nextY] = newDoubleBoard[x, y];
                        newDoubleBoard[x + i1 + i, y + j1 + j] = turnInt;
                        newDoubleBoard[x, y] = 0;
                        newDoubleBoard[x + i1, y + j1] = 0;

                        string firstPos = positionClassifier(x, y);
                        string secondPos = positionClassifier(x + i1, y + j1);
                        string doubleMove = "";

                        if (x <= (x + i1))
                        {
                          doubleMove = "s-" + firstPos + "-" + secondPos + "-" + moveClassifier(i, j);
                          if (x == (x + i1))
                          {
                            if (y <= y + j1)
                            {
                              doubleMove = "s-" + firstPos + "-" + secondPos + "-" + moveClassifier(i, j);
                            }
                            else
                            {
                              doubleMove = "s-" + secondPos + "-" + firstPos + "-" + moveClassifier(i, j);
                            }
                          }
                        }
                        else
                        {
                          doubleMove = "s-" + secondPos + "-" + firstPos + "-" + moveClassifier(i, j);
                        }



                        if (!listContains(ref nextPossibleBoards, newDoubleBoard))
                        {
                          nextMoves.Add(doubleMove);
                          nextPossibleBoards.Add(newDoubleBoard);
                          currentState.addState(currentState.getNextTurnCopy(newDoubleBoard));

                        }

                        //if a double sidestep works then checks for an addition triple side step
                        if (inBounds(x + i1 * 2, y + j1 * 2, board) && inBounds(x + i1 * 2 + i, y + j1 * 2 + j, board)
                        && board[x + i1 * 2, y + j1 * 2] == turnInt && board[x + i1 * 2 + i, y + j1 * 2 + j] == 0)
                        {
                          int[,] newTripleBoard = new int[9, 9];
                          Array.Copy(board, 0, newTripleBoard, 0, board.Length);

                          newTripleBoard[nextX, nextY] = newTripleBoard[x, y];
                          newTripleBoard[x + i1 + i, y + j1 + j] = turnInt;
                          newTripleBoard[x + i1 * 2 + i, y + j1 * 2 + j] = turnInt;
                          newTripleBoard[x + i1 * 2, y + j1 * 2] = 0;
                          newTripleBoard[x, y] = 0;
                          newTripleBoard[x + i1, y + j1] = 0;

                          string fPos = positionClassifier(x, y);
                          string sPos = positionClassifier(x + i1 * 2, y + j1 * 2);
                          string triplemoves = "";

                          if (x <= (x + i1 * 2))
                          {
                            triplemoves = "s-" + fPos + "-" + sPos + "-" + moveClassifier(i, j);
                            if (x == (x + i1 * 2))
                            {
                              if (y <= y + j1 * 2)
                              {
                                triplemoves = "s-" + fPos + "-" + sPos + "-" + moveClassifier(i, j);
                              }
                              else
                              {
                                triplemoves = "s-" + sPos + "-" + fPos + "-" + moveClassifier(i, j);
                              }
                            }
                          }
                          else
                          {
                            triplemoves = "s-" + sPos + "-" + fPos + "-" + moveClassifier(i, j);
                          }




                          if (!listContains(ref nextPossibleBoards, newTripleBoard))
                          {
                            nextMoves.Add(triplemoves);
                            nextPossibleBoards.Add(newTripleBoard);
                            currentState.addState(currentState.getNextTurnCopy(newTripleBoard));

                          }
                        }
                      }
                    }
                  }
                }

                int doubleX = x - i;
                int doubleY = y - j;

                //double inline moves
                if (inBounds(doubleX, doubleY, board) && board[doubleX, doubleY] == turnInt)
                {
                  int[,] newDoubleBoard = new int[9, 9];
                  Array.Copy(board, 0, newDoubleBoard, 0, board.Length);

                  newDoubleBoard[nextX, nextY] = newDoubleBoard[x, y];
                  newDoubleBoard[x - i, y - j] = 0;
                  string doubleMove = "i-" + positionClassifier(doubleX, doubleY) + "-" + moveClassifier(i, j);




                  nextMoves.Add(doubleMove);
                  nextPossibleBoards.Add(newDoubleBoard);
                  currentState.addState(currentState.getNextTurnCopy(newDoubleBoard));




                  int tripleX = x - i * 2;
                  int tripleY = y - j * 2;

                  //triple inline moves
                  if (inBounds(tripleX, tripleY, board) && board[tripleX, tripleY] == turnInt)
                  {
                    int[,] newTripleBoard = new int[9, 9];
                    Array.Copy(board, 0, newTripleBoard, 0, board.Length);

                    newTripleBoard[nextX, nextY] = newTripleBoard[x, y];
                    newTripleBoard[tripleX, tripleY] = 0;
                    string triplemoves = "i-" + positionClassifier(tripleX, tripleY) + "-" + moveClassifier(i, j);



                    nextMoves.Add(triplemoves);
                    nextPossibleBoards.Add(newTripleBoard);

                    currentState.addState(currentState.getNextTurnCopy(newTripleBoard));


                  }
                }

              }


              //sumitos 
              if (board[nextX, nextY] == opponentInt)
              {

                int doubleSumitoX = x - i;
                int doubleSumitoY = y - j;
                //2 marble sumito
                if (inBounds(doubleSumitoX, doubleSumitoY, board) && board[doubleSumitoX, doubleSumitoY] == turnInt
                && sumito(x + i * 2, y + j * 2, board, turnInt, opponentInt))
                {
                  int[,] newDoubleBoard = new int[9, 9];
                  Array.Copy(board, 0, newDoubleBoard, 0, board.Length);

                  newDoubleBoard[nextX, nextY] = newDoubleBoard[x, y];
                  if (inBounds(x + i * 2, y + j * 2, board))
                  {
                    newDoubleBoard[x + i * 2, y + j * 2] = opponentInt;
                  }
                  newDoubleBoard[doubleSumitoX, doubleSumitoY] = 0;
                  string doubleMove = "i-" + positionClassifier(doubleSumitoX, doubleSumitoY) + "-" + moveClassifier(i, j);


                  nextPossibleBoards.Add(newDoubleBoard);
                  currentState.addState(currentState.getNextTurnReduceCopy(newDoubleBoard));

                  nextMoves.Add(doubleMove);
                  int tripleX = x - i * 2;
                  int tripleY = y - j * 2;
                }

                int tripleSumitoX = x - i * 2;
                int tripleSumitoY = y - j * 2;
                //3 marble sumito
                if (inBounds(tripleSumitoX, tripleSumitoY, board) && board[tripleSumitoX, tripleSumitoY] == turnInt
                && board[doubleSumitoX, doubleSumitoY] == turnInt
                && sumito(x + i * 3, y + j * 3, board, turnInt, opponentInt)
                && board[x + i * 2, y + j * 2] != turnInt)

                {

                  int[,] newTripleBoard = new int[9, 9];
                  Array.Copy(board, 0, newTripleBoard, 0, board.Length);

                  newTripleBoard[nextX, nextY] = newTripleBoard[x, y];
                  if (inBounds(x + (i * 2), y + (j * 2), board))
                  {
                    newTripleBoard[x + i * 2, y + j * 2] = opponentInt;

                    if (inBounds(x + (i * 3), y + (j * 3), board) && board[x + (i * 2), y + (j * 2)] == opponentInt)
                    {
                      newTripleBoard[x + i * 3, y + j * 3] = opponentInt;
                    }
                  }
                  newTripleBoard[tripleSumitoX, tripleSumitoY] = 0;
                  string tripleSumito = "i-" + positionClassifier(tripleSumitoX, tripleSumitoY) + "-" + moveClassifier(i, j);

                  nextPossibleBoards.Add(newTripleBoard);
                  currentState.addState(currentState.getNextTurnReduceCopy(newTripleBoard));
                  nextMoves.Add(tripleSumito);
                  int tripleX = x - i * 2;
                  int tripleY = y - j * 2;
                }
              }
            }
          }
        }
      }
      return Tuple.Create(nextPossibleBoards, nextMoves);
    }
    catch (IndexOutOfRangeException e)
    {
      Debug.Log(e);
    }
    return null;

  }
  /*
  static bool tripleSumitohelper(int x, int y,int[,] board, int turnInt, int opponentInt)
      {
      if (board[x,y] == opponentInt)
      {
          return ;
      }
      return ;
  }
  */

  static bool sumito(int x, int y, int[,] board, int turnInt, int opponentInt)
  {
    // returns true if the final position in a submito is viable
    if (!inBounds(x, y, board))
    {
      return true;
    }
    else if (board[x, y] == 0)
    {
      return true;

    }
    else if (board[x, y] == opponentInt)
    {
      return false;
    }
    return false;
  }



  static void singlemoves(int[,] board, int nextX, int nextY, int x, int y, int i, int j, ref List<int[,]> nextPossibleBoards, ref List<string> nextMoves, ref List<State> states, State state)
  //creates a single move and the resulting board
  {
    int[,] newboard = new int[9, 9];
    Array.Copy(board, 0, newboard, 0, board.Length);

    newboard[nextX, nextY] = newboard[x, y];
    newboard[x, y] = 0;
    string move = "i-" + positionClassifier(x, y) + "-" + moveClassifier(i, j);

    state.addState(state.getNextTurnCopy(newboard));
    nextPossibleBoards.Add(newboard);
    nextMoves.Add(move);

  }

  /*
  static void doublemoves()
  {

  }

  static void triplemoves()
  {

  }


  static void TripleSideStep(){

  }
  */



  static bool inBounds(int x, int y, int[,] board)
  //returns true if the position is within the bounds of the board and not and out of bounds location
  {

    if (x > 8 || x < 0 || y > 8 || y < 0)
    {
      return false;
    }
    if (board[x, y] == 8)
    {
      return false;
    }
    return true;
  }

  static string positionClassifier(int x, int y)
  // converts the x y coordinates into A-I and 1-9 coordinates
  {
    return Convert.ToChar(73 - x) + "" + (y + 1);
  }

  static string moveClassifier(int deltaX, int deltaY)
  {
    // classifies the moves into the integers
    string retString = "7";
    if (deltaX > 0 && deltaY < 0)
    {
      return retString;
    }
    if (deltaX < 0 && deltaY > 0)
    {
      return "1";
    }
    if (deltaX > 0)
    {
      return "5";
    }
    if (deltaX < 0)
    {
      return "11";
    }
    if (deltaY > 0)
    {
      return "3";
    }
    if (deltaY < 0)
    {
      return "9";
    }
    return retString;
  }

  static void printBoard(int[,] board)
  //prints the board
  {
    for (int i = 0; i < 9; i++)
    {
      for (int j = 0; j < 9; j++)
      {
        if (i % 2 == 0)
        {
          Console.Write("" + board[i, j] + " ");

        }
        else
        {
          Console.Write(board[i, j] + " ");
        }
      }
      Console.WriteLine();
    }
    Console.WriteLine();
  }

}