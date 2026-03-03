using Godot;
using System;

public partial class Notepad : Control
{
    private Button AddButton;
    private TextEdit Note;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        AddButton = GetNode<Button>("%AddButton");
        Note = GetNode<TextEdit>("%Notepad");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (AddButton.ButtonPressed == true)
        {
            Note.Visible = true;
        }
	}
}
