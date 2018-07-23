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
    //Class Menu in charge of diplay the whole menu screen during the execution of the program

    class Menu
    {

       protected BasicSprite botonJugar;    // Declaration of the buttons as BasicSprites 
        protected BasicSprite botonSalir;
        protected Background FondoMenu;    // Declaration of the menu background
        protected BasicSprite Cursor;      // Declaration of the cursor as BasicSprite
        protected bool Forward=false;      // Boolean in order to indicate if the user decides to continue
        protected bool Out = false;        // Boolean in order to indicate if the user decides to exit

        //Constructor
        //-----------------------------------------------
        public Menu(GraphicsDevice graphicsDevice)
        {
            botonJugar=new BasicSprite(new Rectangle((graphicsDevice.Viewport.Width/2)-30,graphicsDevice.Viewport.Height/2,90,60));
            botonSalir = new BasicSprite(new Rectangle((graphicsDevice.Viewport.Width/2-30), graphicsDevice.Viewport.Height/2+90, 90, 60));
            FondoMenu = new Background(new Rectangle(0,0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height ));
            Cursor = new BasicSprite(new Rectangle(0, 0, 15, 15));
        }

        //Methods
        //----------------------------------------------------
        public virtual void LoadContent(ContentManager Content)
        {

            botonJugar.LoadContent(Content, "botonStart");
            botonSalir.LoadContent(Content, "botonExit");
            FondoMenu.LoadContent(Content, "Menu1B");
            Cursor.LoadContent(Content, "cursor");

        }


        public void Update(GameTime gameTime)
        {
            // Code with the objective of set the values of position to the cursor object
            Rectangle tempbox = Cursor.Getpos;
            tempbox.X = Mouse.GetState().X;
            tempbox.Y = Mouse.GetState().Y;
            Cursor.Getpos = tempbox;

            if (botonJugar.Getpos.Intersects(Cursor.Getpos))
            {
                botonJugar.Getcolor = Color.Gray;
            }
            else
            {
                botonJugar.Getcolor = Color.White;

            }
            if (botonSalir.Getpos.Intersects(Cursor.Getpos))
            {
                botonSalir.Getcolor = Color.Gray;
            }
            else
            {
                botonSalir.Getcolor = Color.White;
            }

            if(Mouse.GetState().LeftButton==ButtonState.Pressed && Cursor.Getpos.Intersects(botonJugar.Getpos))
            {
                Forward = true;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && Cursor.Getpos.Intersects(botonSalir.Getpos))
            {
                Out = true;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            FondoMenu.Draw(spriteBatch);
            botonJugar.Draw(spriteBatch);
            botonSalir.Draw(spriteBatch);
            Cursor.Draw(spriteBatch);
           
        }

        //Properties
        //---------------------------------------------

        //Property to get the boolean of forward
        public bool GetForward
        {
            set
            {
                Forward = value;
            }
            get
            {
                return Forward;
            }
        }

        //Property to get the boolean of out
        public bool GetOut
        {
            set
            {
                Out = value;
            }
            get
            {
                return Out;
            }
        }



    }
}
