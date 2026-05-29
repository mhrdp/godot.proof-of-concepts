using Godot;
using System.Collections.Generic;

public partial class DialoguePrimary : ColorRect
{
    // Signal needs to be suffixed with "EventHandler" in C#
    [Signal] public delegate void DialogueRequest_EventHandler();
    [Signal] public delegate void DialogueClose_EventHandler();
    [Signal] public delegate void WhereMouseClicked_EventHandler();

    // Export
    [Export] public PackedScene CHOICEBOX_SCENE { get; set; }

    // Node
    private Label NAME_LABEL;
    private RichTextLabel DIALOGUE_LABEL;

    // Class variables
    private string csvPath;
    private int currentDialogueId;
    private List<string> dialogueLog;
    private List<Dictionary<string, object>> dialogueList;
    private Dictionary<string, Dictionary<string, string>> sceneState;


    // Godot built-in functions
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        csvPath = "res://modules/dialogue/game_dialogue_sheets.csv";
        NAME_LABEL = GetNode<Label>("%NameLabel");
        DIALOGUE_LABEL = GetNode<RichTextLabel>("%DialogueLabel");

        dialogueLog = new List<string>();
        sceneState = new Dictionary<string, Dictionary<string, string>>();

        dialogueList = ReadCsv(csvPath);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed)
            {
                int getCurrentDialogueId;
                DialogueManager(mouseEvent, currentDialogueId);
                getCurrentDialogueId = (int)dialogueList[currentDialogueId]["dialogueId"];
                currentDialogueId = getCurrentDialogueId;
            }
        }
    }


    // Developer custom functions
    private void DialogueManager(InputEventMouseButton @mouseEvent, int dialogueId)
    {
        switch (@mouseEvent.ButtonIndex)
        {
            case MouseButton.Left:
                break;

            case MouseButton.Right:
                break;

            default:
                break;
        }
    }

    private List<Dictionary<string, object>> ReadCsv(string filePath)
    {
        List<Dictionary<string, object>> listOfCsvData = new
            List<Dictionary<string, object>>();

        using Godot.FileAccess rawCsvData = Godot.FileAccess.Open(
                filePath, Godot.FileAccess.ModeFlags.Read
        );

        int lineIndex = -1;
        string[] headers = [];
        while (!rawCsvData.EofReached())
        {
            lineIndex += 1;
            string[] data = rawCsvData.GetCsvLine(",");

            if (lineIndex == 0)
            {
                headers = data;
            }

            if (lineIndex > 0)
            {
                Dictionary<string, object> csvData = new
                    Dictionary<string, object>();

                for (int headersIndex = 0; headersIndex < headers.Length; headersIndex++)
                {
                    csvData[headers[headersIndex]] = data[headersIndex];
                }
                listOfCsvData.Add(csvData);
            }
        } // while loop for rawCsvData
        return listOfCsvData;
    } // readCsv function
}
