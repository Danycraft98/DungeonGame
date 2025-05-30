using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonGame.Models;
using DungeonGame.Sprites;
using DungeonGame.Managers;


namespace DungeonGame.States;

public class GameState : State
{

    public List<Sprite> _sprites;

    private Player player;

    private MapManager mapManager;

    public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
        player = new Player(new Dictionary<string, Animation>() {
            { "WalkUp", new Animation(content.Load<Texture2D>("1 Characters/2/U_Walk"), 6) },
            { "WalkDown", new Animation(content.Load<Texture2D>("1 Characters/2/D_Walk"), 6) },
            { "WalkLeft", new Animation(content.Load<Texture2D>("1 Characters/2/L_Walk"), 6) },
            { "WalkRight", new Animation(content.Load<Texture2D>("1 Characters/2/R_Walk"), 6) },
        }) {
            Position = new Vector2(640, 370),
            Input = new Input()
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
            },
        };

        _sprites = new List<Sprite> { player };

        mapManager = new("../../../Data/map.txt", _content);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Begin();
        mapManager.LoadMap();
        
        foreach (var item in mapManager.Floors)
        {
            spriteBatch.Draw(item.Value,
                             new Rectangle((int)item.Key.X, (int)item.Key.Y, item.Value.Width * (int)mapManager.scale, item.Value.Height * (int)mapManager.scale),
                             new Rectangle(0, 0, item.Value.Width, item.Value.Height), Color.White);
        }

        foreach (var item in mapManager.Walls) {
            item.Draw(spriteBatch);
        }

        player.Draw(spriteBatch);
        mapManager.TransitionMap(player);

        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime) {
    }

    public override void Update(GameTime gameTime) {
        
        foreach (var sprite in _sprites)
            sprite.Update(gameTime, _sprites, mapManager);
    }
}