using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[System.Serializable]
[ExecuteInEditMode]
public class MaterialPropertyHandler : MonoBehaviour
{
    public Texture arMap;
    public Texture pmdgMap;
    public Texture noMap;

    [ColorUsage(true, true)]
    public Color glowColor;

    [ColorUsage(true, true)]
    public Color material1DiffuseColor;
    [ColorUsage(true, true)]
    public Color material1DustDiffuseColor;
    [ColorUsage(true, true)]
    public Color material1FresnelColor;
    [Range(0.0f, 1.0f)]
    public float material1Gloss;

    [ColorUsage(true, true)]
    public Color material2DiffuseColor;
    [ColorUsage(true, true)]
    public Color material2DustDiffuseColor;
    [ColorUsage(true, true)]
    public Color material2FresnelColor;
    [Range(0.0f, 1.0f)]
    public float material2Gloss;

    [ColorUsage(true, true)]
    public Color material3DiffuseColor;
    [ColorUsage(true, true)]
    public Color material3DustDiffuseColor;
    [ColorUsage(true, true)]
    public Color material3FresnelColor;
    [Range(0.0f, 1.0f)]
    public float material3Gloss;

    [ColorUsage(true, true)]
    public Color material4DiffuseColor;
    [ColorUsage(true, true)]
    public Color material4DustDiffuseColor;
    [ColorUsage(true, true)]
    public Color material4FresnelColor;
    [Range(0.0f, 1.0f)]
    public float material4Gloss;

    [Range(-2.0f, 0.7f)]
    public float dirtAmount;

    public Texture detailMap;

    [SerializeField]
    private MaterialPropertyBlock _materialProperties;

    private void Awake()
    {
        _materialProperties = new MaterialPropertyBlock();
    }

    void Update()
    {
        if (_materialProperties == null)
            _materialProperties = new MaterialPropertyBlock();

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.GetPropertyBlock(_materialProperties);

        _materialProperties.SetTexture("Texture2D_44DAD30F", arMap);
        _materialProperties.SetTexture("Texture2D_F03C4C98", pmdgMap);
        _materialProperties.SetTexture("Texture2D_DA3ECFEC", noMap);

        _materialProperties.SetColor("Color_744D7407", glowColor);

        _materialProperties.SetColor("Color_BFD52AD7", material1DiffuseColor);
        _materialProperties.SetColor("Color_2CA24DA6", material1FresnelColor);
        _materialProperties.SetColor("Color_E6EA7C7", material1DustDiffuseColor);
        _materialProperties.SetFloat("Vector1_B877A037", material1Gloss);

        _materialProperties.SetColor("Color_ECBBCD29", material2DiffuseColor);
        _materialProperties.SetColor("Color_C4702C9", material2FresnelColor);
        _materialProperties.SetColor("Color_9AF46BD0", material2DustDiffuseColor);
        _materialProperties.SetFloat("Vector1_6168D241", material2Gloss);

        _materialProperties.SetColor("Color_837882C3", material3DiffuseColor);
        _materialProperties.SetColor("Color_E2E0254A", material3FresnelColor);
        _materialProperties.SetColor("Color_3EC09000", material3DustDiffuseColor);
        _materialProperties.SetFloat("Vector1_AE7F99E0", material3Gloss);

        _materialProperties.SetColor("Color_433325BD", material4DiffuseColor);
        _materialProperties.SetColor("Color_34432DEA", material4FresnelColor);
        _materialProperties.SetColor("Color_32F3C3FF", material4DustDiffuseColor);
        _materialProperties.SetFloat("Vector1_EC88DF46", material4Gloss);

        _materialProperties.SetFloat("Vector1_5589174F", dirtAmount);

        if (detailMap != null)
            _materialProperties.SetTexture("Texture2D_6DDBBC03", detailMap);

        meshRenderer.SetPropertyBlock(_materialProperties);
	}
}
