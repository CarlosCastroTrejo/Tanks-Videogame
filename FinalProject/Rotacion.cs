using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace FinalProject
{
   
    class Rotacion
    {

        Texture2D spriteTextura; //Texture of the Sprite
        Rectangle spriteRectangle; //Rectangle of the sprite
        Vector2 spriteOrigin;  //Origin of rotation
        float rotation; //How much the sprite will rotate
        int state=3;    //State in to know in which direction will the sprite rotate
        bool alive = true;  //Boolean to indicate if the object is alive



        //Methods
        //---------------------------------------------------------
        public void LoadContent(ContentManager Content,string filename)
        {
            spriteTextura = Content.Load<Texture2D>(filename);
            spriteRectangle = new Rectangle(0,0, spriteTextura.Width, spriteTextura.Height);
            spriteOrigin = new Vector2(spriteRectangle.Width / 2, spriteRectangle.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            if (alive)
            {
                if (state == 1)
                {
                    rotation -= .018f;
                }

                if (state == 2)
                {
                    rotation += .018f;
                }

                state = 3;
            }
           
        }
        
        public void  Draw(SpriteBatch spriteBatch)
       {
            if(alive)
            spriteBatch.Draw(spriteTextura, new Vector2(spriteRectangle.X, spriteRectangle.Y), null, Color.White, rotation, spriteOrigin, 1.2f, SpriteEffects.None, 0);
            
       }

        //Properties
        //-------------------------------------
        //Property to rectangle of the sprite
        public Rectangle GetPos
        {
            set
            {
                spriteRectangle = value;
            }
            get
            {
                return spriteRectangle;
            }
        }

        //Property to get the direcion of the sprite
        public int GetDirection
        {
            set
            {
                state = value;
            }
            get
            {
                return state;
            }
        }

        //Property to know if the object of this class is alive
        public bool GetAlive
        {
            set
            {
                alive = value;
            }
            get
            {
                return alive;
            }
        }
    }
}


