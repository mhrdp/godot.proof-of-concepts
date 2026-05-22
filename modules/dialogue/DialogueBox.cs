using Godot;
using System.Collections.Generic;

public partial class DialogueBox : ColorRect
{
    private string csvPath = "res://modules/dialogue/game_dialogue_sheets.csv";

    private Label nameLabel;
    private RichTextLabel dialogueLabel;

    private List<string> dialogueLog;
    private Dictionary<string, Dictionary<string, string>> sceneState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        nameLabel = GetNode<Label>("NameLabel");
        dialogueLabel = GetNode<RichTextLabel>("DialogueLabel");

        dialogueLog = new List<string>();
        sceneState = new Dictionary<string, Dictionary<string, string>>();

        List<Dictionary<string, object>> dialog = ReadCsv(csvPath);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent e)
    {
        if (e is InputEventMouseButton mouseInput)
        {
            if (mouseInput.Pressed)
            {
                switch (mouseInput.ButtonIndex)
                {
                    case MouseButton.Left:
                        GD.Print("Forward the dialog");
                        break;
                    case MouseButton.Right:
                        GD.Print("Open Menu");
                        break;
                    default:
                        break;
                }
            }
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
