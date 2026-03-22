using Godot;
using System.Collections.Generic;
using System.Text.Json;

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
    private string noteSaveDir = "res://modules/notes_json/components/res/notes/";
    private string jsonPath = "res://modules/notes_json/components/res/notes/note_data_json.json";
    private List<NoteValuesType> jsonList = new ();

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

        if (Godot.DirAccess.DirExistsAbsolute(noteSaveDir)==false)
        {
            Godot.DirAccess.MakeDirRecursiveAbsolute(noteSaveDir);
        }

        if (Godot.FileAccess.FileExists(jsonPath)==false)
        {
            var file = Godot.FileAccess.Open(jsonPath, FileAccess.ModeFlags.Write);

            if (file!=null)
            {
                file.StoreString("[{}]");
                file.Close();
            }
        }
    }

    public override void _Process(double delta)
    {
        if (popupNewNoteTitle.Text=="")
        {
            popupConfirmButton.Disabled = true;
        }
        if (popupNewNoteTitle.Text!="")
        {
            popupConfirmButton.Disabled = false;
        }
    }


    // *** CUSTUM FUNCTIONS ***//
    // NEW NOTE FUNCTIONS //
    private class NoteValuesType
    {
        public int NoteUid {get; set;}
        public string NoteTitle {get; set;}
        public string NoteContent {get; set;}
        public string NoteDatetime {get; set;}
        public bool NoteDeleted {get; set;}
    }


    // *** SIGNAL *** //
    private void _OnNotesAddButtonPressed()
    {
        // Button behavior
        newNotePopup.Visible = true;
        newNoteButton.Disabled = true;

        // Deserialize JSON containing note data
        using var jsonFile = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        string jsonString = jsonFile.GetAsText();
        Json json = new Json();
        Error error = json.Parse(jsonString);
        if (error==Error.Ok)
        {
            jsonList = JsonSerializer.Deserialize<List< NoteValuesType>>(jsonString);
        }
        else
        {
            GD.PrintErr($"JSON parse error: {json.GetErrorMessage()} at line {json.GetErrorLine()}")
        }
    }

    private void _OnCancelButtonPressed()
    {
        // Button behavior
        newNoteButton.Disabled = false;
        newNotePopup.Visible = false;
        notePlaceholder.Visible = true;
    }

    private void _OnConfirmButtonPressed()
    {
        // Button behavior
        newNoteButton.Disabled = false;
        newNotePopup.Visible = false;

        newNoteTitle = popupNewNoteTitle.Text;
        notePlaceholder.Visible = false;
        noteContentMargin.Visible = true;

        // Get Uid for note
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

        // Create Note's custom resource binary data (*.res)
        string savedFile = $"{noteSaveDir}note_{noteUid}.res";
        SavedNotes noteData = new SavedNotes();

        noteData.NoteUid = noteUid;
        noteData.NoteContent = "";
        noteData.NoteTitle = newNoteTitle;
        noteData.NoteDatetime = Godot.Time.GetDatetimeStringFromSystem(); 
        noteData.NoteDeleted = false;

        Error saveStatus = Godot.ResourceSaver.Save(noteData, savedFile);
        if (saveStatus==Error.Ok)
        {
            GD.Print("Saved!");
        }

        // Note's data as Json's list
        jsonList.Add(
               new NoteValuesType
               {
                NoteUid = noteUid,
                NoteTitle = newNoteTitle,
                NoteContent = "",
                NoteDatetime = Godot.Time.GetDatetimeStringFromSystem(),
                NoteDeleted = false
               }
        );
        var options = new JsonSerializerOptions{ WriteIndented=true};
        string noteDataJson = JsonSerializer.Serialize(jsonList, options);
    }
}
