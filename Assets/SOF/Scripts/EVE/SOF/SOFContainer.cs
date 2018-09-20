using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EVE.SOF
{
    public class SOFContainer : MonoBehaviour
    {
        /// <summary>
        /// The spaceobject factory. Serialized to avoid constant reloading being necessary.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        public SOF sof = null;
        /// <summary>
        /// The SOF data cache. Serialized to avoid constant reloading being necessary.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        public EveSOFDataCache cache = null;
    }
}