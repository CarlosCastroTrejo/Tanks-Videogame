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

    //Class in charge of displaying the total ground of the whole game
    class TotalGround
    {
        
        Ground piso;    // Initialization of an object of ground
        ArrayList pisoTotal = new ArrayList();      // Array to add the total ground objects
        Random random=new Random();             // Initialization of a random object
        int elevation;                          // Variable to indicate at which distance there will be another elevation in the ground of the game

        //Constructor
        //-------------------------------------------------------------
        public TotalGround(GraphicsDevice graphicsDevice)
        {
            int ra= random.Next(70, graphicsDevice.Viewport.Height - 180); // the random is made between numbers we have set

            for (int x = 0; x < graphicsDevice.Viewport.Width; x += 10)
            {
                int y = 0;
                if (elevation == 100) // If elevation is equals to 100 a new elevation in the ground is made
                {
                    ra = random.Next(70, graphicsDevice.Viewport.Height - 150); // A random is gerated again
                    elevation = 0;
                }
                do
                {
                    // We set the Y position of the ground objects until is equal to the random number (the elevation)
                    piso = new Ground(new Rectangle(x, graphicsDevice.Viewport.Height - y, 10, 10));
                    pisoTotal.Add(piso);
                    y += 10;

                } while (y <= ra);

                elevation += 10;   // We add elevation 10


            }

        }

        //Methods
        //-----------------------------------------------------------------

        public void LoadContent(ContentManager Content)
        {
            for(int x = 0; x < pisoTotal.Count; x++)
            {
                ((Ground)pisoTotal[x]).LoadContent(Content,"suelo");
            }
           

        }

        public void Update(GameTime gameTime,Rectangle pos)
        {
            for (int x = 0; x < pisoTotal.Count; x++)
            {
                ((Ground)pisoTotal[x]).Collision(pos);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < pisoTotal.Count; x++)
            {
                ((Ground)pisoTotal[x]).Draw(spriteBatch);
            }
        }

        //Properties
        //---------------------------------------------------
        // Property to get the array
        public ArrayList GetArray()
        {
           
                return pisoTotal;
           
        }

        //Property to get the position
        public Rectangle Getpos
        {
            set
            {
                piso.Getpos = value;
            }
            get
            {
                return piso.Getpos;
            }
        }

    }
}
