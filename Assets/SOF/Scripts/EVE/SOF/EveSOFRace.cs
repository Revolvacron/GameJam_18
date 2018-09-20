using System;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Defines race data to use when generating ships with SOF.
    /// </summary>
    [System.Serializable]
    public struct EveSOFRace
    {
        /// <summary>
        /// The name of the race.
        /// </summary>
        public string name;
        /// <summary>
        /// The color to use for boosters with this race.
        /// </summary>
        public Color boosterColor;
    }
}