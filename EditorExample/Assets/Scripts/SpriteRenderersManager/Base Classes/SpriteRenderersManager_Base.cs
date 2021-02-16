using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderersManager_Base : MonoBehaviour
{
    protected SpriteRenderer[] GetSpriteRenderers(bool inclussiefInactieve = true)
    {
        if (inclussiefInactieve)
        {
            Component[] componenents = transform.GetComponentsInChildren(typeof(SpriteRenderer), true);
            SpriteRenderer[] spriteRenderers = new SpriteRenderer[componenents.Length];
            for (int i = 0; i < componenents.Length; i++)
                spriteRenderers[i] = (SpriteRenderer)componenents[i];
            return spriteRenderers;
        }

        return transform.GetComponentsInChildren<SpriteRenderer>();
    }

    public RestorePointManager RestorePointManager;

    public void FillInitial()
    {
        RestorePointManagerRequired();

        RestorePointManager.Populate_RestorePoint_Properties_Initial();
    }

    public void UpdateChangesReport()
    {
        RestorePointManagerRequired();

        RestorePointManager.UpdateReport();
    }

    public void RestorePointManagerRequired()
    {
        if (RestorePointManager == null)
        {
            RestorePointManager = gameObject.GetComponent<RestorePointManager>();
            if (RestorePointManager == null)
                RestorePointManager = gameObject.AddComponent(typeof(RestorePointManager)) as RestorePointManager;

            RestorePointManager.hideFlags = UnityEngine.HideFlags.HideInInspector;

        }
    }


}
