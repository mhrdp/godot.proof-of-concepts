using Godot;
using System;

public partial class NotesHomepage : Control
{
    // INIT POPUP NODE
    public Popup newNotePopup;

    // INIT NOTE CONTAINER NODE
    public Button newNoteButton;


    public override void _Ready()
    {
        newNotePopup = GetNode<Popup>("%NewNotePopup");
        newNoteButton = GetNode<Button>("%NotesAddButton");
    }

    public override void _Process(double delta)
    {
        if (newNotePopup.Visible = false)
        {
            newNoteButton.Disabled = false;
        }
    }


    // *** SIGNAL *** //
    private void _OnNotesAddButtonPressed()
    {
        newNotePopup.Visible = true;
        newNoteButton.Disabled = true;
    }
}
