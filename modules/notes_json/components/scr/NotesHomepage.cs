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
    private Button newNoteButton;


    public override void _Ready()
    {
        newNotePopup = GetNode<Popup>("%NewNotePopup");
        newNoteButton = GetNode<Button>("%NotesAddButton");
        popupConfirmButton = GetNode<Button>("%ConfirmButton");
        popupCancelButton = GetNode<Button>("%CancelButton");
        popupNewNoteTitle = GetNode<LineEdit>("%PopupNewNoteTitle");
    }

    public override void _Process(double delta)
    {
    }


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
    }

    private void _OnConfirmButtonPressed()
    {
        newNoteButton.Disabled = false;
        newNotePopup.Visible = false;
    }
}
