using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class Label
    {
        SpriteFont text;
        String words;
        Vector2 textpos;
        Color color;
        
        public Label(SpriteFont Text, String Words, Vector2 Textpos, Color Color)
        {
            text = Text;
            words = Words;
            textpos = Textpos;
            color = Color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(text, words, textpos, color);
        }
    }
}
