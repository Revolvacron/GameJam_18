using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EVE.SOF
{
    [CreateAssetMenu(fileName = "SpaceObjectFactoryRigidBodyPlugin", menuName = "SOF Plugins/RigidBody")]
    public class SpaceObjectFactoryRigidBodyPlugin : SpaceObjectFactoryPlugin
    {
        /// <summary>
        /// Set's if the rigid body should have the isKinematic flag set.
        /// </summary>
        public bool isKinematic = false;
        /// <summary>
        /// If true, the mass of the object will be mass * ship radius. If false, the mass will just be mass.
        /// </summary>
        public bool deriveMassFromRadius = true;
        /// <summary>
        /// The mass of the new object. If deriveMassFromRadius is true this will be multiplied by the ship
        /// radius.
        /// </summary>
        public float mass = 1.0f;
        /// <summary>
        /// Whether to use gravity on the body.
        /// </summary>
        public bool useGravity = false;

        /// <summary>
        /// Called after a ship has been generated.
        /// </summary>
        /// <param name="ship">The new ship game object</param>
        /// <param name="sofHull">The sofHull info.</param>
        /// <param name="sofFaction">The sofFaction info.</param>
        /// <param name="sofRace">The sofRace info.</param>
        public override void OnShipGenerated(GameObject ship, EveSOFHull sofHull, EveSOFFaction sofFaction, EveSOFRace sofRace)
        {
            var rigidBody = ship.AddComponent<Rigidbody>();
            rigidBody.isKinematic = isKinematic;
            if (deriveMassFromRadius)
                rigidBody.mass = sofHull.boundingSphereRadius * mass;
            else
                rigidBody.mass = mass;
            rigidBody.useGravity = useGravity;
        }
    }
}