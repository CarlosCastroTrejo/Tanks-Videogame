using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;


namespace FinalProject
{
    // Class in charge of just animate an AnimatedSprite once
    class SingleShot:BasicAnimatedSprite
    {
        // Attributes
        //------------------------------------------------
        bool parar = false; //Boolean to know where the animation will stop
        int contador = 0;  // Variable to count the frames that have passed
        int countFrameX;   // Variable to know how many frames the animation has

        //Constructor
        //--------------------------------------
        public SingleShot(Rectangle pos, bool _2D, int countFrameX, int countFrameY, int frameWidht, int frameHeight, float timePerFrame) : base(pos, _2D, countFrameX, countFrameY, frameWidht, frameHeight, timePerFrame)
        {
            this.countFrameX = countFrameX;
        }

        //Methods
        //------------------------------------------------
        public override void Update(GameTime gameTime)
        {
            pos.Width = 30;
            pos.Height = 30;
            contador++;
            if (!parar)
            {
                base.Update(gameTime);
                if (contador == countFrameX) // If the counter is equals the number of frames the sprite has
                {
                    parar = true;  // the Animation must stop
                    this.GetAlive = false; // the alive attribute will be false
                }
            }

        }
    }
}
