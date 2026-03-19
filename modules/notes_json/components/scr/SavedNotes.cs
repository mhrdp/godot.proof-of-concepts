using Godot;
using System;

[GlobalClass]
public partial class SavedNotes : Resource
{
    [Export] public string NoteUid {get; set;}
    [Export] public string NoteTitle {get; set;}
    [Export] public string NoteContent {get; set;}
    [Export] public string NoteDatetime {get; set;}

    // Parameterless constructor was needed in C# to allow
    // edit in Godot's inspector
    public SavedNotes(): this(null, null, null, null) {}

    public SavedNotes(string noteUid, string noteTitle, string noteContent, string noteDatetime)
    {
        NoteUid = noteUid;
        NoteTitle = noteTitle;
        NoteContent = noteContent;
        NoteDatetime = noteDatetime;
    }

}
