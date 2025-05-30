using System.Collections.Generic;
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Models;
using GameProject.Managers;

namespace GameProject.Sprites;

public class Wall : Sprite
{
    public Color Colour = Color.White;
    public Input Input;

    public Wall(Texture2D texture, Vector2 position) {
        _texture = texture;
        _position = position;
    }

    public override void Update(GameTime gameTime, List<Sprite> sprites, MapManager mapManager) {

    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, _position, Colour);
    }
}
