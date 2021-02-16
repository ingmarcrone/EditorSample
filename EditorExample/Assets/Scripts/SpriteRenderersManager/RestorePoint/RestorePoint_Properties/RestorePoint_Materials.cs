using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestorePoint_Materials : RestorePoint_Property_Base
{
    public bool IsAlreadyStored { get; private set; } = false;

    [SerializeField]


#if UNITY_EDITOR
    [ReadOnly]
#endif
    private string _stored;
    public string Stored
    {
        get => _stored;
        private set => _stored = value;
    }

    public override void Store()
    {
        if (!IsAlreadyStored)
        {
            Stored = transform.GetComponent<SpriteRenderer>().sharedMaterial.name;
            IsAlreadyStored = true;
        }
    }

    public override bool IsAangepast() => transform.GetComponent<SpriteRenderer>().sharedMaterial.name != Stored;

    public override void Restore()
    {
        Material[] beschikbareMaterials = Resources.LoadAll<Material>("Materials");
        Material material = beschikbareMaterials.FirstOrDefault(x => x.name == Stored);
        if (material != null)
            transform.GetComponent<SpriteRenderer>().material = material;
    }

}
