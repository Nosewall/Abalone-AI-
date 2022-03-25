using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsForLaunch : MonoBehaviour
{

  public static int timeLimit = 0;
  public static int turnLimit = 0;
  public enum Layout
  {
    Default,
    Belgian,
    German
  }

  public enum Color
  {
    Black,
    White
  }

  public bool whiteIsAgent;
  public bool blackIsAgent;

  public static bool initialized = false;
  public static Layout layout;
  public static Color color;

  void start()
  {
    if (!initialized)
    {
      initialized = true;
      layout = Layout.Default;
      color = Color.White;
      whiteIsAgent = false;
      blackIsAgent = false;
    }
  }

  public void setColor(Color newColor)
  {
    color = newColor;
  }

  public void setLayout(Layout newLayout)
  {
    layout = newLayout;
  }

  public Layout GetLayout()
  {
    return layout;
  }

  public void setBlackAgent(bool agent)
  {
    this.blackIsAgent = agent;
  }

  public bool isBlackAnAgent()
  {
    return blackIsAgent;
  }

  public void setWhiteAgent(bool agent)
  {
    this.whiteIsAgent = agent;
  }

  public bool isWhiteAnAgent()
  {
    return whiteIsAgent;
  }

  public Color GetColor()
  {
    return color;
  }

  public int getTimeLimit()
  {
    return timeLimit;
  }

  public int getTurnLimit()
  {
    return turnLimit;
  }

  public void setTimeLimit(int time)
  {
    timeLimit = time;
  }

  public void setTurnLimit(int turns)
  {
    turnLimit = turns;
  }


}
