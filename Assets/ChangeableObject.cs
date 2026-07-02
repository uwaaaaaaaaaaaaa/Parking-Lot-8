
using UnityEngine;

public class ChangeableObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public void ChangeSprite(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            spriteRenderer.sprite = sprites[index];
        }
    }
    //異変なしの時はデフォルトの見た目にする
    public void ResetSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

}
