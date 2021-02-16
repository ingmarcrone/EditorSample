using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderersManager_Rotation : SpriteRenderersManager_Property_Base
{
    public void SetExactRotation(float rotationZ)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            spriteRenderer.transform.eulerAngles = new Vector3(spriteRenderer.transform.eulerAngles.x, spriteRenderer.transform.eulerAngles.y, rotationZ);
    }

    public void SetRamdomRotation(float van, float tot)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            spriteRenderer.transform.eulerAngles = new Vector3(spriteRenderer.transform.eulerAngles.x, spriteRenderer.transform.eulerAngles.y, Random.Range(van, tot));
    }

}
