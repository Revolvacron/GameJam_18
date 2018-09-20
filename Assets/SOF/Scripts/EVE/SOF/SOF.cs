using System.Collections.Generic;
using UnityEngine;
using EVE.Graphics;

namespace EVE.SOF
{
    /// <summary>
    /// The space object factory.
    /// 
    /// When configured can be used to generate EVE ships/space assets from information loaded from data.red.
    /// </summary>
    [System.Serializable]
    public class SOF
    {
        /// <summary>
        /// The material used on SOF ships.
        /// </summary>
        public static string BASE_SOF_MATERIAL = Paths.ResolveResPath("Materials/BaseSOFMaterial");
        public static string ROCK_SOF_MATERIAL = Paths.ResolveResPath("Materials/RockSOFMaterial");
        /// <summary>
        /// The material to use for boosters.
        /// </summary>
        public static string BOOSTER_MATERIAL = Paths.ResolveResPath("Materials/BoosterMaterial");
        /// <summary>
        /// The material to use for sprites.
        /// </summary>
        public static string SPRITE_MATERIAL = Paths.ResolveResPath("Materials/SpriteMaterial");

        /// <summary>
        /// A list of plugins which are used when a ship is generated.
        /// </summary>
        public List<SpaceObjectFactoryPlugin> plugins = new List<SpaceObjectFactoryPlugin>();

        /// <summary>
        /// A cache of data loaded from data.red.
        /// Serialized to avoid constant need to rebuild.
        /// </summary>
        [SerializeField]
        private EveSOFDataCache cache = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cache">A cache of sof data to use.</param>
        public SOF(EveSOFDataCache cache = null)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Adds hull variation to a new ship. 
        /// </summary>
        /// <param name="ship">The new ship.</param>
        /// <param name="hull">The sofHull information to apply.</param>
        /// <param name="meshFilter">The mesh filter to populated with the hull geometry.</param>
        /// <param name="boundingSphere">The bounding sphere to update for the hull.</param>
        /// <param name="materialProperties">The material properties to add the texture maps to.</param>
        public void ApplyHull(GameObject ship, EveSOFHull hull, MeshFilter meshFilter, SphereCollider boundingSphere, MaterialPropertyHandler materialProperties)
        {
            meshFilter.mesh = Resources.Load<Mesh>(Paths.ResolveResPath(hull.geometryResPath));

            materialProperties.arMap = Resources.Load<Texture>(Paths.ResolveResPath(hull.arMap));
            materialProperties.pmdgMap = Resources.Load<Texture>(Paths.ResolveResPath(hull.pmdgMap));
            materialProperties.noMap = Resources.Load<Texture>(Paths.ResolveResPath(hull.noMap));
            materialProperties.detailMap = null;
            if (hull.detailMap != null && hull.detailMap != "")
            {
                materialProperties.detailMap = Resources.Load<Texture>(Paths.ResolveResPath(hull.detailMap));
            }

            boundingSphere.center = hull.boundingSphereCenter;
            boundingSphere.radius = hull.boundingSphereRadius;
        }

        /// <summary>
        /// Applies faction variation to a new ship.
        /// </summary>
        /// <param name="ship">The new ship.</param>
        /// <param name="faction">The sofFaction to apply.</param>
        /// <param name="materialProperties">The material properties to apply the faciton variation to.s</param>
        public void ApplyFaction(GameObject ship, EveSOFFaction faction, MaterialPropertyHandler materialProperties)
        {
            ApplyMaterial(cache.materials[faction.material1], materialProperties, 1);
            ApplyMaterial(cache.materials[faction.material2], materialProperties, 2);
            ApplyMaterial(cache.materials[faction.material3], materialProperties, 3);
            ApplyMaterial(cache.materials[faction.material4], materialProperties, 4);

            materialProperties.glowColor = faction.glowColor;
        }

        /// <summary>
        /// Applies race variation to the ship.
        /// </summary>
        /// <param name="ship">The ship to apply race variation to.</param>
        /// <param name="race">The sofRace information to apply.</param>
        /// <param name="materialProperties">The material properties to apply the race variation to.</param>
        public void ApplyRace(GameObject ship, EveSOFRace race, MaterialPropertyHandler materialProperties)
        {
        }

        /// <summary>
        /// Applies a material to the new ship by setting the materialProperties.
        /// </summary>
        /// <param name="material">The sofMaterial which contains the color information to use.</param>
        /// <param name="materialProperties">The material properties which is used to set the material data on the ship during runtime.</param>
        /// <param name="materialIndex">The index of the material (1 - 4)</param>
        public void ApplyMaterial(EveSOFMaterial material, MaterialPropertyHandler materialProperties, int materialIndex)
        {
            switch (materialIndex)
            {
                case 1:
                    materialProperties.material1DiffuseColor = material.diffuseColor;
                    materialProperties.material1FresnelColor = material.fresnelColor;
                    materialProperties.material1DustDiffuseColor = material.dustDiffuseColor;
                    materialProperties.material1Gloss = material.gloss;
                    break;
                case 2:
                    materialProperties.material2DiffuseColor = material.diffuseColor;
                    materialProperties.material2FresnelColor = material.fresnelColor;
                    materialProperties.material2DustDiffuseColor = material.dustDiffuseColor;
                    materialProperties.material2Gloss = material.gloss;
                    break;
                case 3:
                    materialProperties.material3DiffuseColor = material.diffuseColor;
                    materialProperties.material3FresnelColor = material.fresnelColor;
                    materialProperties.material3DustDiffuseColor = material.dustDiffuseColor;
                    materialProperties.material3Gloss = material.gloss;
                    break;
                case 4:
                    materialProperties.material4DiffuseColor = material.diffuseColor;
                    materialProperties.material4FresnelColor = material.fresnelColor;
                    materialProperties.material4DustDiffuseColor = material.dustDiffuseColor;
                    materialProperties.material4Gloss = material.gloss;
                    break;
                default:
                    throw new System.Exception("Unsupported material index. Please use an index of 1 - 4.");
            }
        }

        /// <summary>
        /// Sets the dirt amount on a ship.
        /// </summary>
        /// <param name="materialProperties">The material properties which stores the dirt amount.</param>
        /// <param name="dirtAmount">The amount of dirt.</param>
        public void SetDirtLevel(MaterialPropertyHandler materialProperties, float dirtAmount)
        {
            materialProperties.dirtAmount = dirtAmount;
        }

        /// <summary>
        /// Adds boosters to the ship.
        /// </summary>
        /// <param name="ship">The ship to add boosters too.</param>
        /// <param name="hull">The sofHull which defines what boosters to add.</param>
        /// <param name="race">The race which defines the color of the boosters.</param>
        public void AddBoosters(GameObject ship, EveSOFHull hull, EveSOFRace race)
        {
            foreach (var booster in hull.boosters)
            {
                var boosterObject = new GameObject();
                boosterObject.name = "Booster";
                boosterObject.transform.FromMatrix(booster.locationMatrix);
                boosterObject.transform.SetParent(ship.transform);

                var lineRenderer = boosterObject.AddComponent<LineRenderer>();

                var boosterGradient = new Gradient();
                boosterGradient.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0), new GradientAlphaKey(0, 1.0f) };
                boosterGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(race.boosterColor, 0), new GradientColorKey(race.boosterColor / 2, 1.0f) };
                lineRenderer.colorGradient = boosterGradient;
                lineRenderer.material = Resources.Load<Material>(BOOSTER_MATERIAL);

                boosterObject.AddComponent<EveBooster>();
            }
        }

        /// <summary>
        /// Adds boosters to the ship.
        /// </summary>
        /// <param name="ship">The ship to add boosters too.</param>
        /// <param name="hull">The sofHull which defines what boosters to add.</param>
        /// <param name="race">The race which defines the color of the boosters.</param>
        public void AddSprites(GameObject ship, EveSOFHull hull, EveSOFFaction faction)
        {
            foreach (var spriteSet in hull.spriteSets)
            {
                foreach (var sprite in spriteSet.sprites)
                {
                    var spriteObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    GameObject.DestroyImmediate(spriteObj.GetComponent<MeshCollider>());

                    spriteObj.name = "Sprite";
                    spriteObj.transform.position = sprite.position;
                    spriteObj.transform.SetParent(ship.transform);

                    var mesh = spriteObj.GetComponent<MeshRenderer>();
                    mesh.sharedMaterial = Resources.Load<Material>(SPRITE_MATERIAL);

                    var spriteComponent = spriteObj.AddComponent<EveSprite>();
                    spriteComponent.blinkRate = sprite.blinkRate;
                    spriteComponent.blinkPhase = sprite.blinkPhase;
                    spriteComponent.minScale = sprite.minScale;
                    spriteComponent.maxScale = sprite.maxScale;
                    spriteComponent.falloff = sprite.falloff;
                    spriteComponent.color = faction.glowColor;
                }
            }
        }

        /// <summary>
        /// Returns the material appropriate for the asset described by the given hull.
        /// </summary>
        /// <param name="hull">The hull that describes the asset.</param>
        /// <returns>A material to use.</returns>
        public Material GetMaterial(EveSOFHull hull)
        {
            if (hull.shader == "organic/rockv5.fx" || hull.shader == "organic/asteroidv5.fx" || hull.shader == "quad/quadenvironmentv5.fx")
            {
                return Resources.Load<Material>(ROCK_SOF_MATERIAL);
            }
            else
            {
                return Resources.Load<Material>(BASE_SOF_MATERIAL);
            }
        }

        /// <summary>
        /// Constructs a ship from a supplied dna string. The string is expected to be valid.
        /// 
        /// Returns null if failed.
        /// </summary>
        /// <param name="dna">The dna of the ship to make. Must be in the form "hullName:factionName:raceName".</param>
        /// <param name="modelScale">The uniform scale of the new ship.</param>
        /// <param name="dirtAmount">The dirt amount ot apply to the new ship. Expected to be in the range of 0 - 1.</param>
        /// <returns>The new ship or null if creation of the ship fails.</returns>
        public GameObject ConstructFromDNA(string dna, float modelScale = 1.0f, float dirtAmount = 0.3f)
        {
            // initialise the ship and components.
            GameObject ship = new GameObject();
            try
            {
                var parts = dna.Split(':');
                var hull = parts[0];
                var faction = parts[1];
                var race = parts[2];

                var sofHull = cache.hulls[hull];
                var sofFaction = cache.factions[faction];
                var sofRace = cache.races[race];

                var meshFilter = ship.AddComponent<MeshFilter>();
                var renderer = ship.AddComponent<MeshRenderer>();
                renderer.sharedMaterial = GetMaterial(sofHull);
                var materialProperties = ship.AddComponent<MaterialPropertyHandler>();
                var boundingSphere = ship.AddComponent<SphereCollider>();

                ship.transform.name = dna;

                ApplyHull(ship, sofHull, meshFilter, boundingSphere, materialProperties);
                ApplyFaction(ship, sofFaction, materialProperties);
                ApplyRace(ship, sofRace, materialProperties);
                SetDirtLevel(materialProperties, dirtAmount);
                AddBoosters(ship, sofHull, sofRace);
                AddSprites(ship, sofHull, sofFaction);

                ship.transform.localScale = new Vector3(modelScale, modelScale, modelScale);

                foreach (var plugin in plugins)
                {
                    plugin.OnShipGenerated(ship, sofHull, sofFaction, sofRace);
                }
            }
            catch (System.Exception obj)
            {
                Debug.LogError(obj);
                GameObject.DestroyImmediate(ship);
                ship = null;
            }

            return ship;
        }
    }
}