using System.IO;
using System.Linq;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DungeonGame.Sprites;


namespace DungeonGame.Managers;

public class MapManager {

    public int scale = 4;

    private Dictionary<Vector2, Texture2D> mapDict;

    private ContentManager _content;

    public Vector2 location = new(30, 34);

    public MapManager(string path, ContentManager content) {
        mapDict = [];
        _content = content;
        StreamReader reader = new(path);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null) {
            string[] row = line.Split(",");
            for (int x = 0; x < row.Length; x++) {
                if (row[x] != "00") {
                    Texture2D texture = _content.Load<Texture2D>("2 Dungeon Tileset/1 Tiles/Tile_" + row[x]);
                    mapDict[new Vector2(x, y)] = texture;
                }
            }
            y++;
        }
    }

    public Texture2D GetMapCoord (int x, int y) {
        return mapDict[new Vector2(x, y)];
    }

    public string GetTileType(int x, int y) {
        Texture2D tile = GetMapCoord(x, y);
        return tile.Name.Split("_").Last();
    }

    public Rectangle GetTileRect(int x, int y) {
        Texture2D tile = GetMapCoord(x, y);
        return new Rectangle((int) (x - location.X + 10) * tile.Width * scale, (int) (y - location.Y + 7) * tile.Height * scale, tile.Width*scale, tile.Height*scale);
    }

    public Dictionary<Vector2, Texture2D> LoadMap() {
        Dictionary<Vector2, Texture2D> result = new();
        foreach (var kvp in mapDict) {
            Vector2 position = new Vector2((kvp.Key.X - location.X + 10) * kvp.Value.Width * scale, (kvp.Key.Y - location.Y + 7) * kvp.Value.Height * scale);
            result[position] = kvp.Value;
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