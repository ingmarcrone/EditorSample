using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestorePoint_Property
{
    void Store();

    bool IsAangepast();

    void Restore();


}
