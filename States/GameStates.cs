using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonGame.Models;
using DungeonGame.Sprites;
using DungeonGame.Managers;


namespace DungeonGame.States;

public class GameState : State {

    public List<Sprite> _sprites;

    private Player player;

    private MapManager mapManager;

    public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content) {
        player = new Player(new Dictionary<string, Animation>() {
            { "WalkUp", new Animation(content.Load<Texture2D>("1 Characters/2/U_Walk"), 6) },
            { "WalkDown", new Animation(content.Load<Texture2D>("1 Characters/2/D_Walk"), 6) },
            { "WalkLeft", new Animation(content.Load<Texture2D>("1 Characters/2/L_Walk"), 6) },
            { "WalkRight", new Animation(content.Load<Texture2D>("1 Characters/2/R_Walk"), 6) },
        }, 6) {
            Position = new Vector2(660, 370),
            Input = new Input()
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
            },
        };

        _sprites = [player];

        mapManager = new("../../../Data/map.txt", _content);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Begin();
        
        foreach (var item in mapManager.LoadMap()) {
            spriteBatch.Draw(item.Value,
                             //mapManager.GetTileRect((int)item.Key.X, (int)item.Key.Y),
                             new Rectangle((int)item.Key.X, (int)item.Key.Y, item.Value.Width * mapManager.scale, item.Value.Height * mapManager.scale),
                             new Rectangle(0, 0, item.Value.Width, item.Value.Height), Color.White);
        }

        player.Draw(spriteBatch);
        mapManager.TransitionMap(player);

        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime) {
    }

    public override void Update(GameTime gameTime) {
        foreach (var sprite in _sprites)
            sprite.Update(gameTime, mapManager);
    }
}