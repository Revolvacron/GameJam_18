using System.Collections.Generic;

namespace EVE.SOF
{
    /// <summary>
    /// Defines sprite set items (blinkies) to use on hulls.
    /// </summary>
    [System.Serializable]
    public struct EveSOFSpriteSet
    {
        /// <summary>
        /// The position of the sprite set.
        /// </summary>
        public List<EveSOFSpriteSetItem> sprites;
    }
}