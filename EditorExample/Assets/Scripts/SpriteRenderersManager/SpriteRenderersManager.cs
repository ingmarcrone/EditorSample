using System.Collections.Generic;
using System;

public class SpriteRenderersManager : SpriteRenderersManager_Base
{
    public Dictionary<Type, SpriteRenderersManager_Property_Base> Properties { get; private set; } = new Dictionary<Type, SpriteRenderersManager_Property_Base>();
    public bool PropertyExists(Type type) => Properties.ContainsKey(type);

    public void KeepChanges(Type type)
    {
        ToggleOfBefore_Restore_Or_KeepChanges(type);
        RestorePointManager.RemoveRestorePoints_OfType_FromAllSpriteRenderers(type, true);
    }

    public void Restore(Type type)
    {
        ToggleOfBefore_Restore_Or_KeepChanges(type);
        RestorePointManager.Restore_RestorePoints_OfType_ToAllSpriteRenderers(type);
    }

    private void ToggleOfBefore_Restore_Or_KeepChanges(Type type)
    {
        if (Properties.Count > 0 && Properties.ContainsKey(type))
            TogglePropertyOnOf(type);
    }

    public int GetReportForType(Type type)
    {
        RestorePointManagerRequired();
        return RestorePointManager.GetReportForType(type);
    }

    public void TogglePropertyOnOf(Type type)
    {
        RestorePointManagerRequired();

        if (PropertyExists(type))
        {
            RemoveProperty(type);
            RestorePointManager.RemoveRestorePoints_OfType_FromAllSpriteRenderers(type);
        }
        else
        {
            AddProperty(type);
            RestorePointManager.AddRestorePoints_OfType_ToAllSpriteRenderers(type);
        }
    }

    private void InstantiateProperties()
    {
        Properties = new Dictionary<Type, SpriteRenderersManager_Property_Base>();

        foreach (SpriteRenderersManager_Property_Base property in transform.GetComponents<SpriteRenderersManager_Property_Base>())
            if (!PropertyExists(property.GetType()))
                Properties.Add(property.GetType(), property);
    }

    private void AddProperty(Type type)
    {
        DestroyAllProperties();
        if (!PropertyExists(type))
            Properties.Add(type, gameObject.AddComponent(type) as SpriteRenderersManager_Property_Base);
    }

    private void RemoveProperty(Type type)
    {
        if (PropertyExists(type))
            DestroyImmediate(Properties[type]);
        InstantiateProperties();
    }

    private void DestroyAllProperties()
    {
        SpriteRenderersManager_Property_Base properties = gameObject.GetComponent<SpriteRenderersManager_Property_Base>();

        while (properties != null)
        {
            DestroyImmediate(gameObject.GetComponent<SpriteRenderersManager_Property_Base>());
            properties = gameObject.GetComponent<SpriteRenderersManager_Property_Base>();
        }

        InstantiateProperties();
    }

}
