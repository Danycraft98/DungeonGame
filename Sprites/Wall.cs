using Vector2 = System.Numerics.Vector2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonGame.Models;
using DungeonGame.Managers;

namespace DungeonGame.Sprites;

public class Wall : Sprite {
   
    public Color Colour = Color.White;
    public Input Input;

    public Wall(Texture2D texture, Vector2 position)
    {
        _texture = texture;
        _position = position;
        _scale = 4;
    }

    public override void Update(GameTime gameTime, MapManager mapManager) {
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture,
                    Rectangle,
                    new Rectangle(0,
                                0,
                                _texture.Width,
                                _texture.Height),
                    Colour);
    }
}
