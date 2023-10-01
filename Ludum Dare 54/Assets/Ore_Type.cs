using UnityEngine;

[CreateAssetMenu(fileName = "New Ore Type", menuName = "Ore Type")]
public class OreType : ScriptableObject
{
    public string oreName;
    public Sprite oreSprite;
    public Color oreColor;

    // Add other properties for ore chunks here
}
