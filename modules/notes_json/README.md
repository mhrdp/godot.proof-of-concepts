The notebook feature will have three main parts:
1. The basic window/interface
2. The place to write the notes
3. The list of saved notes


# Basic Window
The basic window will split into two section:
1. A section to list a saved notes
2. A section to write or edit the note

Each of the two section will call a different scene, and the note that was
written in section two will be saved into a `.json` file with format as follow:
```json
{
	{
		"note_id": "str, auto generated",
		"note_title": "str, no null",
		"note_content": "str, no null"
	},

	// ....cont
}
```

And the section one will read upon the `.json` file to display the saved notes,
with the title as the only part that will be displayed. When the writer about to
edit the note, the section two will attempt to fetch the note id that was
omitted by the first section, and then read the content based on that.

Read [Saving Game](https://docs.godotengine.org/en/stable/tutorials/io/saving_games.html) for reference.
