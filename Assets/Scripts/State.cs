using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State : IComparable<State>
{


  int blackMarbles;
  int whiteMarbles;
  int turn;
  int[,] board = new int[9, 9];
  List<State> nextStates;

  int value;
  public State(int black, int white, int turn, int[,] board)
  {
    blackMarbles = black;
    whiteMarbles = white;
    this.turn = turn;
    this.board = board;
    nextStates = new List<State>();
  }

  public State getCopy(){
        return new State(blackMarbles,whiteMarbles,turn,board);
    }


  public int getValue()
  {
    return value;
  }

  public void setValue(int x)
  {
    value = x;
  }

  public int getTurn()
  {
    return turn;
  }

  public int getWhite()
  {
    return whiteMarbles;
  }

  public int getBlacks()
  {
    return whiteMarbles;
  }

  public int[,] getBoard()
  {
    return this.board;
  }

  public List<State> getStateList()
  {
    return nextStates;
  }



  public State getNextTurnCopy(int[,] nextBoard)
  {
    if (turn == 1)
    {
      return (new State(blackMarbles, whiteMarbles, 2, nextBoard));
    }
    else
    {
      return (new State(blackMarbles, whiteMarbles, 1, nextBoard));
    }
  }
  public State getNextTurnReduceCopy(int[,] nextBoard)
  {
    if (turn == 1)
    {
      return (new State(blackMarbles, whiteMarbles - 1, 2, nextBoard));
    }
    else
    {
      return (new State(blackMarbles - 1, whiteMarbles, 1, nextBoard));
    }

  }

  public int getTurnMarble()
    {
        if (turn == 1)
        {
            return getBlacks();
        }
        else
        {
            return getWhite();
        }
    }

    public int getOppTurnMarble()
    {
        if (turn == 2)
        {
            return getBlacks();
        }
        else
        {
            return getWhite();
        }
    }

  public void addState(State state)
  {
    nextStates.Add(state);
  }

  public List<State> getNextStates()
  {
    return nextStates;
  }

  public int CompareTo(State compareState)
  {
    // A null value means that this object is greater.
    if (compareState == null)
      return 1;

    else
      return this.value.CompareTo(compareState.value);
  }
}