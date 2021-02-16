using UnityEngine;

public class RestorePoint_OnOff : RestorePoint_Property_Base, IRestorePoint_Property
{
    public bool IsAlreadyStored { get; private set; } = false;

    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    private bool _stored;
    public bool Stored
    {
        get => _stored;
        private set => _stored = value;
    }

    public override void Store()
    {
        if (!IsAlreadyStored)
        {
            Stored = transform.gameObject.activeSelf;
            IsAlreadyStored = true;
        }
    }

    public override bool IsAangepast() => transform.gameObject.activeSelf != Stored;

    public override void Restore() => transform.gameObject.SetActive(Stored);

}

