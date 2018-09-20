using System;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Definition of a material to use on a hull.
    /// </summary>
    [System.Serializable]
    public struct EveSOFMaterial
    {
        /// <summary>
        /// The name of the material.
        /// </summary>
        public string name;
        /// <summary>
        /// The diffuse color.
        /// </summary>
        public Color diffuseColor;
        /// <summary>
        /// The fresnel color. Color used to tint the specular color.
        /// </summary>
        public Color fresnelColor;
        /// <summary>
        /// The gloss amount of this material.
        /// </summary>
        [Range(0.0f, 1.0f)]
        public float gloss;
        /// <summary>
        /// The color of the dust on this material.
        /// </summary>
        public Color dustDiffuseColor;
    }
}