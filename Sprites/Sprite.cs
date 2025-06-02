using System;
using Vector2 = System.Numerics.Vector2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonGame.Managers;


namespace DungeonGame.Sprites;

public abstract class Sprite
{
    #region Fields
    protected float Speed = 2.5f;

    protected float _scale;

    protected int _slide=1;

    protected Texture2D _texture;

    protected Vector2 _position;

    public Vector2 velocity;

    #endregion

    public Rectangle Rectangle {
        get {
            return new Rectangle((int)_position.X, (int)_position.Y, (int)(_texture.Width / _slide * _scale), (int)(_texture.Height * _scale));
        }
    }

    public abstract void Draw(SpriteBatch spriteBatch);

    public abstract void Update(GameTime gameTime, MapManager mapManager);

    #region Colloision
    protected bool IsTouchingLeft(Rectangle rectangle) {
        return Rectangle.Right + velocity.X > rectangle.Left &&
            Rectangle.Left < rectangle.Left &&
            Rectangle.Bottom > rectangle.Top &&
            Rectangle.Top < rectangle.Bottom;
    }

    protected bool IsTouchingRight(Rectangle rectangle) {
        return Rectangle.Left + velocity.X < rectangle.Right &&
            Rectangle.Right > rectangle.Right &&
            Rectangle.Bottom > rectangle.Top &&
            Rectangle.Top < rectangle.Bottom;
    }

    protected bool IsTouchingTop(Rectangle rectangle) {
        return Rectangle.Bottom + velocity.Y > rectangle.Top &&
            Rectangle.Top < rectangle.Top &&
            Rectangle.Right > rectangle.Left &&
            Rectangle.Left < rectangle.Right;
    }

    protected bool IsTouchingBottom(Rectangle rectangle) {
        return Rectangle.Top + velocity.Y < rectangle.Bottom &&
            Rectangle.Bottom > rectangle.Bottom &&
            Rectangle.Right > rectangle.Left &&
            Rectangle.Left < rectangle.Right;
    }

    public void CheckCollision(MapManager mapManager) {
        Vector2 coord = new((int)Math.Round((_position.X + _texture.Width / _slide * _scale / 2) / 64) + mapManager.location.X - 10, (int)Math.Round((_position.Y + _texture.Height * _scale / 2) / 64) + mapManager.location.Y - 7);
        Console.WriteLine("Hello, {0}.", coord);
        for (int i=-1; i<2; i++) {
            if (velocity.X > 0 && int.Parse(mapManager.GetTileType((int)coord.X, (int)coord.Y)) < 17) {
                Rectangle tileRect = mapManager.GetTileRect((int)coord.X, (int)coord.Y+i);
                if (IsTouchingLeft(tileRect)) {
                    velocity.X = 0;
                }
            } else if (velocity.X < 0 && int.Parse(mapManager.GetTileType((int)(coord.X - 1), (int)coord.Y)) < 17) {
                Rectangle tileRect = mapManager.GetTileRect((int)(coord.X - 1), (int)coord.Y+i);
                if (IsTouchingRight(tileRect)) {
                    velocity.X = 0;
                }
            }
            
            if (velocity.Y > 0 && int.Parse(mapManager.GetTileType((int)coord.X, (int)coord.Y)) < 17) {
                Rectangle tileRect = mapManager.GetTileRect((int)coord.X+i, (int)coord.Y);
                //Console.WriteLine("Hello, {0}, {1}.", IsTouchingTop(tileRect), i);
                if (IsTouchingTop(tileRect))
                {
                    velocity.Y = 0;
                }
            } else if (velocity.Y < 0 && int.Parse(mapManager.GetTileType((int)coord.X, (int)(coord.Y - 1))) < 17) {
                Rectangle tileRect = mapManager.GetTileRect((int)coord.X+i, (int)(coord.Y - 1));
                if (IsTouchingBottom(tileRect)) {
                    velocity.Y = 0;
                }
            }
        }
    }

    #endregion
}