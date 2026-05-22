# SYSTEM
The dialogue system was structured as a list of dictionaries with `string` as its `key` type and `object` as its `value` type: `List<Dictionary<"string", "object">>`. The reason by we use `object` as `value` type was because there's a high chance that the `value` type will vary for each `key` that a dynamic type interpretation was needed to make it work.

One file, or one CSV will represent EXACTLY one scene. For different scene new file has to be created.

The CSV will control everything that is involved within the dialogue system, including but not limited to:
1. character's name
2. character's dialogues
3. character's expression/sprite type
4. branching dialogue, including its "jumping to" instruction
5. etc.

For branching dialogue, the CSV will represent it as a single string separated by `|`: `"choice 01" | "choice 02"`, the game will later then split the string with `|` as its splitting point and therefore creating a `List` format before presenting it as a choice dialogue.
