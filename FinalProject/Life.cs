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
    //Class in charge of the execute the methods for the Extra life
    class Life:BasicAnimatedSprite
    {
        //Inherited from Animated Sprite

        //Attributtes
        //---------------------------------
        bool colisionAbajo = false;     // Boolean to check if the sprite collides from the below area
        Random rand = new Random();     // Instantiating a random objct
        int positionX;                          // Variable x, used to locate the sprite at this number
       
        //Constructor
        //-------------------
        public Life(GraphicsDevice graphicsDevice):base(new Rectangle(300, 100, 20, 20), false, 1, 3, 529, 480, 0.4f)
        {
             positionX = rand.Next(0, graphicsDevice.Viewport.Width - 20); //Generate a random 

            pos.X = positionX;  // Locate the sprite in the x-axis at the value of X

        }

        //Methods
        //---------------------------
        public override void Update(GameTime gameTime)
        {
            // Simulating gravity the obejct is getting down always 
            if (colisionAbajo)
            {
                pos.Y+=1;
            }
            colisionAbajo = true;

            base.Update(gameTime);
        }

        //Collision between life and ground
        public void Collision(Ground ground)
        {
            
            if (pos.Intersects(ground.Getpos))
            {
                Rectangle tempBox;
                // checar colision de abajo
                tempBox = pos; tempBox.Height -= 10;
                if (!tempBox.Intersects(ground.Getpos))
                {
                    colisionAbajo = false;
                }

                
            }

        }

        //Collision between life and Tank
        public void Collision(Tank tanque)
        {
            if (tanque.Pos.Intersects(pos))
            {
                this.alive = false;
            }
        }
    }
}
