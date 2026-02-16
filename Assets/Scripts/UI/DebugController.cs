using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;

    string input;

    public static DebugCommand CREATERELAY;

    public List<object> commandList;

    void Awake()
    {
        CREATERELAY = new DebugCommand("CreateRelay", "Starts a Relay and generates a join code", "createrelay", () =>
        {
            TestRelay.Singleton.CreateRelay();
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) showConsole = !showConsole;

        //ADD THIS TO READ DEBUG LOGS
        //Application.logMessageReceived;
    }

    public void OnToggleDebug(InputValue value)
    {
        showConsole = !showConsole;
    }

    void OnGUI()
    {
        if (!showConsole) return;

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }
}

public class DebugCommandBase 
{
    private string commandId;
    private string commandDesc;
    private string commandFormat;

    public string CommandId { get { return commandId; } }
    public string CommandDesc { get { return commandDesc; } }
    public string CommandFormat { get { return commandFormat; } }

    public DebugCommandBase(string id, string desc, string format)
    { 
        commandId = id;
        commandDesc = desc;
        commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action command;
    public DebugCommand(string id, string desc, string format, Action command) : base (id, desc, format)
    { 
        this.command = command;    
    }

    public void Invoke()
    { 
        command.Invoke();
    }
}
