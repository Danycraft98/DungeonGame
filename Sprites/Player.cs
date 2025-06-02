using System;
using System.Linq;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonGame.Managers;
using DungeonGame.Models;

namespace DungeonGame.Sprites;

public class Player : Sprite
{
    #region Fields

    protected AnimationManager _animationManager;

    protected Dictionary<string, Animation> _animations;

    #endregion

    #region Properties
    
    public Input Input;

    public Vector2 Position {
        get { return _position; }
        set {
            _position = value;

            if (_animationManager != null)
                _animationManager.Position = _position;
        }
    }

    #endregion

    #region Methods

    public override void Draw(SpriteBatch spriteBatch) {
        if (_animationManager != null)
            _animationManager.Draw(spriteBatch, _scale);
        else if (_texture != null)
            spriteBatch.Draw(_texture, Position, Color.White);
        else throw new Exception("This ain't right..!");
    }

    public virtual void Move() {
        if (Keyboard.GetState().IsKeyDown(Input.Up))
            velocity.Y = -Speed;
        else if (Keyboard.GetState().IsKeyDown(Input.Down))
            velocity.Y = Speed;
        else if (Keyboard.GetState().IsKeyDown(Input.Left))
            velocity.X = -Speed;
        else if (Keyboard.GetState().IsKeyDown(Input.Right))
            velocity.X = Speed;
    }

    protected virtual void SetAnimations() {
        if (velocity.X > 0)
            _animationManager.Play(_animations["WalkRight"]);
        else if (velocity.X < 0)
            _animationManager.Play(_animations["WalkLeft"]);
        else if (velocity.Y > 0)
            _animationManager.Play(_animations["WalkDown"]);
        else if (velocity.Y < 0)
            _animationManager.Play(_animations["WalkUp"]);
        else _animationManager.Stop();
    }

    public Player(Dictionary<string, Animation> animations, int slide) {
        _texture = animations.First().Value.Texture;
        _animations = animations;
        _animationManager = new AnimationManager(_animations.First().Value);
        _scale=3;
        _slide = slide;
    }

    public Player(Texture2D texture) {
        _texture = texture;
        _scale=3;
    }

    public override void Update(GameTime gameTime, MapManager mapManager) {
        Move();
        CheckCollision(mapManager);

        SetAnimations();

        _animationManager.Update(gameTime);

        Position += velocity;
        velocity = Vector2.Zero;
    }

    #endregion
}