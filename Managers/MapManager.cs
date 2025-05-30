using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;
using DungeonGame.Sprites;


namespace DungeonGame.Managers;

public class MapManager {

    public float scale = 4;

    private Dictionary<Vector2, string> mapDict { get; set; }

    private ContentManager _content;

    private Vector2 dimension;

    public Vector2 location = new(32, 34);

    public MapManager(string path, ContentManager content)
    {
        mapDict = [];
        _content = content;
        int width = 0;
        StreamReader reader = new(path);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] row = line.Split(",");
            width = row.Length;
            for (int x = 0; x < row.Length; x++)
            {
                if (row[x] != "00")
                {
                    mapDict[new Vector2(x, y)] = row[x];
                }
            }
            y++;
        }
        dimension = new(width, y);
    }

    public string GetMapCoord (int x, int y) {
        return mapDict[new Vector2(x, y)];
    }

    public Dictionary<Vector2, Texture2D> LoadMap() {
        Dictionary<Vector2, Texture2D> result = new();
        foreach (var kvp in mapDict) {
            Texture2D texture = _content.Load<Texture2D>("2 Dungeon Tileset/1 Tiles/Tile_" + kvp.Value);
            result[new Vector2((kvp.Key.X - location.X + 10) * texture.Width * scale, (kvp.Key.Y - location.Y + 7) * texture.Height * scale)] = texture;
        }
        return result;
    }
    
    public void TransitionMap (Player player) {
        if (player.Position.X < 0) {
            player.Position = new(1280, player.Position.Y);
            location.X -= 20;
        } else if (player.Position.X > 1280) {
            player.Position = new(0, player.Position.Y);
            location.X += 20;
        }
        
        if (player.Position.Y < 0) {
            player.Position = new(player.Position.X, 900);
            location.Y -= 14;
        } else if (player.Position.Y > 900) {
            player.Position = new(player.Position.X, 0);
            location.Y += 14;
        }
    }
}