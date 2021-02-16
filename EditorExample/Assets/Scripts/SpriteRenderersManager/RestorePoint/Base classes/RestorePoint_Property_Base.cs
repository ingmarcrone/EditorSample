using System;
using UnityEngine;

public class RestorePoint_Property_Base : MonoBehaviour, IRestorePoint_Property
{
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }

    public virtual void Store() => throw new NotImplementedException();

    public virtual bool IsAangepast() => throw new NotImplementedException();

    public virtual void Restore() => throw new NotImplementedException();

}
