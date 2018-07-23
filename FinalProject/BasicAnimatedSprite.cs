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
    //Class in charge of the method of the Animated Sprite (Derived from BasicSprite)
    class BasicAnimatedSprite:BasicSprite
    {

        // Attributes 
        //------------------------------------------
        int countFrameX, countFrameY;   // Total number of frames(either multiple or single file)
        int frameWidth, frameHeight;    // Width and Height of single frame
        int currentFrame;               // Current frame in which the Animated Sprite is at the running of the software
        float timePerFrame;             // Amounnt of frames at which the sprite will be running per second
        float timer;                    // Time since the program is initialized
        bool _2D;                       // Boolean to indicate if the the Animated Sprite is from a 2D sheet
        bool multipleFiles;             // Boolean to indicate if the Animated Sprite is of differente files
        ArrayList imagenes = new ArrayList();

        //Constructors
        //-------------------------------------------------
        //Single Files
        public BasicAnimatedSprite(Rectangle pos, bool _2D, int countFrameX, int countFrameY, int frameWidth, int frameHeight, float timePerFrame) : base(pos)
        {
            this._2D = _2D;
            this.countFrameX = countFrameX;
            this.countFrameY = countFrameY;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.timePerFrame = timePerFrame;

            multipleFiles = false;
            timer = 0;
            currentFrame = 0;
        }
        //Multiple Files
        public BasicAnimatedSprite(Rectangle pos, int countFrameX, float timePerFrame) : base(pos)
        {
            this.countFrameX = countFrameX;
            this.timePerFrame = timePerFrame;
            multipleFiles = true;
            timer = 0;
            currentFrame = 0;
            countFrameY = 1;
        }


        //Methods
        //------------------------------------------------
        // SINGLE LoadContent method - Class internally takes care of loading content appropriately
        public override void LoadContent(ContentManager Content, string textura)
        {
            //Multiple Files
            if (multipleFiles)
            {
                for (int x = 1; x <= countFrameX; x++)
                {
                    base.LoadContent(Content, textura + x.ToString("00"));
                    imagenes.Add(texture);
                }
            }
            //Single Files
            else
            {
                base.LoadContent(Content, textura);
            }
        }

        //Update - to update the current frame of the Animated Sprite 
        public virtual void Update(GameTime gameTime)
        {
            if (alive)
            {
                timer = timer + (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= timePerFrame)
                {
                    currentFrame = (currentFrame + 1) % (countFrameX * countFrameY);
                    timer = timer - timePerFrame;
                }
            }
          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Multiple files
            if (multipleFiles)
            {
                texture = (Texture2D)imagenes[currentFrame];
            }
            //Single Files
            else
            {
                int currentX, currentY;
                if (_2D)
                {
                    currentX = currentFrame % countFrameX;
                    source.X = currentX * frameWidth;
                    currentY = currentFrame / countFrameY;
                    source.Y = currentY * frameHeight;
                }
                else
                {
                    source.X = currentFrame * frameWidth;
                    source.Y = 0;
                }
                source.Width = frameWidth;
                source.Height = frameHeight;
            }
            base.Draw(spriteBatch);
        }
    }



}

