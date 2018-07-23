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

    // Class in charge of the basic methods for the shots
    enum Estate { EXPLOSION,NORMAL}     //Enumerators to indicate the estate of the object

    class Shot
    {   
        BasicSprite bullet;             // Sprite of the shot
        SingleShot fire;                // Animation of fire of the shot
         bool lado=true;                // Boolean to indicate from which side the shot will be fired
        Estate estado;                  // Declaration of the enumerator

        double g = 520;                 // pixels per second squared | gravitational acceleration
       
        double vx, vy, alpha, t = 0;   // v - final velocity | vy - velocity in the y-axis | vx - velocity in the x-axis | alpha - angle fo the shot in radians | t2 - time
        float grados=10;                 // angle of the shot as units

        // Constructor
        //-----------------------------------------------------
        public Shot(int x, int y, bool lado, float velocidad, float angulo)
        {
            estado = Estate.NORMAL;
            bullet = new BasicSprite(new Rectangle(x, y, 12, 12));
            fire = new SingleShot(new Rectangle(x, y, 20, 20), false, 16, 1, 64, 64, 0.04f);
            this.lado = lado;
            this.alpha = MathHelper.ToRadians(angulo); // the angle at which the object is thrown (measured in radians)
            this.vx = 5 * velocidad * Math.Cos(this.alpha);
            this.vy = 5 * velocidad * Math.Sin(this.alpha);
        }
        

        //Methods
        //-------------------------------------------------
        public void LoadContent(ContentManager Content)
        {
            bullet.LoadContent(Content, "Bala");
            fire.LoadContent(Content, "fire");
        }

        public void Update(GameTime gameTime)
        {
            if (estado == Estate.NORMAL)    // If the state is Normal we draw the bullet sprite
            {
                if (lado == true)  // We made a decision depending on which side the bullet will be fired
                {
                    // will be shooted to the right 
                    int y, x;

                    Rectangle tempRec = bullet.Getpos;
                    x = tempRec.X;
                    y = tempRec.Y;
                    //tempRec.Y = y + (int)(vy * t2 + g * t2 * t2 / 2);  // formula to get the position in Y of the bullet
                    //tempRec.X = x+(int)((vx * -1) * t2);             // Formila to get the position in X og the bullet
                    vy = vy + g * t;
                    tempRec.Y = (int)(tempRec.Y + vy * t);
                    tempRec.X += (int)(vx * t);
                    //t2 = t2 + .005;
                    fire.Getpos = tempRec;
                    bullet.Getpos = tempRec;
                    t = 0.01;
                }
                if (lado == false)
                {
                    // will be shooted to the left
                    int y, x;
                    Rectangle tempRec = bullet.Getpos;
                    x = tempRec.X;
                    y = tempRec.Y;
                    //tempRec.Y = y + (int)(vy * t2 + g * t2 * t2 / 2);
                    //tempRec.X = x + (int)((vx * -1) * t2);
                    //vx = vx
                    vy = vy + g * t;
                    tempRec.Y = (int)(tempRec.Y + vy * t);
                    tempRec.X -= (int)(vx * t);
                    //t2 = t2 +.005;
                    fire.Getpos = tempRec;
                    bullet.Getpos = tempRec;
                    t = 0.01;
                }

            }
            if (estado == Estate.EXPLOSION)  // if the state is explosion, the fire will be updated
            {
                fire.Update(gameTime);

            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (estado == Estate.NORMAL)
            {
                bullet.Draw(spriteBatch);
            }
            if (estado == Estate.EXPLOSION)
            {
                fire.Draw(spriteBatch);
            }

        }

       // Collision between the Tanks/Ground and the position of the shot
        public void Collision(Rectangle pos)
        {
                if (pos.Intersects(bullet.Getpos))      
                {
                    estado = Estate.EXPLOSION; // if they intersect the state change to explosion
                    bullet.GetAlive = false;
                    Rectangle tempbox = fire.Getpos;
                     tempbox.Y -= 10;
                    tempbox.Width = 30;
                    tempbox.Height = 30;
                     fire.Getpos = tempbox;
                //  fire.GetAlive = false;
                }
        }

        //Properties    
        //--------------------------------------------------------
         //Property to get the Rectangle of the class
        public Rectangle GetPos
        {
            set
            {
                bullet.Getpos = value;
                fire.Getpos = value;
               
            }
            get
            {
                return bullet.Getpos;
            }
        }
        
        //Property to get the boolean alive
        public bool GetAlive
        {
            set
            {
                bullet.GetAlive = value;
               

            }
            get
            {
                return bullet.GetAlive;
            }
        }

        //Property to get the angles in deegres of the shot
        public float Grados
        {
            set
            {
                grados = value;
            }
            get
            {
                return grados;
            }
        }

    }
}
