# MY GODOT LAB
Bunch of experiment codes I created while playing with [Godot Game Engine](https://github.com/godotengine/godot/)

This could also be a proof-of-concepts.

Each modules were a separate "game", and is standalone. In a case where any of these module(s) need the functions of the other module(s), instead of try referring to that module(s), we will instead make a new one over at the module(s) that need those functions. In other words, there's no such thing as reusable code for a different modules. 

# Notes To Myself
## Using Neovim as an external editor in Godot 4.x
*HUGE CAVEAT*
You can open the Godot file directly from Godot Editor, BUT everytime you open a
new file from there, Godot will create a new instance of Neovim instead of
opening it in a new tab from within the same instance. There are workarounds to
make it opened within the same instance, but applying it kinda too much of a
pain so I stopped.

This way is simpler but will add an extra layer in your workflow: essentially,
Neovim will act solely as code editor, whenever you want to change something
that is not the code (e.g renaming file name, deleting a file, etc), you need to do it from within Godot
editor, and whenever you want to open a new file after the first one that you
opened from Godot editor, you will need to open it from Neovim because otherwise
Godot will create a new instance of Neovim as explained above.

### For Windows
1. Set `External Editor` setting (for `GDScript` and/or `Mono`) on. You should
   see an `Exec Path` and an `Exec Flags` settings opened up. In case you don't
   toggle the `Advance Settings` at the upper right.
2. Set `Exec Path` to:
	* `wt` if you're using Windows Terminal
	* `powershell` if you're using Windows PowerShell
	* In other words, set the `Exec Path` to call the terminal
3. Set `Exec Flags` to:
	* `nvim +{line} {file}` for Windows Terminal
	* `-NoExit -Command "nvim +{line} '{file}'"` for Windows PowerShell

### Linux
#### By making `.sh` script and set it as editor path
1. Create a Bash (`.sh`) script to tell Godot to run the terminal
2. Set `Exec Flags` accordingly

#### The same way as Windows
TBA...

### Other Examples
#### Alacritty
`Exec Path` to `alacritty`; `Exec Flags` to `-e nvim +{line} {file}`.
> This will open the file at the correct line but not column! Neovim doesn't
> support column positioning out of the box via CLI

> If `+{line}` doesn't work, try escaping it: `-e nvim +"normal {line}G" {file}`

#### Kitty
`Exec Path` to `kitty`; `Exec Flags` to `nvim +{line} {file}`.

## C# / GDScript
Prerequisite:
1. gdtoolkit
2. omnisharp

> I installed both of them with mason.nvim from lazy.nvim's Neovim packages.

Run:
```
:Mason
```
to see the list of available languages.

Then Run:
```
:MasonInstall <package_name>
```
to install the package(s).

> IMPORTANT: Don't forget to build the C# project first from within Godot
> editor.
