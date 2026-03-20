using Godot;
using System;

[GlobalClass]
public partial class SavedNotes : Resource
{
    [Export] public int NoteUid {get; set;}
    [Export] public string NoteTitle {get; set;}
    [Export] public string NoteContent {get; set;}
    [Export] public string NoteDatetime {get; set;}

    // Parameterless constructor was needed in C# to allow
    // edit in Godot's inspector
    public SavedNotes(): this(0, null, null, null) {}

    public SavedNotes(int noteUid, string noteTitle, string noteContent, string noteDatetime)
    {
        NoteUid = noteUid;
        NoteTitle = noteTitle;
        NoteContent = noteContent;
        NoteDatetime = noteDatetime;
    }

}
