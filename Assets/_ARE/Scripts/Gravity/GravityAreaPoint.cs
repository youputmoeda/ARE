using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class GravityAreaPoint : GravityArea
{
    [SerializeField] private Vector3 _center;


    public override Vector3 GetGravityDirection(GravityBody _gravityBody)
    {
        return (_center - _gravityBody.transform.position).normalized;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}