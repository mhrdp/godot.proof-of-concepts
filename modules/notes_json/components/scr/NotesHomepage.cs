using Godot;
using System;

public partial class NotesHomepage : Control
{
    // INIT POPUP NODE
    private Popup newNotePopup;
    private Button popupCancelButton;
    private Button popupConfirmButton;
    private LineEdit popupNewNoteTitle;

    // INIT NOTE CONTAINER NODE
    private MarginContainer noteContentMargin;
    private Button newNoteButton;
    private Label notePlaceholder;

    private string newNoteTitle = "";
    private int noteUid = 0;

    [Export] public Resource noteContentResource;

    public override void _Ready()
    {
        newNotePopup = GetNode<Popup>("%NewNotePopup");
        newNoteButton = GetNode<Button>("%NotesAddButton");
        popupConfirmButton = GetNode<Button>("%ConfirmButton");
        popupCancelButton = GetNode<Button>("%CancelButton");
        popupNewNoteTitle = GetNode<LineEdit>("%PopupNewNoteTitle");
        noteContentMargin = GetNode<MarginContainer>("%NoteContentMargin");
        notePlaceholder = GetNode<Label>("%NotePlaceholder");


        newNotePopup.Visible = false;
        noteContentMargin.Visible = false;
        notePlaceholder.Visible = true;
    }

    public override void _Process(double delta)
    {
    }


    // *** CUSTUM FUNCTIONS ***//
    // NEW NOTE FUNCTIONS //


    // *** SIGNAL *** //
    private void _OnNotesAddButtonPressed()
    {
        newNotePopup.Visible = true;
        newNoteButton.Disabled = true;
    }

    private void _OnCancelButtonPressed()
    {
        newNoteButton.Disabled = false;
        newNotePopup.Visible = false;
        notePlaceholder.Visible = true;
    }

    private void _OnConfirmButtonPressed()
    {
        newNoteButton.Disabled = false;
        newNotePopup.Visible = false;

        newNoteTitle = popupNewNoteTitle.Text;
        notePlaceholder.Visible = false;
        noteContentMargin.Visible = true;

        string noteSaveDir = "res://modules/notes_json/components/res/notes/";

        if (Godot.DirAccess.DirExistsAbsolute(noteSaveDir)==false)
        {
            Godot.DirAccess.MakeDirRecursiveAbsolute(noteSaveDir);
        }

        if (Godot.DirAccess.DirExistsAbsolute(noteSaveDir)==true) 
        {
            string[] files = Godot.DirAccess.GetFilesAt(noteSaveDir);
            int numOfFiles = files.Length;
            switch (numOfFiles)
            {
                case 0:
                    noteUid = 1;
                    break;

                case >=1:
                    noteUid = numOfFiles + 1;
                    break;
            }
        }

        SavedNotes noteData = new SavedNotes();
        noteData.NoteUid = noteUid;
        noteData.NoteContent = null;
        noteData.NoteTitle = newNoteTitle;
        noteData.NoteDatetime = Godot.Time.GetDatetimeStringFromSystem();
        Error saveStatus = Godot.ResourceSaver.Save(noteData, noteSaveDir);
        if (saveStatus==Error.Ok)
        {
            GD.Print("Saved!");
        }
    }
}
