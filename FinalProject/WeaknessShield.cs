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

    // Class in charge of generate objects of Weakness and execute the basic methods of this one
    class WeaknessShield :BasicSprite
    {
        Random rand;        // Declaration of a random variable 
        int counter = 0;    // Counter in order to activate the object
        bool active;        // Boolean to determine if the object is active or not 
        int life = 150;     // Total amount of live of the weakness shield
        bool bajarVida = false; // Boolean in order to determine if decrease the life or not

        //Constructor
        //-----------------------------------------------------------------
        public WeaknessShield(Rectangle pos) : base(pos)
        {
            rand = new Random(); //Initialization of the random object in order to make it truly random
        }


        //Method
        //----------------------------------------------------
        public  void Update(GameTime gameTime)
        {
            counter++;
            if (counter == 100) //if counter==100 the object has a chance to be alive
            {

                int numero = rand.Next(0,180); //Sets a random to "numero" between 0 and 180
                if (numero > 70)  // If the random is bigger than 70 the object will be active (meaning that it has a 61.1% of probability to be active)
                {
                    int numero2 = rand.Next(100, 180); //Set a random object between 100 and 180 to determine the pos in Y of the object when it appears
                    active = true; 
                    Rectangle tempbox = Getpos;
                    tempbox.Y = numero2;
                    Getpos = tempbox;
                }
                  
               
            }
            if (counter > 400) // Tha object will be active until the counter is bigger than 400
            {
                active = false;
                counter = 0;
            }

            if (bajarVida)
            {
                life -= 10;
                bajarVida = false;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                base.Draw(spriteBatch);
            }
        }

        //Collition between a shot position and the weakness shield position
        public void Collision(Shot shot)
        {
            if (shot.GetPos.Intersects(this.pos))
            {
                bajarVida = true;
            }
        }

        //Properties
        //--------------------------------------
        //Property in order to get the life
        public int GetLife
        {
            set
            {
                life = value;
            }
            get
            {
                return life;
            }
        } 


    }
}
