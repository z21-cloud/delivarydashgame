using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

public static class PlayerLocator
{
    private static GameObject _player;

    public static GameObject Player
    {
        get
        {
            if(_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
                if (_player == null)
                    Debug.LogWarning("PlayerLocator: player not found");
            }

            return _player;
        }
    }

    public static void ClearCache() => _player = null;
}
