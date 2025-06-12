using GDGeneric = Godot;
using GDCollections = Godot.Collections;
using CSSys = System;
using CSSysIO = System.IO;
using CSJson = System.Text.Json;
using CSCollections = System.Collections.Generic;

public partial class NoteWritingJson : GDGeneric.VBoxContainer
{
    private GDGeneric.Button saveButton;
    private GDGeneric.Button closeButton;
    private GDGeneric.TextEdit noteArea;
    private GDGeneric.Label noteTitle;
    private GDGeneric.Label noteUID;

    private bool edit;

    public override void _Ready()
    {
        saveButton = GetNode<GDGeneric.Button>("%SaveButton");
        closeButton = GetNode<GDGeneric.Button>("%CloseButton");
        noteArea = GetNode<GDGeneric.TextEdit>("%NoteWriting");
        noteTitle = GetNode<GDGeneric.Label>("%NoteTitle");

        edit = false;

        SaveToJson();
    }


    // CUSTOM FUNCTIONS
    public class Note
    {
        public string noteId { get; set; }
        public GDCollections.Dictionary<string, string> noteContent { get; set; }

        public GDCollections.Dictionary<string, GDGeneric.Variant> ToVariantDictionary()
        {
            var dict = new GDCollections.Dictionary<string, GDGeneric.Variant>
            {
                {"note_id", noteId},
                {"note_content", noteContent},
            };
            return dict;
        }
    }

    public void SaveToJson()
    {
        string jsonPath = "modules/notes_json/data/notes_data.json";

        Note notes = new Note();
        GDCollections.Array<GDCollections.Dictionary<string, GDGeneric.Variant>> noteData =
            new GDCollections.Array<GDCollections.Dictionary<string, GDGeneric.Variant>>();
        GDCollections.Dictionary<string, string> data = new GDCollections.Dictionary<string, string>();

        data.Add("note_title", "How to strike it rich");
        data.Add("note_desc", "Legit 100%!!");
        data.Add("note_content", "Who knows?");

        notes.noteId = "0002";
        notes.noteContent = data;

        noteData.Add(notes.ToVariantDictionary());

        string jsonString2 = GDGeneric.Json.Stringify(noteData);

        if (!GDGeneric.FileAccess.FileExists(jsonPath))
        {
            edit = false;
            GDGeneric.FileAccess createNoteFile = GDGeneric.FileAccess.Open(jsonPath, GDGeneric.FileAccess.ModeFlags.Write);
            createNoteFile.StoreLine(jsonString2);
        }
        else
        {
            GDGeneric.Json noteFile = new GDGeneric.Json();
            GDGeneric.FileAccess readNoteFile = GDGeneric.FileAccess.Open(jsonPath, GDGeneric.FileAccess.ModeFlags.Read);
            GDGeneric.Error parsedNoteFile = noteFile.Parse(readNoteFile.GetAsText());

            readNoteFile.Close();

        }
    }



    // SIGNAL AREA
    public void _OnSaveButtonPressed()
    {
        SaveToJson();
    }
}
