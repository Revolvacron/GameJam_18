using System;
using System.Collections.Generic;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Data used to define a new hull spawned by SOF.
    /// </summary>
    [System.Serializable]
    public struct EveSOFHull
    {
        /// <summary>
        /// The name of the hull.
        /// </summary>
        public string name;

        /// <summary>
        /// Res path to a .obj file to use for the ship.
        /// </summary>
        public string geometryResPath;

        /// <summary>
        /// The shader to be used to render the hull.
        /// </summary>
        public string shader;

        /// <summary>
        /// Res path to the arMap.
        /// </summary>
        public string arMap;
        /// <summary>
        /// Res path to the pmdgMap.
        /// </summary>
        public string pmdgMap;
        /// <summary>
        /// Res path to the noMap.
        /// </summary>
        public string noMap;
        /// <summary>
        /// Res path to the detail map.
        /// </summary>
        public string detailMap;

        /// <summary>
        /// Bounding sphere - used to define a sphere collider.
        /// </summary>
        public Vector3 boundingSphereCenter;
        /// <summary>
        /// Bounding sphere radius - used to define a sphere collider.
        /// </summary>
        public float boundingSphereRadius;

        /// <summary>
        /// List of boosters to use with this hull.
        /// </summary>
        public List<EveSOFBoosterItem> boosters;

        /// <summary>
        /// List of sprites.
        /// </summary>
        public List<EveSOFSpriteSet> spriteSets;
    }
}