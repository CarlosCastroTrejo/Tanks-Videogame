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

    //Class in charge of the display of the instructions screen
    class Instrucciones : Menu
    {
        //Inherited from Menu class

        public Instrucciones(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            botonJugar.Getpos = new Rectangle(graphicsDevice.Viewport.Width - 200, graphicsDevice.Viewport.Height - 66, 90, 50);
            botonSalir.Getpos = new Rectangle(graphicsDevice.Viewport.Width - 100, graphicsDevice.Viewport.Height - 70, 90, 60);
        }

        public override void LoadContent(ContentManager Content)
        {
            botonJugar.LoadContent(Content, "botonStart");
            botonSalir.LoadContent(Content, "botonExit");
            FondoMenu.LoadContent(Content, "instructions");
            Cursor.LoadContent(Content, "cursor");

        }
    }



}






