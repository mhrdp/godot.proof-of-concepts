using Godot;
using System;

public partial class ChoicesBox : Control
{
    private Label labelText;
    private ColorRect labelBox;

    public void AddChoices(string choice)
    {
        labelText = GetNode<Label>("%ChoiceText");
        labelText.Text = choice;
    }

    public void AddGap()
    {
        labelBox = GetNode<ColorRect>("%ChoicesContainer");
        float height = labelBox.Size.Y;
        labelBox.Position = new Vector2(0, (int)height + 15);

    }
}
