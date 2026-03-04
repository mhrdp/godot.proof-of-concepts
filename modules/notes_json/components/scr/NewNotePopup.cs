using Godot;
using System;

public partial class NewNotePopup : Popup
{
    private Button cancelButton;
    private Button confirmButton;


    private void _OnCancelButtonPressed()
    {
        this.Visible = false;
    }

    private void _OnConfirmButtonPressed()
    {
        this.Visible = false;
    }
}
