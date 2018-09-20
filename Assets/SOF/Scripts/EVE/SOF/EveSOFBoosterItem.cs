using System;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Stores data which is used by SOF to add boosters to a spawned ship.
    /// </summary>
    [System.Serializable]
    public struct EveSOFBoosterItem
    {
        public Matrix4x4 locationMatrix;
    }
}
