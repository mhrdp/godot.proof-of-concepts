using Godot;
using System.Collections.Generic;

public partial class DialogueBox : ColorRect
{
    [Export] public PackedScene CHOICEBOX_SCENE { get; set; }

    private Label NAME_LABEL;
    private RichTextLabel DIALOGUE_LABEL;

    private string csvPath;

    private int dialogueCount;
    private int dialogueLimit;
    private List<string> dialogueLog;
    private List<Dictionary<string, object>> dialogueList;
    private Dictionary<string, Dictionary<string, string>> sceneState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        csvPath = "res://modules/dialogue/game_dialogue_sheets.csv";
        NAME_LABEL = GetNode<Label>("%NameLabel");
        DIALOGUE_LABEL = GetNode<RichTextLabel>("%DialogueLabel");

        dialogueLog = new List<string>();
        sceneState = new Dictionary<string, Dictionary<string, string>>();

        dialogueList = ReadCsv(csvPath);
        dialogueCount = dialogueList.Count;
        dialogueLimit = dialogueList.Count;
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
