using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Adds a mesh collider when a new ship is generated.
    /// </summary>
    [CreateAssetMenu(fileName = "SpaceObjectFactoryMeshColliderPlugin", menuName = "SOF Plugins/Mesh Collider")]
    public class SpaceObjectFactoryMeshColliderPlugin : SpaceObjectFactoryPlugin
    {
        /// <summary>
        /// Sets if the collider should be convex. This is good for performance, so it should
        /// only be false for very large objects like citadels.
        /// </summary>
        public bool isConvex = true;

        /// <summary>
        /// Called after a ship has been generated.
        /// </summary>
        /// <param name="ship">The new ship game object</param>
        /// <param name="sofHull">The sofHull info.</param>
        /// <param name="sofFaction">The sofFaction info.</param>
        /// <param name="sofRace">The sofRace info.</param>
        public override void OnShipGenerated(GameObject ship, EveSOFHull sofHull, EveSOFFaction sofFaction, EveSOFRace sofRace)
        {
            ship.RemoveComponent<SphereCollider>();
            var meshCollider = ship.AddComponent<MeshCollider>();
            meshCollider.convex = isConvex;
        }
    }
}
