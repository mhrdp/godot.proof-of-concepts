# STATE
Save current characters' state by creating a dictionary with a structure more or less like following:
```
{
    <character_id | string>: {
        dialogueIdList: list[string],
        spriteIdList: list[string],
        characterPositionList: list[string],
    }
}
```
* `character_id` is the id of the character
* `dialogueIdList` is a list of string consist the dialogue's ids that had appeared in current scene, ordered from the first dialogue (index 0) to the final dialogue (index n).
* `spriteIdList` is a list of string consisted of the character's sprite id that had appeared in current scene, ordered from the first dialogue (index 0) to the last dialogue (index n).
* `characterPositionList` is a list of string of the positions the character had appeared in the current scene, ordered from the first dialogue (index 0) to the last dialogue (index n).
[!]: All of this list was saved for current scene for replay purposes, and will be cleared when the scene change — replaced by the new scene state data.


Save this scene dialogue into a nested list more or less like the following:
```
[[<dialogueId>, <characterName>, <dialogueLine>], ...]
```
This is for dialogue log.
