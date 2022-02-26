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

  public enum Players
  {
    PVP,
    PVA,
    AVA
  }

  public static bool initialized = false;
  public static Layout layout;
  public static Color color;
  public static Players players;

  void start()
  {
    if (!initialized)
    {
      initialized = true;
      layout = Layout.Default;
      color = Color.White;
      players = Players.PVP;
    }
  }
  public void setPlayers(Players newPLayers)
  {
    players = newPLayers;
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

  public Players GetPlayers()
  {
    return players;
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
