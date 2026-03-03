using Godot;
using System;

public partial class NotesHomepage : Control
{
    // INIT POPUP NODE
    public Popup newNotePopup;
    private Button newNotePopupCancelBtn;

    // INIT NOTE CONTAINER NODE
    public Button newNoteButton;


    public override void _Ready()
    {
        newNotePopup = GetNode<Popup>("%NewNotePopup");
        newNoteButton = GetNode<Button>("%NotesAddButton");
        newNotePopupCancelBtn = GetNode<Button>("%CancelButton");
    }

    public override void _Process(double delta)
    {
    }


    // *** SIGNAL *** //
    private void _OnNotesAddButtonToggled(bool toggled_on)
    {
        switch (toggled_on)
        {
            case true:
                newNotePopup.Visible = true;
                newNoteButton.Disabled = true;
                break;
            case false:
                newNoteButton.Disabled = false;
                break;
            default:
                newNoteButton.Disabled = false;
                break;
        }
    }
}
