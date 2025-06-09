using Godot;
using Godot.Collections;
using System;

public partial class NoteWritingJson : VBoxContainer
{
    private Button saveButton;
    private Button closeButton;
    private TextEdit noteArea;
    private Label noteTitle;
    private Label noteUID;

    public override void _Ready()
    {
        saveButton = GetNode<Button>("%SaveButton");
        closeButton = GetNode<Button>("%CloseButton");
        noteArea = GetNode<TextEdit>("%NoteWriting");
        noteTitle = GetNode<Label>("%NoteTitle");

        SaveToJson();
    }


    // CUSTOM FUNCTIONS
    public void SaveToJson()
    {
        string jsonPath = "res://modules/notes_json/data/notes_data.json";

        // Godot Dictionary, do not confuse with C# Dictionary
        Dictionary<string, Dictionary<string, string>> note =
            new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> data = new Dictionary<string, string>();

        data.Add("note_title", "How to strike it rich");
        data.Add("note_desc", "Legit 100%!!");
        data.Add("note_content", "Who knows?");

        note.Add("0002", data);

        if (!FileAccess.FileExists(jsonPath))
        {
            FileAccess createNoteFile = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Write);
            createNoteFile.StoreLine(Json.Stringify(note));
        }
        else
        {
            Json noteFile = new Json();
            FileAccess readNoteFile = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
            Error parsedNoteFile = noteFile.Parse(readNoteFile.GetAsText());

            if (parsedNoteFile == Error.Ok)
            {
                Dictionary<string, Dictionary<string, string>> noteFileDict =
                    noteFile.Data.AsGodotDictionary<string, Dictionary<string, string>>();

            }
            readNoteFile.Close();

        }
    }



    // SIGNAL AREA
    public void _OnSaveButtonPressed()
    {
        SaveToJson();
    }
}
