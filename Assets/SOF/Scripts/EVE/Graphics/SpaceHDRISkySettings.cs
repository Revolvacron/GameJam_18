using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;
using EVE.SOF;

namespace EVE.Graphics
{
    /// <summary>
    /// The settings for the space skybox renderer. Can be used in a volume profile.
    /// </summary>
    [SkyUniqueID(4)]
    public class SpaceHDRISkySettings : SkySettings
    {
        /// <summary>
        /// Cubemap used to render the sky.
        /// </summary>
        [Tooltip("Cubemap used to render the sky.")]
        public TextureParameter cubemap = new TextureParameter(null);
        /// <summary>
        /// Texture of stars to be tiled across the skybox.
        /// </summary>
        [Tooltip("Tileable stars texture.")]
        public TextureParameter starsTexture = new TextureParameter(null);
        /// <summary>
        /// Shader to use to render the skybox.
        /// </summary>
        [Tooltip("The shader to use to render the skybox.")]
        public Shader spaceSkyboxShader = null;

        /// <summary>
        /// Creates the space sky renderer object.
        /// </summary>
        /// <returns>A new renderer.</returns>
        public override SkyRenderer CreateRenderer()
        {
            spaceSkyboxShader = Resources.Load<Shader>(Paths.ResolveResPath("Materials/SpaceHDRISkyShader"));
            return new SpaceHDRISkyRenderer(this);
        }

        /// <summary>
        /// Generates the hash code for this object.
        /// </summary>
        /// <returns>The hashcode for this object.</returns>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();

            unchecked
            {
                hash = cubemap.value != null ? hash * 23 + cubemap.GetHashCode() : hash;
            }

            return hash;
        }
    }
}
