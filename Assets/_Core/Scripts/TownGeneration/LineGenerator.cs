using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private int minLines;
    [SerializeField] private int maxLines;

    [SerializeField] private int minLenght;
    [SerializeField] private int maxLenght;
    [SerializeField] private int minDistanceBetweenLines;
    [SerializeField] private int maxCrossLine;
    [SerializeField] private List<int> angles;

    [SerializeField] private Material lineMaterial;
    [SerializeField] private float lineWidth;

    private List<LineData> linesData = new List<LineData>();

    class LineData
    {
        public Vector3[] positions;
        public Vector3 direction;
        public LineData parent;
        public int length;
        public int angle;
        public LineRenderer lineRenderer;
    }

    private void Start()
    {
        Generate(Vector3.zero);
    }

    public void Generate(Vector3 position)
    {
        DeleteLines();
        int lines = Random.Range(minLines, maxLines);
        for (int index = 0; index < lines; index++)
        {
            LineData data = GenerateLine();
            int maxTryCount = 1000;
            while (!VerifyLine(data))
            {
                maxTryCount--;
                if (maxTryCount < 0)
                    throw new Exception("Endless cicle");
                data = GenerateLine();
            }

            Debug.Log($"Created line with length {data.length}");
            CreateLine(data,index);
        }
    }

    LineData GenerateLine()
    {
        LineData parent = null;
        if (linesData.Count == 0)
        {
            parent = new LineData()
            {
                angle = 90,
                direction = Vector3.forward,
                length = Random.Range(minLenght, maxLenght),
                positions = new Vector3[] {Vector3.zero, Vector3.one, Vector3.one * 2}
            };
        }
        else
        {
            parent = linesData[Random.Range(0, linesData.Count)];
        }
        var startPosition = parent.positions[Random.Range(0,parent.positions.Length)];
        int angle = GetRandomAngel(parent);
        int totalLength = Random.Range(minLenght, maxLenght);
        int offset = Random.Range(0, totalLength);
        int otherLength = totalLength - offset;
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * parent.direction;
        Vector3[] positions = new Vector3[totalLength];
        Vector3 endPosition = startPosition + direction * otherLength;
        startPosition = startPosition + -direction * offset;

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Vector3.Lerp(startPosition, endPosition, i / (float)positions.Length);
        }
            // //Init positions before offset
            // for (int i = offset; i >= 0; i--)
            // {
            //     positions[i] = startPosition + (-direction * (1 * i));
            // }
            //
            // for (int i = offset; i < positions.Length; i++)
            // {
            //     positions[i] = startPosition + (direction * (1 * i));
            // }
        

        LineData data = new LineData()
        {
            angle = angle,
            direction = direction,
            length = totalLength,
            positions = positions,
            parent = parent
        };
        return data;
    }

    bool VerifyLine(LineData data)
    {
        if (data.parent == null) return true;
        foreach (var position in data.positions)
        {
            int cross = 0;
            foreach (var otherData in linesData)
            {
                if (otherData == data.parent) continue;
                foreach (var otherPosition in otherData.positions)
                {
                    if (Vector3.Distance(position, otherPosition) < minDistanceBetweenLines)
                    {
                        cross++;
                        break;
                    }
                }

                if (cross > maxCrossLine) return false;
            }
        }
        return true;
    }

    int GetRandomAngel(LineData currentParent)
    {
        var availableAngles = new List<int>(angles);
        availableAngles.Remove(currentParent.angle);
        int multiplier = Random.Range(-1,1) < 0 ? -1 : 1;
        return availableAngles[Random.Range(0, 1)] * multiplier;
    }
    
    

    void DeleteLines()
    {
        foreach (var lineRenderer in linesData)
        {
            if(lineRenderer!= null)
                DestroyImmediate(lineRenderer.lineRenderer.gameObject);
        }
        linesData.Clear();
    }

    void CreateLine(LineData data,int index)
    {
        var go = new GameObject($"Line_{index}");
        var line = go.AddComponent<LineRenderer>();

        for (int i = 0; i < data.positions.Length; i++)
            data.positions[i] = GetPositionOnTerrain(data.positions[i]);

        line.positionCount = data.positions.Length;
        line.SetPositions(data.positions);

        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.startColor = Color.red;
        line.endColor = Color.green;
        line.material = lineMaterial;
        data.lineRenderer = line;
        linesData.Add(data);

    }

    Vector3 GetPositionOnTerrain(Vector3 origin)
    {
        var ray = new Ray(origin + Vector3.up * 100, Vector3.down);
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            return hit.point;
        }
        return origin;
    }
}
