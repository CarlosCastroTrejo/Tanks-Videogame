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
    // Class in charge of generate objects of ground and execute the basic methods of this one
    class Ground:BasicSprite
    {
        //Inherited from Basicsprite

        //Constructor
        //-----------------------------------------------
        public Ground(Rectangle pos) : base(pos)
        {
           
        }

        //Methods
        //---------------------------------------------
        //Collision between a Shot object and the ground position
        public void Collision(Rectangle pos)
        {
             if (this.pos.Intersects(pos))
             {
                GetAlive = false;
               
             }
           
        }
       
    }
}
