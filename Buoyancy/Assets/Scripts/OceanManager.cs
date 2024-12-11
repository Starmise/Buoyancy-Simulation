using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float wavesHeight = 0.5f;
    public float wavesFrequency = 1f;
    public float wavesSpeed = 1f;
    public GameObject ocean;
    Material oceanMat;
    Texture2D wavesDisplacement;

    void Start()
    {
        SetVariables();
    }

    void SetVariables()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)oceanMat.GetTexture("_WavesDisplacement");
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.transform.position.y +
               wavesDisplacement.GetPixelBilinear(position.x * wavesFrequency, position.z * wavesFrequency + Time.time * wavesSpeed).g * wavesHeight * ocean.transform.localScale.x;
    }

    void OnValidate()
    {
        if (oceanMat)
        {
            SetVariables();
            UpdateMaterial();
        }
    }

    void UpdateMaterial()
    {
        oceanMat.SetFloat("_WavesFrequency", wavesFrequency);
        oceanMat.SetFloat("_WavesSpeed", wavesSpeed);
        oceanMat.SetFloat("_WavesHeight", wavesHeight);
    }
}
