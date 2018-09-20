using System.Collections.Generic;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// An interface to a SpaceObjectFactory. Should be used in a scene so there is only need to load the
    /// sof data into a cache once. Only one of these should be needed per scene.
    /// 
    /// A SOF instance can also be used directly in code, but should always be serialized to avoid long loading times.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(SOFContainer))]
    public class SOFInterface : MonoBehaviour
    {
        /// <summary>
        /// The dna to use when generating the next ship.
        /// </summary>
        public string dna = "";
        /// <summary>
        /// The uniform scale to use for generated ships.
        /// </summary>
        public float modelScale = 1.0f;
        /// <summary>
        /// The dirt level to use on a newly generated ship.s
        /// </summary>
        [Range(-2.0f, 0.7f)]
        public float dirtAmount = 0.3f;

        /// <summary>
        /// A list of plugins to use. These are called after a ship is created.
        /// </summary>
        public List<SpaceObjectFactoryPlugin> plugins = new List<SpaceObjectFactoryPlugin>();

        /// <summary>
        /// A sub-container for the sof and related data cache to be held to avoid editor performance issues.
        /// </summary>
        private SOFContainer _sofContainer = null;

        /// <summary>
        /// Start.
        /// </summary>
        public void Start()
        {
            LoadIfRequired();
        }

        /// <summary>
        /// Reloads the cache and recreates the sof instance if it's needed.
        /// </summary>
        public void LoadIfRequired()
        {
            _sofContainer = GetComponent<SOFContainer>();
            if (_sofContainer.cache == null)
                RebuildCache();
            if (_sofContainer.sof == null)
                _sofContainer.sof = new SOF(_sofContainer.cache);
        }

        /// <summary>
        /// Rebuilds the data cache and then reinitializes SOF to point ot the new cache.
        /// </summary>
        public void RebuildCache()
        {
            if (_sofContainer == null)
                _sofContainer = GetComponent<SOFContainer>();
            _sofContainer.cache = SOFDataCacheBuilder.ConstructDataCache();
            _sofContainer.sof = new SOF(_sofContainer.cache);
        }

        /// <summary>
        /// Spawns a ship based on the dna, modelScale and dirtAmount properties.
        /// </summary>
        /// <returns>The newly created ship or null if it fails.</returns>
        public GameObject SpawnShip()
        {
            LoadIfRequired();
            _sofContainer.sof.plugins = plugins;
            var spaceObject = _sofContainer.sof.ConstructFromDNA(this.dna, this.modelScale, this.dirtAmount);
            if (spaceObject != null)
            {
                spaceObject.transform.position = transform.position;
                spaceObject.transform.rotation = transform.rotation;
            }
            return spaceObject;
        }

        /// <summary>
        /// Spawns a ship based on the dna, modelScale and dirtAmount properties.
        /// </summary>
        /// <param name="dna">Override the class dna with this dna.</param>
        /// <param name="size">Override the modelScale with this size.</param>
        /// <param name="dirtAmount">Override the dirtAmount with this amount.</param>
        /// <returns>The newly created ship or null if it fails.</returns>
        public GameObject SpawnShip(string dna, float size, float dirtAmount)
        {
            LoadIfRequired();
            _sofContainer.sof.plugins = plugins;
            var spaceObject = _sofContainer.sof.ConstructFromDNA(dna, size, dirtAmount);
            if (spaceObject != null)
            {
                spaceObject.transform.position = transform.position;
                spaceObject.transform.rotation = transform.rotation;
            }
            return spaceObject;
        }
    }
}
