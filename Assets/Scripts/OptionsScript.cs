using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsScript : MonoBehaviour
{
  public OptionsForLaunch launchOptions;

  TMP_InputField timeInput;
  TMP_InputField turnInput;
  TextMeshProUGUI confirmationText;


  Toggle playerVPlayerInput;
  Toggle playerVAgentInput;
  Toggle AgentVAgentInput;

  Toggle blackInput;
  Toggle whiteInput;

  Toggle defaultLayoutInput;
  Toggle belgianDaisyInput;
  Toggle germanDaisyInput;
  // Start is called before the first frame update
  void Start()
  {
    assignInputs();

  }

  void assignInputs()
  {
    GameObject playerVPlayerInputObject = GameObject.Find("Player vs Player Toggle");
    GameObject playerVAgentInputObject = GameObject.Find("Player Vs Agent Toggle");
    GameObject AgentVAgentInputObject = GameObject.Find("Agent vs Agent Toggle");
    playerVPlayerInput = playerVPlayerInputObject.GetComponent<Toggle>();
    playerVAgentInput = playerVAgentInputObject.GetComponent<Toggle>();
    AgentVAgentInput = AgentVAgentInputObject.GetComponent<Toggle>();

    GameObject blackInputObject = GameObject.Find("Black Toggle");
    GameObject whiteInputObject = GameObject.Find("White Toggle");
    blackInput = blackInputObject.GetComponent<Toggle>();
    whiteInput = whiteInputObject.GetComponent<Toggle>();

    GameObject defaultLayoutInputObject = GameObject.Find("Default Board Toggle");
    GameObject belgianDaisyInputObject = GameObject.Find("Belgin Daisy Toggle");
    GameObject germanDaisyInputObject = GameObject.Find("German Daisy Toggle");

    defaultLayoutInput = defaultLayoutInputObject.GetComponent<Toggle>();
    belgianDaisyInput = belgianDaisyInputObject.GetComponent<Toggle>();
    germanDaisyInput = germanDaisyInputObject.GetComponent<Toggle>();

    confirmationText = GameObject.Find("Limit Confirmation Text").GetComponent<TextMeshProUGUI>();

    GameObject timeLimitObject = GameObject.Find("Time Limit Input");
    GameObject turnLimitObject = GameObject.Find("Turn Limit Input");
    timeInput = timeLimitObject.GetComponent<TMP_InputField>();
    turnInput = turnLimitObject.GetComponent<TMP_InputField>();
  }

  public void updateOptions()
  {
    if (defaultLayoutInput.isOn)
    {
      launchOptions.setLayout(OptionsForLaunch.Layout.Default);
    }
    else if (belgianDaisyInput.isOn)
    {
      launchOptions.setLayout(OptionsForLaunch.Layout.Belgian);
    }
    else if (germanDaisyInput.isOn)
    {
      launchOptions.setLayout(OptionsForLaunch.Layout.German);
    }

    if (blackInput.isOn)
    {
      launchOptions.setColor(OptionsForLaunch.Color.Black);
    }
    else if (whiteInput.isOn)
    {
      launchOptions.setColor(OptionsForLaunch.Color.White);
    }

    if (playerVPlayerInput.isOn)
    {
      launchOptions.setPlayers(OptionsForLaunch.Players.PVP);
    }
    if (playerVAgentInput.isOn)
    {
      launchOptions.setPlayers(OptionsForLaunch.Players.PVA);
    }
    if (AgentVAgentInput.isOn)
    {
      launchOptions.setPlayers(OptionsForLaunch.Players.AVA);
    }
  }

  public void submitLimits()
  {
    confirmationText.text = "Confirmed!";

    if (!timeInput.text.Equals(""))
    {
      int timeInt = Int32.Parse(timeInput.text);


      if (timeInt > 0)
      {
        launchOptions.setTimeLimit(timeInt);
      }
      else
      {
        launchOptions.setTimeLimit(0);
      }
    }
    else
    {
      launchOptions.setTimeLimit(0);
    }


    if (!turnInput.text.Equals(""))
    {
      int turnInt = Int32.Parse(turnInput.text);

      if (turnInt > 0)
      {
        launchOptions.setTurnLimit(turnInt);
      }
      else
      {
        launchOptions.setTurnLimit(0);
      }
    }
    else
    {
      launchOptions.setTurnLimit(0);
    }
  }




  // Update is called once per frame
  void Update()
  {

  }
}
