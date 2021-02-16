using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TypeHelper
{

    public static Type FindMatchingType(Type type)
    {
        if (type == typeof(SpriteRenderersManager_Position))
            return typeof(RestorePoint_Position);
        else if (type == typeof(SpriteRenderersManager_Rotation))
            return typeof(RestorePoint_Rotation);
        else if (type == typeof(SpriteRenderersManager_OnOff))
            return typeof(RestorePoint_OnOff);
        else if (type == typeof(SpriteRenderersManager_Scale))
            return typeof(RestorePoint_Scale);
        else if (type == typeof(SpriteRenderersManager_Materials))
            return typeof(RestorePoint_Materials);
        else if (type == typeof(SpriteRenderersManager_SortingOrder))
            return typeof(RestorePoint_SortingOrder);
        else if (type == typeof(SpriteRenderersManager_Property_Base))
            return typeof(RestorePoint_Property_Base);

        if (type == typeof(RestorePoint_Position))
            return typeof(SpriteRenderersManager_Position);
        else if (type == typeof(RestorePoint_Rotation))
            return typeof(SpriteRenderersManager_Rotation);
        else if (type == typeof(RestorePoint_OnOff))
            return typeof(SpriteRenderersManager_OnOff);
        else if (type == typeof(RestorePoint_Scale))
            return typeof(SpriteRenderersManager_Scale);
        else if (type == typeof(RestorePoint_Materials))
            return typeof(SpriteRenderersManager_Materials);
        else if (type == typeof(RestorePoint_SortingOrder))
            return typeof(SpriteRenderersManager_SortingOrder);
        else if (type == typeof(RestorePoint_Property_Base))
            return typeof(SpriteRenderersManager_Property_Base);

        return type;
    }
}