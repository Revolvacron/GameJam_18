using System;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Contains faction info which is used by SOF to apply faction variation to spawned ships.
    /// </summary>
    [System.Serializable]
    public struct EveSOFFaction
    {
        /// <summary>
        /// The name of the faction.
        /// </summary>
        public string name;
        /// <summary>
        /// The name of material 1.
        /// </summary>
        public string material1;
        /// <summary>
        /// The name of material 2.
        /// </summary>
        public string material2;
        /// <summary>
        /// The name of material 3.
        /// </summary>
        public string material3;
        /// <summary>
        /// The name of material 4.
        /// </summary>
        public string material4;
        /// <summary>
        /// The glow color to use.
        /// </summary>
        public Color glowColor;
    }
}
