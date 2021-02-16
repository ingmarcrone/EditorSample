using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteRenderersManager_Scale : SpriteRenderersManager_Property_Base
{
    public void SetExactScale(Vector2 exactScale)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            spriteRenderer.transform.localScale = new Vector3(exactScale.x, exactScale.y, 1);

    }

    public void SetRandomScale(Vector2 van, Vector2 tot, bool isPlusHuidige)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float x = Random.Range(van.x, tot.x);
            float y = Random.Range(van.y, tot.y);

            if (isPlusHuidige)
            {
                RestorePoint_Scale restorePoint_Scale = spriteRenderer.GetComponent<RestorePoint_Scale>();
                x = x + restorePoint_Scale.Stored.x;
                y = y + restorePoint_Scale.Stored.y;
            }

            spriteRenderer.transform.localScale = new Vector3(x, y, 1);

        }
    }

    public void SetOplopendeScaleHorizontaal(Vector2 van, Vector2 tot, bool isPlusHuidige)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        float xLeft = spriteRenderers.Min(x => x.transform.position.x);
        float xRight = spriteRenderers.Max(x => x.transform.position.x);

        float yLeft = spriteRenderers.Min(x => x.transform.position.y);
        float yRight = spriteRenderers.Max(x => x.transform.position.y);

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float procentueleLocatieX = MathS.ProcentueleLocatie_VoorFloats(xLeft, xRight, spriteRenderer.transform.position.x);
            float x = MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(van.x, tot.x, procentueleLocatieX);

            float procentueleLocatieY = MathS.ProcentueleLocatie_VoorFloats(yLeft, yRight, spriteRenderer.transform.position.y);
            float y = MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(van.y, tot.y, procentueleLocatieX);

            if (isPlusHuidige)
            {
                RestorePoint_Scale restorePoint_Scale = spriteRenderer.GetComponent<RestorePoint_Scale>();
                x = x + restorePoint_Scale.Stored.x;
                y = y + restorePoint_Scale.Stored.y;
            }

            float z = 1;

            spriteRenderer.transform.localScale = new Vector3(x, y, z);
        }

    }

}
