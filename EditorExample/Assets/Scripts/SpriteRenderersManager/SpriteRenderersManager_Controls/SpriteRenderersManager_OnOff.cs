using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpriteRenderersManager_OnOff : SpriteRenderersManager_Property_Base
{
    public void TurnOnOff(bool isOn)
    {
        base.IsIncluissiefInActieve = true;
        foreach (SpriteRenderer spriteRenderer in SpriteRenderers())
            spriteRenderer.gameObject.SetActive(isOn);

    }

}
