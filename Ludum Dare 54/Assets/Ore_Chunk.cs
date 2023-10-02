using UnityEngine;

public class OreChunk : MonoBehaviour
{
    public OreType oreType;

    void Start()
    {
        if (oreType != null)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = oreType.oreSprite;
                // spriteRenderer.color = oreType.oreColor;
            }
            else
            {
                Debug.LogError("No SpriteRenderer attached to the GameObject.", this);
            }
        }
    }
}
