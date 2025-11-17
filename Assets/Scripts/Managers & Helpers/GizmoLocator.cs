using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GizmoLocator
{
    private static GizmoManager _gizmo;

    public static GizmoManager GizmoManager
    {
        get
        {
            if (_gizmo == null)
            {
                _gizmo = GameObject.FindFirstObjectByType<GizmoManager>();
                if (_gizmo == null)
                    Debug.LogWarning("GizmoManager: gizmo not found");
            }

            return _gizmo;
        }
    }

    public static void ClearCache() => _gizmo = null;
}
