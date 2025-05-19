using Godot;
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
        noteUID = GetNode<Label>("%NoteUID");

        SaveToJson();
    }


    // CUSTOM FUNCTIONS
    public void SaveToJson()
    {
        string jsonPath = "res://modules/notes_json/data/notes_data.json";


        Godot.Collections.Array<Godot.Collections.Dictionary<string, string>> noteData = 
            new Godot.Collections.Array<Godot.Collections.Dictionary<string, string>>();

        noteData.Add(new Godot.Collections.Dictionary<string, string>());
        noteData[0].Add("note_id", "001");
        noteData[0].Add("note_title", "note_title_01");
        noteData[0].Add("note_content", "note_content_01");


        string noteDataToString = JsonSerializer.Serialize(noteData);
        GD.Print(noteDataToString.GetType());

        if(!FileAccess.FileExists(jsonPath))
        {
            FileAccess fileObj = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Write);
            fileObj.StoreLine(noteDataToString);
            GD.Print("File doesn't exist, creating one...");
        }
        else
        {
            // Read existing Json
            FileAccess fileObj = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
            string fileObjStringify = fileObj.GetLine();
            Variant parsedFileObjStringify = Json.ParseString(fileObjStringify);

            Godot.Collections.Array<Godot.Collections.Dictionary<string, string>> parsedFileObjStringifyToArray = 
                parsedFileObjStringify.AsGodotArray<Godot.Collections.Dictionary<string, string>>();

            GD.Print("File exist...");
            GD.Print(fileObjStringify.GetType());
            GD.Print(parsedFileObjStringifyToArray.Count);
        }
    }


    public string GenerateUID()
    {
        // Generate uid like: "note00001, note00002, etc"
        // Make sure there's no duplicate from previous created notes
        string jsonPath = "res://modules/notes_json/data/notes_data.json";
        string noteUID = "note";

        if(!FileAccess.FileExists(jsonPath))
        {
            noteUID = noteUID + 1;
        }
        else
        {
            FileAccess fileObj = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
            string fileObjStringify = fileObj.GetLine();
            Variant parsedFileObjStringify = Json.ParseString(fileObjStringify);
            
            Godot.Collections.Array<Godot.Collections.Dictionary<string, string>> parsedFileObjToArray =
                parsedFileObjStringify.AsGodotArray<Godot.Collections.Dictionary<string, string>>();

            int numOfArray = parsedFileObjToArray.Count;
            noteUID = noteUID + (numOfArray + 1);
        }
        return noteUID;
    } 


    // SIGNAL AREA
    public void _OnSaveButtonPressed() 
    { 
        SaveToJson(); 
    } 
}
