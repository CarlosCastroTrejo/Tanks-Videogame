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
    // Class in charge run the basic method for the Sprites
    class BasicSprite
    {
        // Attributes
        // ---------------------------------------------
        protected Texture2D texture;  //Texture of the Sprite
        protected Rectangle pos;      //Rectangle of the sprite
        protected Rectangle source;   //Rectangle of the source of  the sprite
        protected Color color;        //Color of the Sprite
        protected bool alive=true;    //Boolean to indicate if the object is alive
       


        // Methods
        //--------------------------------------------
        public BasicSprite(Rectangle pos)
        {
            this.pos = pos;
            this.color = Color.White;
        }
        
        public virtual void LoadContent(ContentManager Content, String name)
        {
            texture = Content.Load<Texture2D>(name);
            // By default, set the source size to the image size
            source = new Rectangle(0, 0, texture.Width, texture.Height);
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //If the object is alive, it will be drawn
            if (alive)
            {
                spriteBatch.Draw(texture, pos, source, color);
            }
        }


        // Properties
        // ---------------------------------------------
        //Property to get the position of the sprite
        public Rectangle Getpos
        {
            set { pos = value; }
            get { return pos; }
        }
        //Property to get the boolean value if the object is alive
        public bool GetAlive
        {
            set { alive = value; }
            get { return alive; }
        }
        //Property to get the color of the Sprite
        public Color Getcolor
        {
            set { color = value; }
            get { return color; }
        }
        

    }
}
