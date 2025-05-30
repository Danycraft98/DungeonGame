using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Sprites;

namespace GameProject.States;

public class MenuState : State
{
    private List<Component> _components;
    private int centerWidth = 640;

    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
      : base(game, graphicsDevice, content) {
        var titleTexture = content.Load<Texture2D>("4 GUI/6 Logo/1");
        
        var buttonTexture = content.Load<Texture2D>("4 GUI/2 Buttons/Button0");
        var font = content.Load<SpriteFont>("4 GUI/Font");

        var title = new GUIBlock(titleTexture, font, 3, false)
        {
            Position = new Vector2((float)(centerWidth - titleTexture.Width * 1.5), 50),
            Text = "Game Title",
        };

        var newGameButton = new GUIBlock(buttonTexture, font, 2, true)
        {
            Position = new Vector2(centerWidth - buttonTexture.Width, 100 + titleTexture.Height * 3),
            Text = "New Game",
        };

        newGameButton.Click += NewGameButton_Click;

        var loadGameButton = new GUIBlock(buttonTexture, font, 2, true)
        {
            Position = new Vector2(centerWidth - buttonTexture.Width, 200 + titleTexture.Height * 3),
            Text = "Load Game",
        };

        loadGameButton.Click += LoadGameButton_Click;

        var quitGameButton = new GUIBlock(buttonTexture, font, 2, true)
        {
            Position = new Vector2(centerWidth - buttonTexture.Width, 300 + titleTexture.Height * 3),
            Text = "Quit Game",
        };

        quitGameButton.Click += QuitGameButton_Click;

        _components = new List<Component>()
            {
                title,
                newGameButton,
                loadGameButton,
                quitGameButton,
            };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Begin();

        foreach (var component in _components)
            component.Draw(gameTime, spriteBatch);

        spriteBatch.End();
    }

    private void LoadGameButton_Click(object sender, EventArgs e) {
        Console.WriteLine("Load Game");
    }

    private void NewGameButton_Click(object sender, EventArgs e) {
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
    }

    public override void PostUpdate(GameTime gameTime) {
      // remove sprites if they're not needed
    }

    public override void Update(GameTime gameTime) {
        foreach (var component in _components)
            component.Update(gameTime);
    }

    private void QuitGameButton_Click(object sender, EventArgs e) {
        _game.Exit();
    }
}