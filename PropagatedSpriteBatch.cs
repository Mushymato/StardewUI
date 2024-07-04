﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SupplyChain.UI;

/// <summary>
/// Sprite batch wrapper with transform propagation.
/// </summary>
public class PropagatedSpriteBatch(SpriteBatch spriteBatch, Transform transform) : ISpriteBatch
{
    private readonly SpriteBatch spriteBatch = spriteBatch;
    private Transform transform = transform;

    public void Draw(
        Texture2D texture,
        Vector2 position,
        Rectangle? sourceRectangle,
        Color? color = null,
        float rotation = 0,
        Vector2? origin = null,
        float scale = 1.0f,
        SpriteEffects effects = SpriteEffects.None,
        float layerDepth = 0)
    {
        spriteBatch.Draw(
            texture,
            position + transform.Translation,
            sourceRectangle,
            color ?? Color.White,
            rotation,
            origin ?? Vector2.Zero,
            scale,
            effects,
            layerDepth);
    }

    public void Draw(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle? sourceRectangle,
        Color? color = null,
        float rotation = 0,
        Vector2? origin = null,
        SpriteEffects effects = SpriteEffects.None,
        float layerDepth = 0)
    {
        var location = (destinationRectangle.Location.ToVector2() + transform.Translation).ToPoint();
        spriteBatch.Draw(
            texture,
            new(location, destinationRectangle.Size),
            sourceRectangle,
            color ?? Color.White,
            rotation,
            origin ?? Vector2.Zero,
            effects,
            layerDepth);
    }

    public void DrawString(
        SpriteFont spriteFont,
        string text,
        Vector2 position,
        Color color,
        float rotation = 0,
        Vector2? origin = null,
        float scale = 1,
        SpriteEffects effects = SpriteEffects.None,
        float layerDepth = 0)
    {
        spriteBatch.DrawString(
            spriteFont,
            text,
            position + transform.Translation,
            color,
            rotation,
            origin ?? Vector2.Zero,
            scale,
            effects,
            layerDepth);
    }

    public IDisposable SaveTransform()
    {
        return new TransformReverter(this);
    }

    public void Translate(float x, float y)
    {
        Translate(new(x, y));
    }

    public void Translate(Vector2 translation)
    {
        transform = transform.Translate(translation);
    }

    private class TransformReverter(PropagatedSpriteBatch owner) : IDisposable
    {
        private readonly Transform savedTransform = owner.transform;

        public void Dispose()
        {
            owner.transform = savedTransform;
            GC.SuppressFinalize(this);
        }
    }
}
