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

        var dialog = ReadCsv(csvPath);
        for (int i = 0; i < dialog.Count; i++)
        {
            GD.Print(dialog.Count);
            foreach (KeyValuePair<string, object> kvp in dialog[i])
            {
                GD.Print($"{kvp.Key}: {kvp.Value}");
            }
            GD.Print($"\n");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
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
        }
        return listOfCsvData;
    } // readCsv
}
