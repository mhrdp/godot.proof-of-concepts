using Godot;
using System;

public partial class NoteWritingFuncs : VBoxContainer
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
        noteUID = GetNode<Label>("%NoteUID");

        GD.Print(saveButton);
    }


    // CUSTOM FUNCTIONS
    public Godot.Collections.Dictionary<string, Variant> SaveToJson()
    {
        string savePath = "res://modules/notes_tech/notes_data.json";

        return new Godot.Collections.Dictionary<string, Variant>()
        {
            {"note_id", noteUID.Text},
            {"note_title", noteTitle.Text},
            {"note_content", noteArea.Text},
        };
    }

    public int GenerateUID()
    {
        // Generate uid like: "note00001, note00002, etc"
        // Make sure there's no duplicate from previous created notes

        return 0;
    }
}
