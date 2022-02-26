using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{

  public GameObject consolePanel, textObject;
  public static int maxMessages = 200;

  [SerializeField]
  public static List<Message> messageList = new List<Message>();

  void Start()
  {

  }
  void Update()
  {
    //If there is a new message
  }

  public static void debugPrintAllMessages()
  {
    foreach (Message message in messageList)
    {
      Debug.Log(message.text);
    }
  }

  public void sendMessageToConsole(string newMessage)
  {
    if (messageList.Count > maxMessages)
    {
      Destroy(messageList[0].textObject.gameObject);
      messageList.Remove(messageList[0]);
    }
    Debug.Log("Sending " + newMessage + "To messageList");
    Message message = new Message();
    message.text = newMessage;
    GameObject newText = Instantiate(textObject, consolePanel.transform);

    message.textObject = newText.GetComponent<Text>();
    message.textObject.text = message.text;
    messageList.Add(message);
    Debug.Log(messageList);
  }
}

[System.Serializable]
public class Message
{
  public string text;
  public Text textObject;
}
