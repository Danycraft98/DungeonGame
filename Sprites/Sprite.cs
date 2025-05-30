using System;
using System.Collections.Generic;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public abstract class Sprite
{
    #region Fields
    protected float Speed = 2f;

    protected Texture2D _texture;

    protected Vector2 _position;

    public Vector2 velocity;

    #endregion

    public Rectangle Rectangle
    {
        get
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
    }

    public abstract void Draw(SpriteBatch spriteBatch);

    public abstract void Update(GameTime gameTime, List<Sprite> sprites, MapManager mapManager);

    #region Colloision
    protected bool IsTouchingLeft(Sprite sprite)
    {
        return Rectangle.Right + velocity.X > sprite.Rectangle.Left &&
            Rectangle.Left < sprite.Rectangle.Left &&
            Rectangle.Bottom > sprite.Rectangle.Top &&
            Rectangle.Top < sprite.Rectangle.Bottom;
    }

    protected bool IsTouchingRight(Sprite sprite)
    {
        return Rectangle.Left + velocity.X < sprite.Rectangle.Right &&
            Rectangle.Right > sprite.Rectangle.Right &&
            Rectangle.Bottom > sprite.Rectangle.Top &&
            Rectangle.Top < sprite.Rectangle.Bottom;
    }

    protected bool IsTouchingTop(Sprite sprite)
    {
        return Rectangle.Bottom + velocity.Y > sprite.Rectangle.Top &&
            Rectangle.Top < sprite.Rectangle.Top &&
            Rectangle.Right > sprite.Rectangle.Left &&
            Rectangle.Left < sprite.Rectangle.Right;
    }

    protected bool IsTouchingBottom(Sprite sprite)
    {
        return Rectangle.Top + velocity.Y < sprite.Rectangle.Bottom &&
            Rectangle.Bottom > sprite.Rectangle.Bottom &&
            Rectangle.Right > sprite.Rectangle.Left &&
            Rectangle.Left < sprite.Rectangle.Right;
    }

    protected void CheckCollision(List<Sprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            if (sprite == this)
                continue;

            if ((velocity.X > 0 && IsTouchingLeft(sprite)) ||
                (velocity.X < 0 & IsTouchingRight(sprite)))
                velocity.X = 0;

            if ((velocity.Y > 0 && IsTouchingTop(sprite)) ||
                (velocity.Y < 0 & IsTouchingBottom(sprite)))
                velocity.Y = 0;
        }
    }
    
    public void CheckCollision(MapManager mapManager) {
        Vector2 coord = new((int)Math.Round((_position.X + 24) / 64) + mapManager.location.X - 10, (int)Math.Round((_position.Y + 24) / 64) + mapManager.location.Y - 7);
        if (_position.X % 64 == 0 && ((velocity.X > 0 && int.Parse(mapManager.GetMapCoord((int) coord.X + 1, (int) coord.Y)) < 17) ||
                (velocity.X < 0 && int.Parse(mapManager.GetMapCoord((int) coord.X - 1, (int) coord.Y)) < 17)))
            velocity.X = 0;

        if (_position.Y % 64 == 0 && ((velocity.Y > 0 && int.Parse(mapManager.GetMapCoord((int) coord.X, (int) coord.Y + 1)) < 17) ||
                (velocity.Y < 0 && int.Parse(mapManager.GetMapCoord((int) coord.X, (int) coord.Y - 1)) < 17)))
            velocity.Y = 0;
    }

    #endregion
}