using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpriteRenderersManager_SortingOrder : SpriteRenderersManager_Property_Base
{
    [SerializeField]
    private int Number = 0;

    public void SetOrderLayer()
    {
        foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
            spriteRenderer.sortingOrder = Number;
    }

    public void AddToOrderLayer()
    {
        foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder + Number;
    }

    public void SubtractFromOrderLayer()
    {
        foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder - Number;
    }
}
