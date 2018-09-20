using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EVE.SOF
{
    public abstract class SpaceObjectFactoryPlugin : ScriptableObject
    {

        /// <summary>
        /// Called after a ship has been generated.
        /// </summary>
        /// <param name="ship">The new ship game object</param>
        /// <param name="sofHull">The sofHull info.</param>
        /// <param name="sofFaction">The sofFaction info.</param>
        /// <param name="sofRace">The sofRace info.</param>
        public abstract void OnShipGenerated(GameObject ship, EveSOFHull sofHull, EveSOFFaction sofFaction, EveSOFRace sofRace);
    }
}