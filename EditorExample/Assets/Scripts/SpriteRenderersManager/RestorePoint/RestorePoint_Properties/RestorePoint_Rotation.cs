using UnityEngine;

public class RestorePoint_Rotation : RestorePoint_Property_Base, IRestorePoint_Property
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
            Stored = transform.localEulerAngles;
            IsAlreadyStored = true;
        }
    }

    public override bool IsAangepast() => Vector3.Distance(transform.localEulerAngles, Stored) != 0;

    public override void Restore() => transform.localEulerAngles = Stored;

}
