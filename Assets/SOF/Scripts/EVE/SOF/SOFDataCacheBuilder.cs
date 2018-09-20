using System;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;
using UnityEngine;

namespace EVE.SOF
{
    /// <summary>
    /// Used to construct a EveSOFDataCache object.
    /// </summary>
    public static class SOFDataCacheBuilder
    {
        /// <summary>
        /// Res path to data.red.
        /// </summary>
        public static string DATA = Paths.ResolveResPathGeneral("res:/dx9/model/SpaceObjectFactory/data.red");
        /// <summary>
        /// The power to use to apply gamma correction to colors.
        /// </summary>
        public static float GAMMA_CORRECTION_POWER = 1.0f / 2.2f;

        /// <summary>
        /// Constructs a new EveSOFDataCache.
        /// </summary>
        /// <returns>A newly constructed EveSOFDataCache.</returns>
        public static EveSOFDataCache ConstructDataCache()
        {
            var newCache = new EveSOFDataCache();
            YamlDocument doc = _LoadYamlFile(DATA);

            _ExtractHullData(ref newCache, doc);
            _ExtractFactionData(ref newCache, doc);
            _ExtractRaceData(ref newCache, doc);
            _ExtractMaterialData(ref newCache, doc);

            newCache.UpdateSerialization();

            return newCache;
        }

        /// <summary>
        /// Extracts all sofHull information from the data.red yaml document.
        /// </summary>
        /// <param name="newCache">The cache to add the hull data to.</param>
        /// <param name="doc">The data.red yaml document.</param>
        public static void _ExtractHullData(ref EveSOFDataCache newCache, YamlDocument doc)
        {
            var hullData = (YamlSequenceNode)doc.RootNode["hull"];
            foreach (YamlMappingNode hull in hullData.Children)
            {
                var sofHull = new EveSOFHull();
                var name = hull["name"].ToString();
                sofHull.name = name;
                sofHull.geometryResPath = hull["geometryResFilePath"].ToString();
                var boundsInfo = _ExtractVector4((YamlSequenceNode)hull["boundingSphere"]);
                sofHull.boundingSphereCenter = new Vector3(boundsInfo.x, boundsInfo.y, boundsInfo.z);
                sofHull.boundingSphereRadius = boundsInfo.w;
                try
                {
                    _ExtractOpaqueAreas(hull, ref sofHull);
                    _ExtractBoosters(hull, ref sofHull);
                    _ExtractSpriteSets(hull, ref sofHull);
                    newCache.hulls[name] = sofHull;
                }
                catch
                {
                    Debug.LogError("Error loading hull: " + name + ".");
                }
            }
        }

        /// <summary>
        /// Extracts opaque area info from a yaml node and adds it to the referenced sofHull.
        /// </summary>
        /// <param name="hull">The hull node from data.red.</param>
        /// <param name="sofHull">The sofhull being loaded to which opaque area info should be added.</param>
        public static void _ExtractOpaqueAreas(YamlMappingNode hull, ref EveSOFHull sofHull)
        {
            var opaqueAreas = (YamlSequenceNode)hull["opaqueAreas"];

            sofHull.arMap = null;
            sofHull.pmdgMap = null;
            sofHull.noMap = null;
            sofHull.detailMap = null;

            foreach (YamlMappingNode area in opaqueAreas)
            {
                sofHull.shader = area["shader"].ToString();
                var textures = (YamlSequenceNode)area["textures"];
                foreach (YamlMappingNode texture in textures)
                {
                    switch (texture["name"].ToString())
                    {
                        case "ArMap":
                            sofHull.arMap = texture["resFilePath"].ToString();
                            break;
                        case "PmdgMap":
                            sofHull.pmdgMap = texture["resFilePath"].ToString();
                            break;
                        case "NoMap":
                            sofHull.noMap = texture["resFilePath"].ToString();
                            break;
                        case "Detail1Map":
                            sofHull.detailMap = texture["resFilePath"].ToString();
                            break;
                    }
                }
                if (sofHull.arMap != null && sofHull.pmdgMap != null && sofHull.noMap != null)
                {
                    // all textures found...
                    break;
                }
            }
        }

        /// <summary>
        /// Extracts booster info from a yaml node and adds it to the referenced sofHull.
        /// </summary>
        /// <param name="hull">The hull node from data.red.</param>
        /// <param name="sofHull">The sofhull being loaded to which boosters should be added.</param>
        public static void _ExtractBoosters(YamlMappingNode hull, ref EveSOFHull sofHull)
        {
            sofHull.boosters = new List<EveSOFBoosterItem>();
            try
            {
                var boosters = (YamlSequenceNode)hull["booster"]["items"];
                foreach (YamlMappingNode booster in boosters)
                {
                    var boosterItem = new EveSOFBoosterItem();
                    boosterItem.locationMatrix = _ExtractMatrix4x4((YamlSequenceNode)booster["transform"]);
                    sofHull.boosters.Add(boosterItem);
                }
            }
            catch
            {
                // IGNORE: there aren't any boosters.
            }
        }

        /// <summary>
        /// Extracts sprite set info from a yaml node and adds it to the referenced sofHull.
        /// </summary>
        /// <param name="hull">The hull node from data.red.</param>
        /// <param name="sofHull">The sofhull being loaded to which sprites should be added.</param>
        public static void _ExtractSpriteSets(YamlMappingNode hull, ref EveSOFHull sofHull)
        {
            sofHull.spriteSets = new List<EveSOFSpriteSet>();
            try
            {
                var spriteSets = (YamlSequenceNode)hull["spriteSets"];
                foreach (YamlMappingNode set in spriteSets)
                {
                    var spriteSet = new EveSOFSpriteSet();
                    spriteSet.sprites = new List<EveSOFSpriteSetItem>();
                    var sprites = (YamlSequenceNode)set["items"];
                    foreach (YamlMappingNode sprite in sprites)
                    {
                        var spriteItem = new EveSOFSpriteSetItem();

                        spriteItem.blinkRate = TryGetValue<float>((YamlMappingNode)sprite, "blinkRate", 1.0f, _ExtractFloat);
                        spriteItem.blinkPhase = TryGetValue<float>((YamlMappingNode)sprite, "blinkPhase", 0.0f, _ExtractFloat);
                        spriteItem.minScale = TryGetValue<float>((YamlMappingNode)sprite, "minScale", 1.0f, _ExtractFloat);
                        spriteItem.maxScale = TryGetValue<float>((YamlMappingNode)sprite, "maxScale", 1.0f, _ExtractFloat);
                        spriteItem.falloff = TryGetValue<float>((YamlMappingNode)sprite, "falloff", 0.0f, _ExtractFloat);
                        spriteItem.position = TryGetValue<Vector3>((YamlMappingNode)sprite, "position", Vector3.zero, _ExtractVector3);

                        spriteSet.sprites.Add(spriteItem);
                    }
                    sofHull.spriteSets.Add(spriteSet);
                }
            }
            catch (System.Exception)
            {
                // IGNORE: there aren't any sprites.
            }
        }

        /// <summary>
        /// Extracts all sofFaction information from the data.red yaml document.
        /// </summary>
        /// <param name="newCache">The cache to add the faction data to.</param>
        /// <param name="doc">The data.red yaml document.</param>
        public static void _ExtractFactionData(ref EveSOFDataCache newCache, YamlDocument doc)
        {
            var factionData = (YamlSequenceNode)doc.RootNode["faction"];
            foreach (YamlMappingNode faction in factionData.Children)
            {
                var sofFaction = new EveSOFFaction();
                var name = faction["name"].ToString();
                sofFaction.name = name;
                sofFaction.material1 = faction["areaTypes"]["Primary"]["material1"].ToString();
                sofFaction.material2 = faction["areaTypes"]["Primary"]["material2"].ToString();
                sofFaction.material3 = faction["areaTypes"]["Primary"]["material3"].ToString();
                sofFaction.material4 = faction["areaTypes"]["Primary"]["material4"].ToString();
                try
                {
                    sofFaction.glowColor = _ExtractColorFromYaml((YamlSequenceNode)faction["colorSet"]["Hull"]);
                }
                catch
                {
                    sofFaction.glowColor = Color.white;
                }
                newCache.factions[name] = sofFaction;
            }
        }

        /// <summary>
        /// Extracts all sofRace information from the data.red yaml document.
        /// </summary>
        /// <param name="newCache">The cache to add the race data to.</param>
        /// <param name="doc">The data.red yaml document.</param>
        public static void _ExtractRaceData(ref EveSOFDataCache newCache, YamlDocument doc)
        {
            var raceData = (YamlSequenceNode)doc.RootNode["race"];
            foreach (YamlMappingNode race in raceData.Children)
            {
                var sofRace = new EveSOFRace();
                var name = race["name"].ToString();
                sofRace.name = name;
                try
                {
                    sofRace.boosterColor = _ExtractColorFromYaml((YamlSequenceNode)race["booster"]["glowColor"]);
                }
                catch
                {
                    sofRace.boosterColor = Color.black;
                }
                newCache.races[name] = sofRace;
            }
        }

        /// <summary>
        /// Extracts all sofMaterial information from the data.red yaml document.
        /// </summary>
        /// <param name="newCache">The cache to add the material data to.</param>
        /// <param name="doc">The data.red yaml document.</param>
        public static void _ExtractMaterialData(ref EveSOFDataCache newCache, YamlDocument doc)
        {
            var materialData = (YamlSequenceNode)doc.RootNode["material"];
            foreach (YamlMappingNode material in materialData)
            {
                var sofMaterial = new EveSOFMaterial();
                var name = material["name"].ToString();
                sofMaterial.name = name;
                var parameters = (YamlSequenceNode)material["parameters"];
                foreach (YamlMappingNode param in parameters)
                {
                    var paramName = param["name"].ToString();
                    switch (paramName)
                    {
                        case "DiffuseColor":
                            sofMaterial.diffuseColor = _ExtractColorFromYaml((YamlSequenceNode)param["value"]);
                            break;
                        case "DustDiffuseColor":
                            sofMaterial.dustDiffuseColor = _ExtractColorFromYaml((YamlSequenceNode)param["value"]);
                            break;
                        case "FresnelColor":
                            sofMaterial.fresnelColor = _ExtractColorFromYaml((YamlSequenceNode)param["value"]);
                            break;
                        case "Gloss":
                            sofMaterial.gloss = _ExtractFloatArray((YamlSequenceNode)param["value"])[0];
                            break;
                    }
                }
                newCache.materials[name] = sofMaterial;
            }
        }

        /// <summary>
        /// Extracts a float value from a given node.
        /// </summary>
        /// <param name="node">The node to extract the value from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>Float</returns>
        public static float _ExtractFloat(string nodeString, float defaultValue=0.0f, bool quiet = true)
        {
            try
            {
                return float.Parse(nodeString);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Extracts a float array from a given sequence node.
        /// </summary>
        /// <param name="node">The yaml node to read from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>A float array object.</returns>
        public static float[] _ExtractFloatArray(YamlSequenceNode node, float[] defaultValue=null, bool quiet = true)
        {
            try
            {
                var rv = new float[node.Children.Count];
                for (int i = 0; i < node.Children.Count; ++i)
                {
                    rv[i] = float.Parse(node.Children[i].ToString());
                }
                return rv;
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Extracts a Vector2 from a given sequence node.
        /// </summary>
        /// <param name="node">The yaml node to read from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>A Vector2 object.</returns>
        public static Vector2 _ExtractVector2(YamlSequenceNode node, Vector2 defaultValue=default(Vector2), bool quiet = true)
        {
            try
            {
                var values = _ExtractFloatArray(node);
                return new Vector2(values[0], values[1]);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Extracts a Vector3 from a given sequence node.
        /// </summary>
        /// <param name="node">The yaml node to read from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>A Vector3 object.</returns>
        public static Vector3 _ExtractVector3(YamlSequenceNode node, Vector3 defaultValue=default(Vector3), bool quiet = true)
        {
            try
            { 
                var values = _ExtractFloatArray(node);
                return new Vector3(-values[0], values[1], values[2]);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Extracts a Vector4 from a given sequence node.
        /// </summary>
        /// <param name="node">The yaml node to read from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>A Vector4 object.</returns>
        public static Vector4 _ExtractVector4(YamlSequenceNode node, Vector4 defaultValue=default(Vector4), bool quiet = true)
        {
            try
            { 
                var values = _ExtractFloatArray(node);
                return new Vector4(values[0], values[1], values[2], values[3]);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Extracts a 4x4 matrix from a given sequence node.
        /// </summary>
        /// <param name="node">The yaml node to read from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>A Matrix4x4 object.</returns>
        public static Matrix4x4 _ExtractMatrix4x4(YamlSequenceNode node, Matrix4x4 defaultValue = default(Matrix4x4), bool quiet = true)
        {
            try
            {
                var values = _ExtractFloatArray(node);
                var column0 = new Vector4(values[0], values[1], values[2], values[3]);
                var column1 = new Vector4(values[4], values[5], values[6], values[7]);
                var column2 = new Vector4(values[8], values[9], values[10], values[11]);
                var column3 = new Vector4(values[12], values[13], values[14], values[15]);
                return new Matrix4x4(column0, column1, column2, column3);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Applies gamma correction - pow(value, 0.45454545) - to a given float.
        /// </summary>
        /// <param name="value">The value to correct.</param>
        /// <returns>A corrected value.</returns>
        public static float ApplyGammaCorrection(float value)
        {
            return Mathf.Pow(value, GAMMA_CORRECTION_POWER);
        }

        /// <summary>
        /// Applies gamma correction - pow(color, 0.45454545) - to a given color.
        /// </summary>
        /// <param name="c">The color to correct.</param>
        /// <returns>A corrected color.</returns>
        public static Color ApplyGammaCorrection(Color c)
        {
            return new Color(
                ApplyGammaCorrection(c.r),
                ApplyGammaCorrection(c.g),
                ApplyGammaCorrection(c.b),
                ApplyGammaCorrection(c.a));
        }

        /// <summary>
        /// Extracts a color as a Color object from a given sequence node.
        /// </summary>
        /// <param name="node">The yaml node to read from.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>A Color object.</returns>
        public static Color _ExtractColorFromYaml(YamlSequenceNode node, Color defaultValue=default(Color), bool applyGammaCorrection = true, bool quiet = true)
        {
            try
            {
                var values = _ExtractFloatArray(node);
                if (applyGammaCorrection)
                    return new Color(
                        ApplyGammaCorrection(values[0]),
                        ApplyGammaCorrection(values[1]),
                        ApplyGammaCorrection(values[2]),
                        ApplyGammaCorrection(values[3]));
                else
                    return new Color(values[0], values[1], values[2], values[3]);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Makes an attempt to extract a value at the given key of the node supplied and apply a processing function.
        /// </summary>
        /// <typeparam name="T">The type of item to return.</typeparam>
        /// <param name="node">The node to process.</param>
        /// <param name="key">The key of the item to extract from the node.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <param name="processingFunction">The function to process the value by.</param>
        /// <returns>The value or the default on failure.</returns>
        public static T TryGetValue<T>(YamlMappingNode node, string key, T defaultValue, Func<YamlSequenceNode, T, bool, T> processingFunction = null, bool quiet = true)
        {
            try
            {
                return processingFunction((YamlSequenceNode)node[key], defaultValue, quiet);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Makes an attempt to extract a value at the given key of the node supplied and apply a processing function.
        /// </summary>
        /// <typeparam name="T">The type of item to return.</typeparam>
        /// <param name="node">The node to process.</param>
        /// <param name="key">The key of the item to extract from the node.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <param name="processingFunction">The function to process the value by.</param>
        /// <returns>The value or the default on failure.</returns>
        public static T TryGetValue<T>(YamlMappingNode node, string key, T defaultValue, Func<string, T, bool, T> processingFunction = null, bool quiet = true)
        {
            try
            {
                return processingFunction(node[key].ToString(), defaultValue, quiet);
            }
            catch (System.Exception e)
            {
                if (!quiet)
                    Debug.LogError(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Loads a yaml file from a an asset path.
        /// </summary>
        /// <param name="path">The asset path to the yaml file.</param>
        /// <returns>The loaded file.</returns>
        public static YamlDocument _LoadYamlFile(string path)
        {
            var streamReader = new StreamReader(path);
            var yaml = new YamlStream();
            yaml.Load(streamReader);
            YamlDocument doc = yaml.Documents[0];
            return doc;
        }
    }
}
