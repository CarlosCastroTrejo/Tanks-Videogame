using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;
using System;

namespace FinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    enum GameState { MENU,INSTRUCCIONES,JUEGO,RESET,FIN}

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont player1;             // Signboard for player 1
        SpriteFont player2;            // Signboard for player 2
        SpriteFont vida1;               // Signboard for tank1 life
        SpriteFont vida2;               // Signboard for tank2 life
        SpriteFont shield1T;            // Singboard for shield1
        SpriteFont shield2T;            // Signboard for shield2
        Background fondo;               // Backfground of the first scene 
        Tank tanque1;                   // Declaration of the tank1
        Tank tanque2;                   // Declaration of the tank2
        WeaknessShield shield1;         // Declaration of the shield for tank 1
        WeaknessShield shield2;         // Declaraion of the shield for tank 2
        BasicSprite vidaTanque1;        // Declaration of the Barlife for tank1
        BasicSprite vidaTanque2;        // Declaration of the Barlife for tank2
        BasicSprite vidaShield1;        // Declaration of the Barlife for weaknessShield 1
        BasicSprite vidaShield2;        // Declaration of the Barlife for weaknessShield2
        BasicSprite gameOver;           // Declaration for the gameOver signboard
        TotalGround stage;              // Declararion of the stage
        GameState gamephase;
        Menu mainmenu;                  // declaration of the Menu
        Instrucciones instrucciones;    // Declaration of the instructions
        bool pasar=false;               // Boolean to know if user have already picked to pass to the game
        bool salir= false;              // Boolean to know if user have already picked to pass to the exit
        Life vidaExtra;                 // Declaration of the life
        int numeroDeFondo=1;            // Variable to change the backgrounds
            
        Random random = new Random();
 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fondo = new Background(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            tanque1 = new Tank(new Rectangle(200, 75, 60, 60), 4, 0.03f, Keys.Right, Keys.Left, Keys.Up, Keys.Down,Keys.Space,GraphicsDevice);
            tanque1.GetFlechaColor = "RedIndicator";
            tanque2 = new Tank(new Rectangle(500, 75, 60, 60), 4, 0.03f, Keys.D, Keys.A, Keys.W, Keys.S,Keys.F,GraphicsDevice);
            tanque2.GetFlechaColor = "BlueIndicator";
            shield1 = new WeaknessShield(new Rectangle(20, 10, 40, 40));
            shield1.Getcolor = Color.Red;
            shield2 = new WeaknessShield(new Rectangle(GraphicsDevice.Viewport.Width-60, 10, 40, 40));
            stage = new TotalGround(GraphicsDevice);
            gamephase = GameState.MENU;
            mainmenu = new Menu(GraphicsDevice);
            instrucciones = new Instrucciones(GraphicsDevice);
            vidaExtra = new Life(GraphicsDevice );
           

            vidaTanque1 = new BasicSprite(new Rectangle(5, 33, 150, 23));
            vidaShield1 = new BasicSprite(new Rectangle(5, 60, 150, 23));

            vidaTanque2 = new BasicSprite(new Rectangle(GraphicsDevice.Viewport.Width - 205, 33, 150, 23));
            vidaShield2 = new BasicSprite(new Rectangle(GraphicsDevice.Viewport.Width - 205, 60, 150, 23));

            //Signs loadContent
            player1 = Content.Load<SpriteFont>("SpriteFont1");
            player2 = Content.Load<SpriteFont>("SpriteFont1");
            vida1 = Content.Load<SpriteFont>("SpriteFont1");
            vida2= Content.Load<SpriteFont>("SpriteFont1");
            shield1T= Content.Load<SpriteFont>("SpriteFont1");
            shield2T= Content.Load<SpriteFont>("SpriteFont1");

            gameOver = new BasicSprite(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fondo.LoadContent(Content, "Background1");
            tanque1.LoadContent(Content, "MoveLeft/f", SideDirection.MOVELEFT);
            tanque1.LoadContent(Content, "MoveRight/f", SideDirection.MOVERIGHT);
            tanque1.LoadContent(Content, "StandLeft/f01", SideDirection.STANDLEFT);
            tanque1.LoadContent(Content, "StandRight/f01", SideDirection.STANDRIGHT);
            tanque1.LoadContent(Content, "fusilLeft", SideDirection.FUSILEFT);
            tanque1.LoadContent(Content, "fusilRight", SideDirection.FUSILRIGHT);

            tanque2.LoadContent(Content, "MoveLeft/f", SideDirection.MOVELEFT);
            tanque2.LoadContent(Content, "MoveRight/f", SideDirection.MOVERIGHT);
            tanque2.LoadContent(Content, "StandLeft/f01", SideDirection.STANDLEFT);
            tanque2.LoadContent(Content, "StandRight/f01", SideDirection.STANDRIGHT);
            tanque2.LoadContent(Content, "fusilLeft", SideDirection.FUSILEFT);
            tanque2.LoadContent(Content, "fusilRight", SideDirection.FUSILRIGHT);

            stage.LoadContent(Content);
            mainmenu.LoadContent(Content);
            instrucciones.LoadContent(Content);
            shield1.LoadContent(Content,"shield");
            shield2.LoadContent(Content, "shield");

            vidaTanque1.LoadContent(Content, "rojo");
            vidaShield1.LoadContent(Content, "rojo");

            vidaTanque2.LoadContent(Content, "azul");
            vidaShield2.LoadContent(Content, "azul");
            gameOver.LoadContent(Content, "gameOver");

            vidaExtra.LoadContent(Content, "life");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Update for the Menu phase
            if (gamephase == GameState.MENU)
            {
                mainmenu.Update(gameTime);
                pasar = mainmenu.GetForward;
                if (pasar == true)
                {
                    gamephase = GameState.INSTRUCCIONES;
                    pasar = false;
                }
                salir = mainmenu.GetOut;
                if (salir == true)
                {
                    Exit();
                }
            }

            // Update for the instruction phase
            if (gamephase == GameState.INSTRUCCIONES)
            {
                instrucciones.Update(gameTime);
                pasar = instrucciones.GetForward;
                if (pasar == true)
                {
                    gamephase = GameState.JUEGO;
                }
                salir = instrucciones.GetOut;
                if (salir == true)
                {
                    Exit();
                }
            }

            // Update for the Game phase
            if (gamephase == GameState.JUEGO)
            {
                

                // TODO: Add your update logic here

                if (tanque1.GetAlive == false || tanque2.GetAlive == false)
                {
                    gamephase = GameState.RESET;
                }
                if(tanque1.Getlives==0 || tanque2.Getlives == 0)
                {
                    gamephase = GameState.FIN;
                }
                if(shield1.GetAlive==false || shield2.GetAlive == false)
                {
                    gamephase = GameState.FIN;
                }
                tanque1.Update(gameTime);
                tanque2.Update(gameTime);

                shield1.Update(gameTime);
                shield2.Update(gameTime);
                vidaExtra.Update(gameTime);


                //Check collision between Shots of the tanks
                for (int x = 0; x < tanque1.GetArreglo().Count; x++)
                {
                    if (tanque2.GetAlive)
                    {
                        if (((Shot)tanque1.GetArreglo()[x]).GetAlive)
                        {
                            ((Shot)tanque1.GetArreglo()[x]).Collision(tanque2.Pos);
                            tanque2.Collision(((Shot)tanque1.GetArreglo()[x]));
                        }
                    }
                }

                for (int x = 0; x < tanque2.GetArreglo().Count; x++)
                {
                    if (tanque1.GetAlive)
                    {
                        if (((Shot)tanque2.GetArreglo()[x]).GetAlive)
                        {
                            ((Shot)tanque2.GetArreglo()[x]).Collision(tanque1.Pos);
                            tanque1.Collision(((Shot)tanque2.GetArreglo()[x]));
                        }
                    }
                }

                //Check Collision between bullets of Tank1 and ground
                for (int x = 0; x < stage.GetArray().Count; x++)
                {
                    if (((Ground)stage.GetArray()[x]).GetAlive)
                    {
                        if (tanque1.GetAlive)
                        {
                            for (int y = 0; y < tanque1.GetArreglo().Count; y++)
                            {

                                if (((Shot)tanque1.GetArreglo()[y]).GetAlive)
                                {

                                    ((Ground)stage.GetArray()[x]).Collision(((Shot)tanque1.GetArreglo()[y]).GetPos);
                                    ((Shot)tanque1.GetArreglo()[y]).Collision(((Ground)stage.GetArray()[x]).Getpos);

                                }
                            }
                        }
                        
                    }

                }


                //Check Collision between bullets of Tank2 and ground
                for (int x = 0; x < stage.GetArray().Count; x++)
                {
                    if (((Ground)stage.GetArray()[x]).GetAlive)
                    {
                        if (tanque2.GetAlive)
                        {
                            for (int y = 0; y < tanque2.GetArreglo().Count; y++)
                            {

                                if (((Shot)tanque2.GetArreglo()[y]).GetAlive)
                                {

                                    ((Ground)stage.GetArray()[x]).Collision(((Shot)tanque2.GetArreglo()[y]).GetPos);

                                    ((Shot)tanque2.GetArreglo()[y]).Collision(((Ground)stage.GetArray()[x]).Getpos);
                                }
                            }
                        }
                        
                    }

                }

                //Check collision of the tanks with the ground, this to implement gravity
                for (int x = 0; x < stage.GetArray().Count; x++)
                {
                    if (((Ground)stage.GetArray()[x]).GetAlive)
                    {
                        tanque1.Collision(((Ground)stage.GetArray()[x]).Getpos);
                        tanque2.Collision(((Ground)stage.GetArray()[x]).Getpos);
                    }
                }

                //Updates for the life bar
                {
                    Rectangle tempox = vidaTanque1.Getpos;
                    tempox.Width = tanque1.GetLife;
                    vidaTanque1.Getpos = tempox;

                    tempox = vidaTanque2.Getpos;
                    tempox.Width = tanque2.GetLife;
                    vidaTanque2.Getpos = tempox;

                    tempox = vidaShield1.Getpos;
                    tempox.Width = shield1.GetLife;
                    vidaShield1.Getpos = tempox;

                    tempox = vidaShield2.Getpos;
                    tempox.Width = shield2.GetLife;
                    vidaShield2.Getpos = tempox;
                }
                
                //Check Collision between bullets of Tank1 and shield2
                if (shield2.GetAlive)
                {
                    if (tanque1.GetAlive)
                    {
                        for (int x = 0; x < tanque1.GetArreglo().Count; x++)
                        {

                            if (((Shot)tanque1.GetArreglo()[x]).GetAlive)
                            {

                                shield2.Collision(((Shot)tanque1.GetArreglo()[x]));
                                ((Shot)tanque1.GetArreglo()[x]).Collision(shield2.Getpos);

                            }
                        }
                    }
                }
                
                //Check Collision between bullets of Tank2 and shield1
                if (shield1.GetAlive)
                {
                    if (tanque2.GetAlive)
                    {
                        for (int x = 0; x < tanque2.GetArreglo().Count; x++)
                        {

                            if (((Shot)tanque2.GetArreglo()[x]).GetAlive)
                            {

                                shield1.Collision(((Shot)tanque2.GetArreglo()[x]));
                                ((Shot)tanque2.GetArreglo()[x]).Collision(shield1.Getpos);

                            }
                        }
                    }
                }
                
                //Check Collision between the extra Life and ground
                for (int x = 0; x < stage.GetArray().Count; x++)
                {
                    if (((Ground)stage.GetArray()[x]).GetAlive)
                    {
                        if (vidaExtra.GetAlive)
                        {
                            vidaExtra.Collision(((Ground)stage.GetArray()[x]));
                        }                        
                    }

                }


                //Check Collision between the extra Life Tanks
                if (vidaExtra.GetAlive)
                {
                    if (tanque1.GetAlive)
                    {
                        vidaExtra.Collision(tanque1);
                        tanque1.Collision(vidaExtra);
                    }
                }
                if (vidaExtra.GetAlive)
                {
                    if (tanque2.GetAlive)
                    {
                        vidaExtra.Collision(tanque2);
                        tanque2.Collision(vidaExtra);
                    }
                }


                //Check Collision between of the tanks from the right and left part
                for (int x = 0; x < stage.GetArray().Count; x++)
                {
                    if (((Ground)stage.GetArray()[x]).GetAlive)
                    {
                        tanque1.CollisionMedio(((Ground)stage.GetArray()[x]).Getpos);
                    }
                }
                for (int x = 0; x < stage.GetArray().Count; x++)
                {
                    if (((Ground)stage.GetArray()[x]).GetAlive)
                    {
                        tanque2.CollisionMedio(((Ground)stage.GetArray()[x]).Getpos);
                    }
                }

            }

            //Update for the Reset phase
            if (gamephase == GameState.RESET)
            {
                numeroDeFondo++;
                if (numeroDeFondo == 6)
                {
                    numeroDeFondo = 1;
                }
                fondo.LoadContent(Content, "Background" + numeroDeFondo);
                int x = random.Next(1, GraphicsDevice.Viewport.Width - 62);
                if (tanque1.GetAlive == false)
                {
                    tanque1.GetAlive = true;
                    tanque1.Getlives += 1;
                    tanque1.GetLife = 150;
                    tanque1.Pos = new Rectangle(x, 0, 60, 60);
                   
                    vidaTanque1.Getpos = new Rectangle(5, 33, 150, 23);
                }
                if (tanque2.GetAlive == false)
                {
                    tanque2.Getlives += 1;
                    tanque2.GetAlive = true;
                    tanque2.GetLife = 150;
                    tanque2.Pos = new Rectangle(x, 75, 60, 60);
                 
                    vidaTanque2.Getpos = new Rectangle(GraphicsDevice.Viewport.Width - 205, 33, 150, 23);
                }
                

                gamephase = GameState.JUEGO;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (gamephase == GameState.JUEGO)
            {
                spriteBatch.Begin();
                fondo.Draw(spriteBatch);
                tanque1.Draw(spriteBatch);
                tanque2.Draw(spriteBatch);
                stage.Draw(spriteBatch);
                shield1.Draw(spriteBatch);
                shield2.Draw(spriteBatch);

                vidaExtra.Draw(spriteBatch);


                //Life Bar drawing
                vidaTanque1.Draw(spriteBatch);
                vidaTanque2.Draw(spriteBatch);
                vidaShield1.Draw(spriteBatch);
                vidaShield2.Draw(spriteBatch);

                //Signs Drawing
                spriteBatch.DrawString(player1, "Player 1: "+tanque1.Getlives+ " lives", new Vector2(10, 3), Color.White);
                spriteBatch.DrawString(player2, "Player 2: "+ tanque2.Getlives+ " lives", new Vector2(GraphicsDevice.Viewport.Width - 200, 3), Color.White);
                spriteBatch.DrawString(vida1, "Tank", new Vector2(10, 30), Color.Black);
                spriteBatch.DrawString(vida2, "Tank", new Vector2(GraphicsDevice.Viewport.Width - 200, 30), Color.Black);
                spriteBatch.DrawString(shield1T, "Shield", new Vector2(10, 60), Color.Black);
                spriteBatch.DrawString(shield2T, "Shield", new Vector2(GraphicsDevice.Viewport.Width - 200, 60), Color.Black);

                spriteBatch.End();
            }
            if (gamephase == GameState.MENU)
            {
                spriteBatch.Begin();
                mainmenu.Draw(spriteBatch);
                spriteBatch.End();
            }
            if (gamephase == GameState.INSTRUCCIONES)
            {
                spriteBatch.Begin();
                instrucciones.Draw(spriteBatch);
                spriteBatch.End();
            }
            if (gamephase == GameState.FIN)
            {
                spriteBatch.Begin();
                gameOver.Draw(spriteBatch);
                spriteBatch.End();
            }


            base.Draw(gameTime);
        }
    }
}
