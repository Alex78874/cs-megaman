using System.Numerics;
using Raylib_cs;
using Newtonsoft.Json;

public class Map
{
    public Tile[,] blocks;
    public int rows;
    public int cols;
    public int blockSize;

    int[,] map = new int[10,10] {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12 }
    };

    public Map(int rows, int cols, int blockSize)
    {
        this.rows = rows;
        this.cols = cols;
        this.blockSize = blockSize;

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
                }
            }
        }
    }

    public Map ConvertJsonFileToMap(string jsonFile)
    {
        // Read the JSON file
        string json = File.ReadAllText(jsonFile);
        // Check if the JSON string is null or empty
        // Return an empty Map object if it is
        if (string.IsNullOrEmpty(json)) {
            return new Map(0, 0, 0);
        }
        // Deserialize the JSON file
        Map map = JsonConvert.DeserializeObject<Map>(json, new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore
        }) ?? new Map(0, 0, 0);
        
        return map;
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