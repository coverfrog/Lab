using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using IxMilia.Dxf;
using IxMilia.Dxf.Entities;

public class NetDxfReaderV3 : GisReader
{
    private DxfFile _mFile;
    private List<Vector3> _mContourPoints;
    private Vector3 _mMaxPoint, _mMinPoint;
    private float[,] _mHeightMap;
    
    private void Start()
    {
        if (!mIsStartRun) return;
        
        Run();
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;
        
        Run();
    }

    private void Run()
    {
        var stopWatch = new Stopwatch();

        RunWithStopWatch(ref stopWatch, ReadFile, nameof(ReadFile));
        RunWithStopWatch(ref stopWatch, GetContourList, nameof(GetContourList));
        RunWithStopWatch(ref stopWatch, GetNormal, nameof(GetNormal));
        RunWithStopWatch(ref stopWatch, GetHeightMap, nameof(GetHeightMap));
        RunWithStopWatch(ref stopWatch, GetFillEmptyHeightMap, nameof(GetFillEmptyHeightMap));
        RunWithStopWatch(ref stopWatch, GetSmoothHeightMapAll, nameof(GetSmoothHeightMapAll));
        RunWithStopWatch(ref stopWatch, GetSmoothTerrainEdges, nameof(GetSmoothTerrainEdges));
        RunWithStopWatch(ref stopWatch, GenTerrain, nameof(GenTerrain));
    }

    private void ReadFile()
    {
        using var reader = new StreamReader(Path.Combine("Assets", "000_Test", "Data", $"{mFileName}.dxf"));
        
        _mFile = DxfFile.Load(reader.BaseStream);
    }

    private void GetContourList()
    {
        _mContourPoints = new List<Vector3>();
        
        var entities = _mFile.Entities;
        var cursor = Vector3.zero;
        
        foreach (var entity in entities)
        {
            switch (entity)
            {
                case DxfPolyline polyline:
                {
                    foreach (var vertex in polyline.Vertices)
                    {
                        AddContour(ref cursor, vertex.Location);
                    }

                    if (!polyline.IsClosed || _mContourPoints.Count <= 0)
                    {
                        continue;
                    }
                
                    AddContourFirst();
                    
                    break;
                }
                case DxfLwPolyline lxPolyline:
                {
                    foreach (var vertex in lxPolyline.Vertices)
                    {
                        AddContour(ref cursor, vertex, lxPolyline);
                    }

                    if (!lxPolyline.IsClosed || _mContourPoints.Count <= 0)
                    {
                        continue;
                    }

                    AddContourFirst();
                    
                    break;
                }
            }
        }
    }

    private void AddContourFirst()
    {
        _mContourPoints.Add(_mContourPoints[0]);
    }

    private void AddContour(ref Vector3 cursor, DxfPoint dxfPoint)
    {
        cursor.x = (float)dxfPoint.X;
        cursor.y = (float)dxfPoint.Y;
        cursor.z = (float)dxfPoint.Z;

        _mContourPoints.Add(cursor);
    }
    
    private void AddContour(ref Vector3 cursor, DxfLwPolylineVertex dxfVertex, DxfLwPolyline lwPolyline)
    {
        cursor.x = (float)dxfVertex.X;
        cursor.y = (float)dxfVertex.Y;
        cursor.z = (float)lwPolyline.Elevation;

        _mContourPoints.Add(cursor);
    }

    private void GetNormal()
    {
        _mMinPoint = new Vector3(
            _mContourPoints.Min(p => p.x),
            _mContourPoints.Min(p => p.y),
            _mContourPoints.Min(p => p.z));

        _mMaxPoint = new Vector3(
            _mContourPoints.Max(p => p.x),
            _mContourPoints.Max(p => p.y),
            _mContourPoints.Max(p => p.z));
    }

    private void GetHeightMap()
    {
        var resolution = ToInt(mTerrainHeightMapResolution);
        
        _mHeightMap = new float[resolution, resolution];

        var normal = _mMaxPoint - _mMinPoint;
        
        foreach (Vector3 point in _mContourPoints)
        {
            var xIndex = Mathf.Clamp(Mathf.RoundToInt((point.x - _mMinPoint.x) / normal.x * (resolution - 1)), 0, resolution - 1);
            var yIndex = Mathf.Clamp(Mathf.RoundToInt((point.y - _mMinPoint.y) / normal.y * (resolution - 1)), 0, resolution - 1);

            var normalizedHeight = Mathf.Clamp01((point.z - _mMinPoint.z) / normal.z);
            
            _mHeightMap[yIndex, xIndex] = normalizedHeight;
        }
    }

    private void GetFillEmptyHeightMap()
    {
        FillEmptyHeightMap(ref _mHeightMap, mTerrainHeightMapResolution);
    }

    private void GetSmoothHeightMapAll()
    {
        SmoothHeightMapAll(ref _mHeightMap, mTerrainHeightMapResolution, mIterationCount);
    }

    private void GetSmoothTerrainEdges()
    {
        SmoothTerrainEdges(ref _mHeightMap, mTerrainHeightMapResolution);
    }

    private void GenTerrain()
    {
        GenTerrain(ref mTerrain, ref _mHeightMap, mTerrainXLen, mTerrainYLen, mTerrainZLen);
    }
}
