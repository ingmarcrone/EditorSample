using UnityEngine;

public class RestorePoint_Scale : RestorePoint_Property_Base, IRestorePoint_Property
{
    public bool IsAlreadyStored { get; private set; } = false;

    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    private Vector3 _stored;
    public Vector3 Stored
    {
        get => _stored;
        private set => _stored = value;
    }

    public override void Store()
    {
        if (!IsAlreadyStored)
        {
            Stored = transform.localScale;
            IsAlreadyStored = true;
        }
    }

    public override bool IsAangepast() => Vector3.Distance(transform.localScale, Stored) != 0;

    public override void Restore() => transform.localScale = Stored;

}
