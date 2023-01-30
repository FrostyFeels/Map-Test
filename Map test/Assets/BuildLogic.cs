using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildLogic
{
    public static void ChangeAlpha(float colorIndex, Renderer renderer)
    {
        Color color = renderer.material.color;
        color.a = colorIndex;

        renderer.material.color = color;
    } 
    public static void ChangeColor(Color color, Renderer render)
    {
        render.material.color = color;
    }

    public static TileStats OnTileSelect(int layers)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layers))
        {
            return hit.collider.GetComponent<TileStats>();
        }
        else
            return null;
    }
    public static Vector3[] GetStart(Vector3 start, Vector3 end)
    {
        Vector3 realStart;
        Vector3 realEnd;

        if (start.x > end.x)
        {
            realStart.x = end.x;
            realEnd.x = start.x;
        }
        else
        {
            realStart.x = start.x;
            realEnd.x = end.x;
        }

        if (start.z > end.z)
        {
            realStart.z = end.z;
            realEnd.z = start.z;
        }
        else
        {
            realStart.z = start.z;
            realEnd.z = end.z;
        }

        if (start.y > end.y)
        {
            realStart.y = end.y;
            realEnd.y = start.y;
        }
        else
        {
            realStart.y = start.y;
            realEnd.y = end.y;
        }


        Vector3[] pos = new Vector3[2];
        pos[0] = realStart;
        pos[1] = realEnd;

        return pos;
    }
    public static List<Tile> GetEdges(Vector3 start, Vector3 end, Tile[,,] list, int height)
    {
        List<Tile> dataList = new List<Tile>();
        Tile data;
        for (int z = (int)start.z; z <= end.z; z++)
        {
            for (int x = (int)start.x; x <= end.x; x++)
            {
                Debug.Log("x " + x + " y " + height + " z " + z);
                data = list[x, height, z];
                if (((x == start.x || x == end.x) || (z == start.z || z == end.z)))
                {
                    dataList.Add(data);
                }
            }
        }

        return dataList;
    }
    public static List<Tile> GetMiddle(Vector3 start, Vector3 end, Tile[,,] list, int height)
    {
        List<Tile> dataList = new List<Tile>();
        Tile data;
        for (int y = (int)start.z; y <= end.z; y++)
        {
            for (int x = (int)start.x; x <= end.x; x++)
            {
                data = list[x, height, y];
                if (((x != start.x && x != end.x) && (y != start.z && y != end.z)))
                {
                    dataList.Add(data);
                }

            }
        }

        return dataList;
    }
}
