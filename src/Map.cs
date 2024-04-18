using System.Numerics;
using Raylib_cs;
using Newtonsoft.Json;

public class Map
{
    public Tile[,] blocks;
    public int rows;
    public int cols;
    public int blockSize = 32;

    public Map(int[,]? map = null)
    {
        if (map == null)
        {
            rows = 0;
            cols = 0;
            blocks = new Tile[0, 0];
            return;
        }

        rows = map.GetLength(0);
        cols = map.GetLength(1);

        blocks = new Tile[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] != 0)
                {
                    blocks[i, j] = new Tile(blockSize, map[i, j]);
                    blocks[i, j].position.X = j * blockSize;
                    blocks[i, j].position.Y = i * blockSize;
                    Program.tiles.Add(blocks[i, j]);
                }
            }
        }
    }

    public void MakeFloor()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // Create floor if block is at the bottom of the map
                if (i == rows - 1)
                {
                    blocks[i, j] = new Tile(blockSize, 14);
                    blocks[i, j].position.X = j * blockSize;
                    blocks[i, j].position.Y = i * blockSize;
                }
            }
        }
    }

    // Convert a JSON file to a map array
    // The JSON file must have a "stage" property with a 2D array of integers
    // if the JSON file is empty or invalid, it will return a Map object with 0 rows and 0 cols
    // if the JSON file is valid, it will return a Map object with the rows, cols and blocks
    public static int[,]? ConvertJsonFileToMap(string jsonFile)
    {
        string json = File.ReadAllText(jsonFile);
        int[,]? map = null;

        if (string.IsNullOrEmpty(json))
        {
            Console.WriteLine("Empty JSON file.");
            return null;
        }

        var mapData = JsonConvert.DeserializeObject<MapData>(json);
        if (mapData != null && mapData.stage != null)
        {
            int rows = mapData.stage.Length;
            int cols = mapData.stage[0].Length;
            map = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = mapData.stage[i][j];
                    Console.Write(map[i, j] + " ");
                }
            }
            Console.WriteLine("Map loaded successfully.");
        }
        else
        {
            Console.WriteLine("Invalid JSON file format.");
        }
        return map;
    }

    private class MapData
    {
        public int[][]? stage { get; set; }
    }

    public int[,] GetCurrentMap()
    {
        int[,] currentMap = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                currentMap[i, j] = blocks[i, j].type;
                Console.Write(currentMap[i, j] + " ");
            }
            Console.WriteLine();
        }

        return currentMap;
    }

    public void Update()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (blocks[i, j] != null)
                {
                    blocks[i, j].Update();
                }
            }
        }
    }

    public void Draw()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (blocks[i, j] != null)
                {
                    blocks[i, j].Draw();
                }
            }
        }
    }
}