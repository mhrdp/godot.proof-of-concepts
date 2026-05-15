using Godot;
using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public partial class DialogueBox : ColorRect
{
	private string csvPath = "./modules/dialogue/game_dialogue_sheets.csv";

	private Label nameLabel;
	private RichTextLabel dialogueLabel;

	private List<string> dialogueLog;
	private Dictionary<string, Dictionary<string, string>> sceneState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nameLabel = GetNode<Label>("NameLabel");
		dialogueLabel = GetNode<RichTextLabel>("DialogueLabel");

		dialogueLog = new List<string>();
		sceneState = new Dictionary<string, Dictionary<string, string>>();

		GD.Print(ReadCsv(csvPath).Rows[0].ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private DataTable ReadCsv(string filePath)
	{
		DataTable dt = new DataTable();
		string[] lines = File.ReadAllLines(filePath);
		if (lines.Length > 0)
		{
			string[] header = lines[0].Split(",");
			foreach (string h in header)
			{
				dt.Columns.Add(h);
			}

			for (int i=1; i<lines.Length; i++)
			{
				string[] data = lines[i].Split(',');
				dt.Rows.Add(data);
			}
		}

		return dt;
	}
}
