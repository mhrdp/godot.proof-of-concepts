using Godot;
using System.Collections.Generic;

public partial class DialogueBox : ColorRect
{
    [Export] public PackedScene choicesBoxScene { get; set; }

    private string csvPath;

    private Label nameLabel;
    private RichTextLabel dialogueLabel;

    private int dialogueCount;
    private int dialogueLimit;
    private List<string> dialogueLog;
    private List<Dictionary<string, object>> dialogueList;
    private Dictionary<string, Dictionary<string, string>> sceneState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        csvPath = "res://modules/dialogue/game_dialogue_sheets.csv";
        nameLabel = GetNode<Label>("%NameLabel");
        dialogueLabel = GetNode<RichTextLabel>("%DialogueLabel");

        dialogueLog = new List<string>();
        sceneState = new Dictionary<string, Dictionary<string, string>>();

        dialogueList = ReadCsv(csvPath);
        dialogueCount = dialogueList.Count;
        dialogueLimit = dialogueList.Count;
        GD.Print(dialogueCount);
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
                        // Forward the dialogue
                        int count = dialogueLimit - dialogueCount;
                        if (count < dialogueLimit)
                        {
                            string characterName = (string)dialogueList[count]["characterName"];
                            object dialogueLine = (string)dialogueList[count]["dialogueLine"];

                            if (characterName == "null")
                            {
                                nameLabel.Text = "";
                            }

                            if (dialogueLine.ToString().Contains("|"))
                            {
                                string[] dialogueChoices = dialogueLine.ToString().Split("|");
                                for (int index = 0; index < dialogueChoices.Length; index++)
                                {
                                    ChoicesBox choicesBox = choicesBoxScene.Instantiate<ChoicesBox>();
                                    GetTree().Root.AddChild(choicesBox);
                                    choicesBox.AddChoices(dialogueChoices[index]);
                                    choicesBox.AddGap();
                                }
                            }

                            if (!dialogueLine.ToString().Contains("|"))
                            {
                                nameLabel.Text = characterName;
                                dialogueLabel.Text = (string)dialogueLine;
                            }
                        } // if count < dialogueLimit
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
