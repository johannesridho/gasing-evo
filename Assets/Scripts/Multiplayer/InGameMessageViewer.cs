using UnityEngine;
using System.Collections;

public class InGameMessageViewer : MonoBehaviour
{

    float displayTime = 3f;
    float startTime = 0f;
    bool isMessageShown = false;
    string playerDeathMessage = "";
    bool isMessageShownForever = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (MultiplayerManager.instance.isGameStarted)
        {
            if (isMessageShown)
            {
                if (((Time.time - startTime) < displayTime) || isMessageShownForever)
                {
                    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                    GUILayout.BeginVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(playerDeathMessage);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndVertical();
                    GUILayout.EndArea();
                }
                else
                {
                    isMessageShown = false;
                }
            }
        }
    }

    public void showPlayerDeadMessage(NetworkPlayer player)
    {
        playerDeathMessage = MultiplayerManager.getMPPlayer(player).playerName + " has been defeated";
        isMessageShown = true;
        isMessageShownForever = false;
        startTime = Time.time;
    }

    public void showMyPlayerDeadScreen()
    {
        playerDeathMessage = "You have been defeated!";
        isMessageShown = true;
        isMessageShownForever = true;
        startTime = Time.time;
    }
}
