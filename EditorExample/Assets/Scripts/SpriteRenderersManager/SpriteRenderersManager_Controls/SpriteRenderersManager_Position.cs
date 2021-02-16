using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpriteRenderersManager_Position : SpriteRenderersManager_Property_Base
{
    public bool IsShowBox = false;
    public Vector3 BoxHoekA { get; set; } = new Vector3(0f, 5f, 0f);
    public Vector3 BoxHoekB { get; set; } = new Vector3(4f, 0f, 0f);

    public void PlaceInBox()
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.transform.position = new Vector3(
                Random.Range(BoxHoekA.x, BoxHoekB.x),
                Random.Range(BoxHoekA.y, BoxHoekB.y),
                Random.Range(BoxHoekA.z, BoxHoekB.z));
        }

    }

    public void HuidigPlusRandom(Vector2 van, Vector2 tot)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float x = Random.Range(van.x, tot.x);
            float y = Random.Range(van.y, tot.y);

            RestorePoint_Position restorePoint_Position = spriteRenderer.GetComponent<RestorePoint_Position>();
            x = x + restorePoint_Position.Stored.x;
            y = y + restorePoint_Position.Stored.y;

            spriteRenderer.transform.position = new Vector3(x, y, 1);

        }
    }


    public void HuidigPlusStretch(Vector2 van, Vector2 tot)
    {
        Vector2[] positieRange = new Vector2[] { van, tot };
        float erbijMeestLinks = positieRange.Min(x => x.x);
        float erbijMeestRechts = positieRange.Max(x => x.x);
        float erbijMeestOnder = positieRange.Min(x => x.y);
        float erbijMeestBoven = positieRange.Max(x => x.y);

        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        float xMostLeft = spriteRenderers.Min(x => x.transform.position.x);
        float xMostRight = spriteRenderers.Max(x => x.transform.position.x);
        float xMostDown = spriteRenderers.Min(x => x.transform.position.y);
        float xMostUp = spriteRenderers.Max(x => x.transform.position.y);

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float procentueleLocatie_Horizontaal = MathS.ProcentueleLocatie_VoorFloats(xMostLeft, xMostRight, spriteRenderer.transform.position.x);
            float plusX = MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(erbijMeestLinks, erbijMeestRechts, procentueleLocatie_Horizontaal);

            float procentueleLocatie_Verticaal = MathS.ProcentueleLocatie_VoorFloats(xMostDown, xMostUp, spriteRenderer.transform.position.y);
            float plusY = MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(erbijMeestOnder, erbijMeestBoven, procentueleLocatie_Verticaal);

            RestorePoint_Position restorePoint_Position = spriteRenderer.GetComponent<RestorePoint_Position>();

            spriteRenderer.transform.position = new Vector3(
                restorePoint_Position.Stored.x + plusX,
                restorePoint_Position.Stored.y + plusY,
                restorePoint_Position.Stored.z);


        }

        MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(0, 0, 0);
        MathS.ProcentueleLocatie_VoorFloats(0, 0, 0);

        float yLowest = spriteRenderers.Min(x => x.transform.position.y);
        float yHighest = spriteRenderers.Max(x => x.transform.position.y);


    }

    public void HuidigPlusTrap(Vector2 van, Vector2 tot)
    {
        SpriteRenderer[] spriteRenderers = SpriteRenderers();
        float xMostLeft = spriteRenderers.Min(x => x.transform.position.x);
        float xMostRight = spriteRenderers.Max(x => x.transform.position.x);
        float yLowest = spriteRenderers.Min(x => x.transform.position.y);
        float yHighest = spriteRenderers.Max(x => x.transform.position.y);

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float procentueleLocatieX = MathS.ProcentueleLocatie_VoorFloats(xMostLeft, xMostRight, spriteRenderer.transform.position.x);
            float x = MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(van.x, tot.x, procentueleLocatieX);

            float procentueleLocatieY = MathS.ProcentueleLocatie_VoorFloats(yLowest, yHighest, spriteRenderer.transform.position.y);
            float y = MathS.Getal_OpBasisVan_ProcentueleLocatie_VoorFloats(van.y, tot.y, procentueleLocatieX);

            RestorePoint_Position restorePoint_Position = spriteRenderer.GetComponent<RestorePoint_Position>();
            x = x + restorePoint_Position.Stored.x;
            y = y + restorePoint_Position.Stored.y;

            float z = 1;

            spriteRenderer.transform.position = new Vector3(x, y, z);
        }
    }



#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (IsShowBox)
            DrawBox();

    }

    private void DrawBox()
    {
        Vector3 boxHoekA = BoxHoekA + transform.position;
        Vector3 boxHoekB = BoxHoekB + transform.position;
        Vector3 boxHoekAB = new Vector3(BoxHoekA.x, BoxHoekB.y, 0) + transform.position;
        Vector3 boxHoekBA = new Vector3(BoxHoekB.x, BoxHoekA.y, 0) + transform.position;

        Handles.color = Color.black;
        Handles.DrawLine(boxHoekA, boxHoekAB);
        Handles.DrawLine(boxHoekA, boxHoekBA);
        Handles.DrawLine(boxHoekB, boxHoekAB);
        Handles.DrawLine(boxHoekB, boxHoekBA);

        Handles.color = Color.white;
        float dotted = 2.5f;
        Handles.DrawDottedLine(boxHoekA, boxHoekAB, dotted);
        Handles.DrawDottedLine(boxHoekA, boxHoekBA, dotted);
        Handles.DrawDottedLine(boxHoekB, boxHoekAB, dotted);
        Handles.DrawDottedLine(boxHoekB, boxHoekBA, dotted);

        float dotZise = 0.25f;
        Handles.SphereHandleCap(0, boxHoekAB, Quaternion.LookRotation(Vector3.up), dotZise, EventType.Repaint);
        Handles.SphereHandleCap(0, boxHoekBA, Quaternion.LookRotation(Vector3.up), dotZise, EventType.Repaint);

        Handles.color = Color.red;
        Handles.SphereHandleCap(0, boxHoekA, Quaternion.LookRotation(Vector3.up), dotZise, EventType.Repaint);

        Handles.color = Color.green;
        Handles.SphereHandleCap(0, boxHoekB, Quaternion.LookRotation(Vector3.up), dotZise, EventType.Repaint);

    }

#endif
}
