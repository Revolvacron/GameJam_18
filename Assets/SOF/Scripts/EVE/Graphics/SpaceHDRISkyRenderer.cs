using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace EVE.Graphics
{
    /// <summary>
    /// Renderer object for an EVE Space scene skybox.
    /// </summary>
    public class SpaceHDRISkyRenderer : SkyRenderer
    {
        /// <summary>
        /// Material to use.
        /// </summary>
        Material _skyHDRIMaterial;
        /// <summary>
        /// Property block used to update the material.
        /// </summary>
        MaterialPropertyBlock _propertyBlock;
        /// <summary>
        /// The params object containing the settings to apply to the material.
        /// </summary>
        SpaceHDRISkySettings _hdriSkyParams;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="hdriSkyParams">Object containing the parameters used in the material.</param>
        public SpaceHDRISkyRenderer(SpaceHDRISkySettings hdriSkyParams)
        {
            _hdriSkyParams = hdriSkyParams;
            _propertyBlock = new MaterialPropertyBlock();
        }

        /// <summary>
        /// Called when effect is built.
        /// </summary>
        public override void Build()
        {
            _skyHDRIMaterial = CoreUtils.CreateEngineMaterial(_hdriSkyParams.spaceSkyboxShader);
        }

        /// <summary>
        /// Cleanup the material.
        /// </summary>
        public override void Cleanup()
        {
            CoreUtils.Destroy(_skyHDRIMaterial);
        }

        /// <summary>
        /// Sets the render target to use during rendering.
        /// </summary>
        /// <param name="builtinParams">Parameters for the render target.</param>
        public override void SetRenderTargets(BuiltinSkyParameters builtinParams)
        {
            if (builtinParams.depthBuffer == BuiltinSkyParameters.nullRT)
            {
                HDUtils.SetRenderTarget(builtinParams.commandBuffer, builtinParams.hdCamera, builtinParams.colorBuffer);
            }
            else
            {
                HDUtils.SetRenderTarget(builtinParams.commandBuffer, builtinParams.hdCamera, builtinParams.colorBuffer, builtinParams.depthBuffer);
            }
        }

        /// <summary>
        /// Called when sky is rendered.
        /// </summary>
        /// <param name="builtinParams">Rendering parameters.</param>
        /// <param name="renderForCubemap">True if rendering for a cubemap.</param>
        public override void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
        {
            _propertyBlock.SetTexture("_Cubemap", _hdriSkyParams.cubemap);
            _propertyBlock.SetTexture("_Stars", _hdriSkyParams.starsTexture);
            _propertyBlock.SetVector(HDShaderIDs._SkyParam, new Vector4(_hdriSkyParams.exposure, _hdriSkyParams.multiplier, -_hdriSkyParams.rotation, 0.0f)); // -rotation to match Legacy...

            // This matrix needs to be updated at the draw call frequency.
            _propertyBlock.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);

            CoreUtils.DrawFullScreen(builtinParams.commandBuffer, _skyHDRIMaterial, _propertyBlock, renderForCubemap ? 0 : 1);
        }

        /// <summary>
        /// Determines if this instance is valid.
        /// </summary>
        /// <returns>True if valid.</returns>
        public override bool IsValid()
        {
            return _hdriSkyParams != null && _skyHDRIMaterial != null;
        }
    }
}
