using System;
using System.Collections;
using Unity;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Agent : MonoBehaviour
{
  Hashtable transpoTable;


  public static int[,] positionValues =  {
                {0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 2, 2, 2, 2, 0},
                {0, 0, 0, 2, 4, 4, 4, 2, 0},
                {0, 0, 2, 4, 8, 8, 4, 2, 0},
                {0, 2, 4, 8, 16, 8, 4, 2, 0},
                {0, 2, 4, 8, 8, 4, 2, 0, 0},
                {0, 2, 4, 4, 4, 2, 0, 0, 0},
                {0, 2, 2, 2, 2, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0}};


  int testDepth = 2;
  State currentState;
  int side;

  int opSide;

  bool firstTurn;
  int numberOfTiles;
  int numberOfOpponentTiles;
  int numberOfCombinationsChecked;

  public Agent(State startState)
  {
    transpoTable = new Hashtable();
    currentState = startState;
    printBoard(currentState.getBoard());
    if (startState.getTurn() == 1)
    {
      side = 1;
      opSide = 2;
      firstTurn = true;
    }
    else
    {
      side = 2;
      opSide = 1;
      firstTurn = false;
    }
    //!!!REMOVE AFTER TESTING
    firstTurn = false;

    numberOfOpponentTiles = getNumberOfColoredPieces(startState, opSide);
    numberOfTiles = getNumberOfColoredPieces(startState, side);
  }

  public void setState(State s)
  {
    currentState = s;
  }

  public State turnOne()
  {
    Generator.generate(currentState);
    int x = currentState.getNextStates().Count;
    System.Random rnd = new System.Random();
    Console.WriteLine(x);
    State nextBoard = currentState.getNextStates().ElementAt(rnd.Next(x));
    printBoard(nextBoard.getBoard());
    return nextBoard;
  }
  public State turn(State startState)
  {
    currentState = startState;
    printBoard(currentState.getBoard());
    if (startState.getTurn() == 1)
    {
      side = 1;
      opSide = 2;
      firstTurn = true;
    }
    else
    {
      side = 2;
      opSide = 1;
      firstTurn = false;
    }

    numberOfOpponentTiles = getNumberOfColoredPieces(startState, opSide);
    numberOfTiles = getNumberOfColoredPieces(startState, side);

    firstTurn = false;
    if (firstTurn)
    {
      return turnOne();
    }
    else
    {
      Generator.generate(currentState);
      toDepth(currentState, 1);
      State bestMove = AlphaBeta(currentState);
      printBoard(bestMove.getBoard());
      if (currentState.getBoard().Cast<int>().SequenceEqual(bestMove.getBoard().Cast<int>()))
      {
        return currentState.getNextStates()[0];
      }
      return bestMove;

    }

  }

  public void toDepth(State currentState, int depth)
  {

    if (depth <= 0)
    {
      return;
    }
    foreach (State s in currentState.getNextStates())
    {
      numberOfCombinationsChecked++;
      Generator.generate(s);
      toDepth(s, depth - 1);

    }
  }

  private State AlphaBeta(State currentState)
  {
    State best = new State(0, 0, 0, new int[1, 1]);
    int alpha = Int32.MinValue;
    int beta = Int32.MaxValue;
    State v = MaxValue(currentState, ref alpha, ref beta, ref best);
    printBoard(v.getBoard());
    Console.WriteLine(v.getValue());
    return (v);
  }



  private State MaxValue(State currentState, ref int alpha, ref int beta, ref State best)
  {

    if (!currentState.getNextStates().Any())
    {
      currentState.setValue(evaluate(currentState));
      return currentState;
    }
    int v = Int32.MinValue;
    State localBest = currentState.getNextStates()[0];

    currentState.getNextStates().Sort();
    foreach (State nextState in currentState.getNextStates())
    {
      State s = MinValue(nextState, ref alpha, ref beta, ref best);
      int min = s.getValue();
      if (min > v)
      {
        v = min;
        nextState.setValue(v);
        localBest = nextState;
      }

      if (v > beta)
      {
        return localBest;
      }
      alpha = Math.Max(alpha, v);
    }

    return localBest;
  }

  private State MinValue(State currentState, ref int alpha, ref int beta, ref State best)
  {

    if (!currentState.getNextStates().Any())
    {
      currentState.setValue(evaluate(currentState));
      return currentState;
    }
    int v = Int32.MaxValue;
    State localBest = currentState.getNextStates()[0];

    foreach (State nextState in currentState.getNextStates())
    {
      State s = MaxValue(nextState, ref alpha, ref beta, ref best);

      int max = s.getValue();
      if (max <= v)
      {
        v = max;
        nextState.setValue(v);
        localBest = nextState;
      }

      if (v <= alpha)
      {
        return localBest;
      }
      beta = Math.Min(beta, v);
    }
    return localBest;
  }



  /*
  private State AlphaBeta(State currentState)
  {
      State best = new State(0, 0, 0, new int[1, 1]);
      int alpha = largeNegative;
      int beta = largePositive;
      Console.WriteLine(MaxValue(currentState, ref largeNegative,ref  largePositive,ref best));
      return best;
  }


  private int MaxValue(State currentState, ref int alpha, ref int beta, ref State best)
  {

      if (!currentState.getNextStates().Any())
      {
          return evaluate(currentState);
      }
      int v = largeNegative;
      //currentState.getNextStates().Sort();
      foreach (State nextState in currentState.getNextStates())
      {
          int min = MinValue(nextState,ref alpha,ref beta, ref best);
          if (min > v)
          {
              v = min;
              best = nextState;
          }

          if (v > beta)
          {
              return v;
          }
          alpha = Math.Max(alpha, v);
      }

      return v;
  }

  private int MinValue(State currentState,ref  int alpha,ref  int beta, ref State best)
  {

      if (!currentState.getNextStates().Any())
      {
          return evaluate(currentState);
      }
      int v = largePositive;
      foreach (State nextState in currentState.getNextStates())
      {
          int max = MaxValue(nextState,ref alpha,ref  beta, ref best);

          if (max <= v)
          {
              v = max;
              best = nextState;

          }

          if (v <= alpha)
          {
              return v;
          }
          beta = Math.Min(beta, v);
      }
      return v;
  }
  */


  //New evaluation function that checks for pushing pieces off, and saving their own pieces
  private int evaluate(State state)
  {
    int newNumberOfOpponentsPieces = getNumberOfColoredPieces(state, opSide);
    int newNumberOfColoredPieces = getNumberOfColoredPieces(state, side);
    int sum = 0;

    if (newNumberOfColoredPieces < numberOfTiles)
    {
      sum -= 1000;
    }
    if (newNumberOfOpponentsPieces < numberOfOpponentTiles)
    {
      sum += 1000;
    }

    if (newNumberOfColoredPieces == 8)
    {
      sum -= 10000;
    }
    if (newNumberOfOpponentsPieces == 8)
    {
      sum += 10000;
    }

    for (int i = 0; i < 9; i++)
    {
      for (int j = 0; j < 9; j++)
      {
        if (state.getBoard()[i, j] == side)
        {
          sum += positionValues[i, j] * 2;
        }
        else if (state.getBoard()[i, j] == opSide)
        {
          sum -= positionValues[i, j];
        }
      }
    }

    return sum;
  }

  // private int evaluate(State state)
  // {
  //   int sum = 0;
  //   for (int i = 0; i < 9; i++)
  //   {
  //     for (int j = 0; j < 9; j++)
  //     {
  //       if (state.getBoard()[i, j] == side)
  //       {
  //         sum += positionValues[i, j] * 2;
  //       }
  //       else if (state.getBoard()[i, j] == opSide)
  //       {
  //         sum -= positionValues[i, j];
  //       }
  //     }
  //   }

  //   if (side == 1)
  //   {
  //     if (state.getWhite() == 8)
  //     {
  //       return 1000000;
  //     }
  //     sum += 10 * (state.getBlacks() - state.getWhite());
  //   }
  //   else
  //   {
  //     if (state.getBlacks() == 8)
  //     {
  //       return -1000000;
  //     }
  //     sum += 10 * (state.getBlacks() - state.getWhite());
  //   }
  //   return sum;
  // }

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

  public int getNumberOfColoredPieces(State state, int color)
  {
    int count = 0;
    for (int i = 0; i < 9; i++)
    {
      for (int j = 0; j < 9; j++)
      {
        if (state.getBoard()[i, j] == color)
        {
          count++;
        }
      }
    }
    return count;
  }

}