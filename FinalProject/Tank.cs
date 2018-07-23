using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace FinalProject
{
    // Class of the Tank

        // Enumerators to know the State of our sprite
    enum SideDirection { MOVERIGHT, MOVELEFT, STANDLEFT, STANDRIGHT, FUSILRIGHT,FUSILEFT };

    class Tank
    {

        // Attributes
        //--------------------------------------------------------
        BasicSprite standLeft, standRight;            // Sprite when there is no movement
        BasicSprite flecha;                          // BasicSprite of the arrow above the object 
        BasicAnimatedSprite moveRight, moveLeft;     // AnimatedSprite whenever the tank is moving left or right
        Shot disp;                                   // Shot of the tank
        Rectangle tempBox;            // Tempbox - helper Rectangle to assing values of other rectangles, abajo & medio - Rectangles to check directional collisions
        ArrayList arregloBalas = new ArrayList();   // Array im which bullets will be added 
        Rotacion fusilRight, fusilLeft;              // Sprite for the rifle of the tank

        BasicSprite abajo, medio;

        SideDirection currentState;
        Keys right, left, up, down,shoot;            // Keys to animate the tanks
       
        int retraso = 0;                            // Variable to make a delay for the shot
        bool ColisionAbajo = true;                  // Boolean to know if there have been a collision on the below area of the sprite
        bool ColisionDerechaMedio = false;                 // Boolean to know if there have been a collision on the right area of the sprite
        bool ColisionIzquierdaMedio = false;              // Boolean to know if there have been a collision on the left area of the sprite
        bool ColisionDerechaAbajo = false;                 // Boolean to know if there have been a collision on the right area of the sprite
        bool ColisionIzquierdaAbajo = false;
        int life = 150;                             // Amount of life of the tank
        bool bajarVida = false;                     // Boolean to know if we need to decrease the life of the tank
        bool subirVida = false;                     // Boolean to know if we need to increase the life of the tank
        float grados=60;                            // Angle (units) of the shot 
        float velocidad = -50;                        // Velocity in which the vullet will travel
        bool lado=false;                            // Boolean to know from which side will the shot wil be fired
        float angulo;                               // Variable that determines the angle of the shot
        int y = 3;                                   //Varible to adjust the position of the shot to the rifle position
        int vidas = 3;                               // Variable that indicates how manu lives does the object has
        string colorFlecha;                          // Variable to asign the filename of the arrrow

        ContentManager _Content;
        GraphicsDevice graphicsDevice;


        // Constructor
        //-------------------------------------
        public Tank(Rectangle pos, int countFrameX, float timePerFrame, Keys right, Keys left, Keys up, Keys down, Keys shoot,GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            standLeft = new BasicSprite(pos);
            standRight = new BasicSprite(pos);

            moveRight = new BasicAnimatedSprite(pos, countFrameX, timePerFrame);
            moveLeft = new BasicAnimatedSprite(pos, countFrameX, timePerFrame);


            fusilLeft = new Rotacion();
            fusilRight = new Rotacion();



            abajo = new BasicSprite( new Rectangle(pos.X, pos.Height - 17, pos.Width-22 , 14));
            medio = new BasicSprite(new Rectangle(pos.X, pos.Height - 37, pos.Width+15, 14));

            flecha = new BasicSprite(new Rectangle (0,0,30,30));

            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
            this.shoot = shoot;
            this.Pos = pos;
        }


        //Methods
        //----------------------------------------------------------
        public virtual void LoadContent(ContentManager Content, string textura, SideDirection currentDirection)
        {
            this._Content = Content;

            flecha.LoadContent(Content, colorFlecha);
            abajo.LoadContent(Content, "azul");
            medio.LoadContent(Content, "azul");
      
            switch (currentDirection)
            {
                case SideDirection.MOVERIGHT:
                    {
                        moveRight.LoadContent(Content, textura);
                    }
                    break;

                case SideDirection.MOVELEFT:
                    {
                        moveLeft.LoadContent(Content, textura);
                    }
                    break;
                case SideDirection.FUSILEFT:
                    {
                        fusilLeft.LoadContent(Content, textura);
                    }
                    break;
                case SideDirection.FUSILRIGHT:
                    {
                        fusilRight.LoadContent(Content, textura);
                    }
                    break;
                case SideDirection.STANDRIGHT:
                    {
                        standRight.LoadContent(Content, textura);
                    }
                    break;
                case SideDirection.STANDLEFT:
                    {
                        standLeft.LoadContent(Content, textura);
                    }
                    break;


            }


        }

        public void Update(GameTime gameTime)
        {
            Rectangle currentPos = this.Pos;
            Rectangle col = abajo.Getpos;

            col.X = currentPos.X+12;
            col.Y = currentPos.Y + currentPos.Height - 17;
            abajo.Getpos = col;

            col = medio.Getpos;
            col.X = currentPos.X -10;
            col.Y = currentPos.Y + currentPos.Height - 37;
            medio.Getpos = col;




            Rectangle posFusil = fusilRight.GetPos;
            posFusil.X = currentPos.X + 41;
            posFusil.Y = currentPos.Y+16;
            fusilRight.GetPos = posFusil;

            posFusil = fusilLeft.GetPos;
            posFusil.X = currentPos.X +13;
            posFusil.Y = currentPos.Y+16;
            fusilLeft.GetPos = posFusil;

            fusilLeft.Update(gameTime);
            fusilRight.Update(gameTime);

            posFusil = flecha.Getpos;
            posFusil.X = currentPos.X+(currentPos.Width / 2)-14;
            posFusil.Y = currentPos.Y - 20;
            flecha.Getpos = posFusil;

            if (bajarVida) // if "bajarvida" is true the life of the tank will decrease 30 units
            {
                life -=15;
                bajarVida = false;
            }
            if (subirVida) // if "subirVida" is true the life of the tank will increase 30 units
            {
                life += 30;
                subirVida = false;
            }

            if (life == 0) // if "life" is equals to 0 the alive attribute of the tank will be false
            {
                GetAlive = false;
                vidas -= 1;
            }
            if (Pos.Y > graphicsDevice.Viewport.Height)
            {
                life = 0;
                GetAlive = false;
            }

            if (currentState == SideDirection.MOVERIGHT)
            {
                currentState = SideDirection.STANDRIGHT;
            }
            if (currentState == SideDirection.MOVELEFT)
            {
                currentState = SideDirection.STANDLEFT;
            }


            
            if (Keyboard.GetState().IsKeyDown(up))
            {
                if (grados < 75)
                {
                    grados += 1;
                    velocidad -= 3;
                    fusilRight.GetDirection=1;
                    fusilLeft.GetDirection =2;
                    y--;
                }
            }

            if (Keyboard.GetState().IsKeyDown(down))
            {
                if (grados > 40)
                {
                    grados -= 1;
                    velocidad += 3;
                    fusilRight.GetDirection=2;
                    fusilLeft.GetDirection=1;
                    y++;
                }
            }
           
            if (!ColisionDerechaMedio && !ColisionIzquierdaMedio && Pos.X>3 && (Pos.X+Pos.Width)<graphicsDevice.Viewport.Width-3)
            {
                if (Keyboard.GetState().IsKeyDown(left))
                {
                    currentState = SideDirection.MOVELEFT;
                    currentPos.X -= 5;
                    moveLeft.Update(gameTime);
                }

                if (Keyboard.GetState().IsKeyDown(right))
                {
                    currentState = SideDirection.MOVERIGHT;
                    currentPos.X += 5;
                    moveRight.Update(gameTime);
                }
            }
            
            else if (ColisionDerechaMedio || (Pos.X + Pos.Width) > graphicsDevice.Viewport.Width - 4)
            {
                if (Keyboard.GetState().IsKeyDown(left))
                {
                    currentState = SideDirection.MOVELEFT;
                    currentPos.X -= 5;
                    moveLeft.Update(gameTime);
                }
            }
            else if (ColisionIzquierdaMedio || Pos.X < 4)
            {
                if (Keyboard.GetState().IsKeyDown(right))
                {
                    currentState = SideDirection.MOVERIGHT;
                    currentPos.X += 5;
                    moveRight.Update(gameTime);
                }
            }

            



            if (Keyboard.GetState().IsKeyDown(shoot) && retraso >= 40)
            {

                int x = fusilLeft.GetPos.X-20;
             
               
                if (currentState == SideDirection.STANDRIGHT || currentState == SideDirection.MOVERIGHT)
                {
                    x = fusilRight.GetPos.X + 10;
                    angulo = grados;
                }
                
                if (currentState == SideDirection.STANDLEFT || currentState == SideDirection.MOVELEFT)
                {
                    lado = false;
                    angulo = 180 - grados;
                }

                disp = new Shot(x, currentPos.Y+y,lado,velocidad,angulo);
                disp.LoadContent(_Content);
                arregloBalas.Add(disp);
                retraso = 0;
            }

            for (int x = 0; x < arregloBalas.Count; x++)
            {

                ((Shot)arregloBalas[x]).Update(gameTime);

            }
            retraso++;

            
            if (ColisionAbajo)
            {

                currentPos.Y += 5;

            }
            else if(ColisionAbajo && (ColisionIzquierdaAbajo || ColisionDerechaAbajo || ColisionDerechaMedio || ColisionIzquierdaMedio))
            {
                currentPos.Y += 5;

            }
            else if (ColisionIzquierdaAbajo || ColisionDerechaAbajo && !ColisionDerechaMedio && !ColisionIzquierdaMedio)
            {
                currentPos.Y -= 8;
            }
            ColisionAbajo = true;
            ColisionDerechaMedio = false;
            ColisionIzquierdaMedio = false;

            ColisionDerechaAbajo = false;
            ColisionIzquierdaAbajo = false;

            this.Pos = currentPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentState == SideDirection.STANDRIGHT)
            {
               
                fusilRight.Draw(spriteBatch);
                standRight.Draw(spriteBatch);
             
            }
            if (currentState == SideDirection.STANDLEFT)
            {
               
                fusilLeft.Draw(spriteBatch);
                standLeft.Draw(spriteBatch);
            }
            if (currentState == SideDirection.MOVERIGHT)
            {
                fusilRight.Draw(spriteBatch);
                moveRight.Draw(spriteBatch);
               
            }
            if (currentState == SideDirection.MOVELEFT)
            {
                fusilLeft.Draw(spriteBatch);
                moveLeft.Draw(spriteBatch);
              
            }

            for (int x = 0; x < arregloBalas.Count; x++)
            {
                ((Shot)arregloBalas[x]).Draw(spriteBatch);
            }
            flecha.Draw(spriteBatch);
            //abajo.Draw(spriteBatch);
            //medio.Draw(spriteBatch);
        }


        // Colision del Tanque con el Ground
        public void Collision(Rectangle pos)
        {
            if (pos.Intersects(Pos))
            {

               
                // checar colision de abajo
                tempBox = Pos; tempBox.Height -= 10;
                if (!tempBox.Intersects(pos))
                {
                    ColisionAbajo = false;
                }

               
            }

        }

        //Collision betwwen de middle part of the tank and the grounds
        public void CollisionMedio(Rectangle pos)
        {
            if (medio.Getpos.Intersects(pos))
            {

                // checar colision de Derecha
                tempBox = medio.Getpos; tempBox.Width -= 10;
                if (!tempBox.Intersects(pos))
                {
                    ColisionDerechaMedio = true;
                }

                // checar colision de la izquierda
                tempBox = medio.Getpos; tempBox.Width -= 10; tempBox.X += 10;
                if (!tempBox.Intersects(pos))
                {
                    ColisionIzquierdaMedio = true;
                }

            }
            if (abajo.Getpos.Intersects(pos))
            {
                // checar colision de Derecha
                tempBox = abajo.Getpos; tempBox.Width -= 10;
                if (!tempBox.Intersects(pos))
                {
                    ColisionDerechaAbajo = true;
                }

                // checar colision de la izquierda
                tempBox = abajo.Getpos; tempBox.Width -= 10; tempBox.X +=10;
                if (!tempBox.Intersects(pos))
                {
                    ColisionIzquierdaAbajo = true;
                }
            }
        }

        //Colision con el Shot para bajar vida
        public void Collision(Shot shot)
        {
            if (shot.GetPos.Intersects(Pos))
            {
                bajarVida = true;
            }

        }

        //Colision con Life para subir vida
        public void Collision(Life vida)
        {
            if (vida.Getpos.Intersects(Pos))
            {
                subirVida = true;
            }
        }
        
        
        
        //Properties
        //-----------------------------------------------

        //Property to get the position of all the BasicSprites and AnimatedSprites
        public Rectangle Pos
        {
            set
            {
                standLeft.Getpos = value;
                standRight.Getpos = value;
                moveLeft.Getpos = value;
                moveRight.Getpos = value;
                //standUP.Getpos = value;
                //standDown.Getpos = value;
                //moveUp.Getpos = value;
                //moveDown.Getpos = value;
            }
            get
            {
                return standRight.Getpos;
            }
        }

        //Property to get the Alive of all the BasicSprites and AnimatedSprites
        public bool GetAlive
        {
            set
            {
                standLeft.GetAlive = value;
                standRight.GetAlive = value;
                moveLeft.GetAlive = value;
                moveRight.GetAlive = value;
                fusilLeft.GetAlive = value;
                fusilRight.GetAlive = value;
            }
            get
            {
                return standLeft.GetAlive;
            }
        }

        //Property to get the Array of bullets
        public ArrayList GetArreglo()
        {
            return arregloBalas;
            
        }

        //Property to get the life of the tanks
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

        //Property to get the lives of the tanks
        public int Getlives
        {
            set
            {
                vidas = value;
            }
            get
            {
                return vidas;
            }
        }

        //Property to get the life of the tanks
        public string GetFlechaColor
        {
            set
            {
                colorFlecha = value;
            }
            get
            {
                return colorFlecha;
            }
        }





    }
}
