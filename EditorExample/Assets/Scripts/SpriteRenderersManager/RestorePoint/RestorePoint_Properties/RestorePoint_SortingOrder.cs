using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePoint_SortingOrder : RestorePoint_Property_Base, IRestorePoint_Property
{
    public bool IsAlreadyStored { get; private set; } = false;

    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    private int _stored;
    public int Stored
    {
        get => _stored;
        private set => _stored = value;
    }

    public override void Store()
    {
        if (!IsAlreadyStored)
        {
            Stored = SpriteRenderer.sortingOrder;
            IsAlreadyStored = true;
        }
    }

    public override bool IsAangepast() => SpriteRenderer.sortingOrder != Stored;

    public override void Restore() => SpriteRenderer.sortingOrder = Stored;

}
