using System;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Defines sprite set items (blinkies) to use on hulls.
    /// </summary>
    [System.Serializable]
    public struct EveSOFSpriteSetItem
    {
        /// <summary>
        /// The position of the sprite.
        /// </summary>
        public Vector3 position;
        /// <summary>
        /// The blink rate - if not positive value, sprite doesn't blink
        /// </summary>
        public float blinkRate;
        /// <summary>
        /// Offset phase for the blinking.
        /// </summary>
        public float blinkPhase;
        /// <summary>
        /// Minimum size.
        /// </summary>
        public float minScale;
        /// <summary>
        /// Maximum size.
        /// </summary>
        public float maxScale;
        /// <summary>
        /// Camera based scaling.
        /// </summary>
        public float falloff;
    }
}