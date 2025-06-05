using Godot;
using Godot.Collections;
using System;
using System.Text.Json;

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

        FileAccess noteFile = FileAccess.Open(jsonPath, FileAccess.ModeFlags.WriteRead);
        noteFile.StoreLine(Json.Stringify(note));
    }



    // SIGNAL AREA
    public void _OnSaveButtonPressed()
    {
        SaveToJson();
    }
}
