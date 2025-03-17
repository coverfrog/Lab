using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public enum TerrainResolution
{
    _33,
    _65,
    _129,
    _257,
    _513,
    _1025,
    _2049,
    _4097,
}

public abstract class GisReader : MonoBehaviour
{
    [Header("Start")] 
    [SerializeField] protected bool mIsStartRun;
    
    [Header("File")] 
    [SerializeField] protected string mFileName = "36608009";
    
    [Header("Terrain Size")] 
    [SerializeField] [Min(10)] protected float mTerrainYLen = 10.0f;
    [SerializeField] [Min(10)] protected float mTerrainXLen = 512.0f;
    [SerializeField] [Min(10)] protected float mTerrainZLen = 512.0f;
    
    [Header("Terrain Resolution")] 
    [SerializeField] protected TerrainResolution mTerrainHeightMapResolution = TerrainResolution._513;
    
    [Header("Tool")] 
    [SerializeField] [Min(3)] protected int mIterationCount = 5;
    
    [Header("Reference")] 
    [SerializeField] protected Terrain mTerrain;

    protected static void RunWithStopWatch(ref Stopwatch stopwatch, Action action, string subject)
    {
        stopwatch.Stop();
        stopwatch.Reset();
        stopwatch.Start();
        
        action?.Invoke();
        
        stopwatch.Stop();
        
        Debug.Log($"[Gis] {subject}: {stopwatch.ElapsedMilliseconds * (1.0f / 1000.0f)} sec");
    }

    protected static int ToInt(TerrainResolution terrainResolution) => terrainResolution switch
    {
        TerrainResolution._33 => 33,
        TerrainResolution._65 => 65,
        TerrainResolution._129 => 129,
        TerrainResolution._257 => 257,
        TerrainResolution._513 => 513,
        TerrainResolution._1025 => 1025,
        TerrainResolution._2049 => 2049,
        TerrainResolution._4097 => 4097,
        _ => throw new ArgumentOutOfRangeException(nameof(terrainResolution), terrainResolution, null)
    };

    protected static void FillEmptyHeightMap(ref float[,] heightMap, TerrainResolution terrainResolution)
    {
        var resolution = ToInt(terrainResolution);
        
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                if (heightMap[y, x] == 0)
                {
                    heightMap[y, x] = (
                        heightMap[Mathf.Max(y - 1, 0), x] +
                        heightMap[Mathf.Min(y + 1, resolution - 1), x] +
                        heightMap[y, Mathf.Max(x - 1, 0)] +
                        heightMap[y, Mathf.Min(x + 1, resolution - 1)]
                    ) / 4.0f;
                }
            }
        }
    }
    
    protected static void SmoothHeightMapAll(ref float[,] heightMap, TerrainResolution terrainResolution, int iterations = 1)
    {
        var resolution = ToInt(terrainResolution);
            
        for (int iter = 0; iter < iterations; iter++) 
        {
            float[,] tempMap = new float[resolution, resolution];

            for (int y = 1; y < resolution - 1; y++)
            {
                for (int x = 1; x < resolution - 1; x++)
                {
                    tempMap[y, x] = (
                        heightMap[y, x] +
                        heightMap[y - 1, x] +
                        heightMap[y + 1, x] +
                        heightMap[y, x - 1] +
                        heightMap[y, x + 1]
                    ) / 5.0f; 
                }
            }

            for (int y = 1; y < resolution - 1; y++)
            {
                for (int x = 1; x < resolution - 1; x++)
                {
                    heightMap[y, x] = tempMap[y, x];
                }
            }
        }
    }

    protected static void SmoothTerrainEdges(ref float[,] heightMap, TerrainResolution terrainResolution)
    {
        var resolution = ToInt(terrainResolution);
        
        for (int x = 1; x < resolution - 1; x++)
        {
            heightMap[0, x] = (heightMap[1, x] + heightMap[2, x]) / 2.0f; 
            heightMap[resolution - 1, x] = (heightMap[resolution - 2, x] + heightMap[resolution - 3, x]) / 2.0f;
        }

        for (int y = 1; y < resolution - 1; y++)
        {
            heightMap[y, 0] = (heightMap[y, 1] + heightMap[y, 2]) / 2.0f;
            heightMap[y, resolution - 1] = (heightMap[y, resolution - 2] + heightMap[y, resolution - 3]) / 2.0f; 
        }
        
        for (int x = 0; x < resolution; x++)
        {
            if (heightMap[0, x] == 0) heightMap[0, x] = heightMap[1, x]; 
            if (heightMap[resolution - 1, x] == 0) heightMap[resolution - 1, x] = heightMap[resolution - 2, x]; 
        }

        for (int y = 0; y < resolution; y++)
        {
            if (heightMap[y, 0] == 0) heightMap[y, 0] = heightMap[y, 1]; 
            if (heightMap[y, resolution - 1] == 0) heightMap[y, resolution - 1] = heightMap[y, resolution - 2]; 
        }
    }

    protected static void GenTerrain(ref Terrain terrain, ref float[,] heightMap, float xLen,  float yLen,  float zLen)
    {
        xLen = Mathf.Max(xLen, 10);
        yLen = Mathf.Max(yLen, 10);
        zLen = Mathf.Max(zLen, 10);
        
        var terrainData = terrain.terrainData;
        var heightmapResolution = heightMap.GetLength(0);
        
        terrainData.heightmapResolution = heightmapResolution;
        terrainData.size = new Vector3(xLen, yLen, zLen); 
        terrainData.SetHeights(0, 0, heightMap);
    }
}
