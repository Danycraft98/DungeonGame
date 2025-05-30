using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Sprites;

public class GUIBlock : Component
{
    #region Fields

    private MouseState _currentMouse;

    private SpriteFont _font;

    private bool _isHovering;

    private MouseState _previousMouse;

    private Texture2D _texture;

    private float _scale = 1;

    #endregion

    #region Properties

    public event EventHandler Click;

    private bool _interactive;

    public bool Clicked { get; private set; }

    public Color PenColour { get; set; }

    public Vector2 Position { get; set; }

    public Rectangle Rectangle {
        get
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)(_texture.Width * _scale), (int)(_texture.Height * _scale));
        }
    }

    public string Text { get; set; }

    #endregion

    #region Methods

    public GUIBlock(Texture2D texture, SpriteFont font, float scale, bool interactive) {
        _texture = texture;

        _scale = scale;

        _font = font;

        PenColour = Color.Black;

        _interactive = interactive;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        var colour = Color.White;

        if (_isHovering)
            colour = Color.Gray;

        spriteBatch.Draw(_texture, Rectangle, colour);

        if (!string.IsNullOrEmpty(Text))
        {
            var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
            var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

            spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
        }
    }

    public override void Update(GameTime gameTime) {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();

        var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

        _isHovering = false;

        if (_interactive && mouseRectangle.Intersects(Rectangle))
        {
            _isHovering = true;

            if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, new EventArgs());
            }
        }
    }

    #endregion
}