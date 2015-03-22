using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NoNamedGame.Screens
{
    public class MenuScreen : Screen
    {
        //TODO: Serializar buttons
        private String imagesPath;
        private Image logoImage;
        private Image backgroundImage;
        private Image chemistryImage;
        private Image cubeImage;
        private Image energyImage;
        private Image infiniteImage;
        private Image triangleImage;
        private Image drBostrom;

        private List<Image> drawings;

        private ButtonJugar buttonJugar;
        private ButtonOpciones buttonOpciones;
        private ButtonSalir buttonSalir;
        private List<Button> buttons;
        //En el botón que se encuentra el drBostrom
        private int drBostromButton;
        //Espacio entre el botón y el drBostrom
        private Vector2 drBostromFromButton;

        public MenuScreen()
        {
            base.Type = this.GetType();
            drawings = new List<Image>();
            buttons = new List<Button>();
            imagesPath = "MenuScreen/";

            //Imágenes
            drBostrom = new Image();
            logoImage = new Image();
            backgroundImage = new Image();
            chemistryImage = new Image();
            cubeImage = new Image();
            energyImage = new Image();
            infiniteImage = new Image();
            triangleImage = new Image();

            drBostromButton = 0;
            drBostromFromButton.X = 10;

        }

        public override void LoadContent(ContentManager Content)
        {
            //Paths de la textura
            logoImage.path = imagesPath + "Logo";
            backgroundImage.path = imagesPath + "Background";
            chemistryImage.path = imagesPath + "Chemistry";
            cubeImage.path = imagesPath + "Cube";
            energyImage.path = imagesPath + "Energy";
            infiniteImage.path = imagesPath + "Infinite";
            triangleImage.path = imagesPath + "Triangle";
            drBostrom.path = imagesPath + "DrBostrom";

            //Añade los dibujos a drawings (dibujos)
            drawings.Add(chemistryImage);
            drawings.Add(cubeImage);
            drawings.Add(energyImage);
            drawings.Add(infiniteImage);
            drawings.Add(triangleImage);

            //Cargas de las imágenes (Incluye carga de textura con el Content.Load<>(), por eso se pone antes que la posición)
            logoImage.Loadcontent();
            backgroundImage.Loadcontent();
            drBostrom.Loadcontent();

            foreach (Image image in drawings)
                image.Loadcontent();

            //Posiciones
            logoImage.position = new Vector2(Managers.ScreenManager.Instance.dimensions.X / 2 - logoImage.texture.Width / 2, 10);
            backgroundImage.position = new Vector2(0, 0);
            //Arriba a la izquierda
            chemistryImage.position = new Vector2(10, 10);
            //Arriba a la derehca
            cubeImage.position = new Vector2(Managers.ScreenManager.Instance.dimensions.X - cubeImage.texture.Width - 10, 10);
            //Abajo a la izquierda
            energyImage.position = new Vector2(10, Managers.ScreenManager.Instance.dimensions.Y - energyImage.texture.Height - 10);
            //Abajo a la derecha
            infiniteImage.position = new Vector2(Managers.ScreenManager.Instance.dimensions.X - infiniteImage.texture.Width - 10,
                Managers.ScreenManager.Instance.dimensions.Y - infiniteImage.texture.Height - 10);
            //Centro
            triangleImage.position = new Vector2(Managers.ScreenManager.Instance.dimensions.X / 2 - triangleImage.texture.Width / 2,
                Managers.ScreenManager.Instance.dimensions.Y / 2 - triangleImage.texture.Height / 2);


            //Speed
            chemistryImage.speed = new Vector2(1, 1);
            cubeImage.speed = new Vector2(-1, 1);
            energyImage.speed = new Vector2(1, -1);
            infiniteImage.speed = new Vector2(-1, -1);

            //Random para que la figura triángulo centrada en el medio de la pantalla empiece en dirección random
            Random r = new Random();
            int result = r.Next(4);
            if (result == 0)
                triangleImage.speed = new Vector2(1, 1);
            else if (result == 1)
                triangleImage.speed = new Vector2(-1, 1);
            else if (result == 2)
                triangleImage.speed = new Vector2(1, -1);
            else
                triangleImage.speed = new Vector2(-1, -1);

            //Alpha
            foreach (Image image in drawings)
                image.alpha = 0.3F;

            //Botones
            int espacioEntreBotones = 15;
            buttonJugar = new ButtonJugar(imagesPath + "Jugar", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3 - 25, Managers.ScreenManager.Instance.dimensions.Y / 2));
            buttonJugar.LoadContent(Content);
            buttonOpciones = new ButtonOpciones(imagesPath + "Opciones", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3 - 25, buttonJugar.position.Y + buttonJugar.texture.Height + espacioEntreBotones));
            buttonOpciones.LoadContent(Content);
            buttonSalir = new ButtonSalir(imagesPath + "Salir", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3 - 25, buttonOpciones.position.Y + buttonOpciones.texture.Height + espacioEntreBotones));
            buttonSalir.LoadContent(Content);

            buttons.Add(buttonJugar);
            buttons.Add(buttonOpciones);
            buttons.Add(buttonSalir);
        }

        public override void Update(GameTime gameTime)
        {
            Managers.InputManager.Instance.Update();

            //Mueve los dibujitos por la pantalla :$
            DoTheMove();

            //Establece la posición del Drbostrom en los botones basándose en el mouse/keys
            MouseControl();
            KeysControl();

            //Acomoda al señor doctor en el botón seleccionado
            drBostrom.position.X = buttons[drBostromButton].position.X - drBostrom.texture.Width - drBostromFromButton.X;
            drBostrom.position.Y = buttons[drBostromButton].position.Y;

            if (Managers.InputManager.Instance.KeyPressed(Keys.Enter))
                buttons[drBostromButton].OnClick();

            else
                foreach (Button btn in buttons)
                    if (btn.IsMouseIn() && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        btn.OnClick();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            backgroundImage.Draw(spriteBatch);

            logoImage.Draw(spriteBatch);
            foreach (Image image in drawings)
                image.Draw(spriteBatch);
            foreach (Button btn in buttons)
                btn.Draw(spriteBatch);

            drBostrom.Draw(spriteBatch);
        }

        /* Mueve los dibujos de la pantalla
         * tales como la energia, el infinito, etc
         * dando uso a los atributos position y speed
         * */
        private void DoTheMove()
        {
            foreach (Image image in drawings)
            {
                image.position.X += image.speed.X;
                image.position.Y += image.speed.Y;

                //COlisión con bordes de la ventana
                if (image.position.X <= 0)
                    image.speed.X *= -1;

                if (image.position.X + image.texture.Width >= Managers.ScreenManager.Instance.dimensions.X)
                    image.speed.X *= -1;

                if (image.position.Y <= 0)
                    image.speed.Y *= -1;

                if (image.position.Y + image.texture.Height >= Managers.ScreenManager.Instance.dimensions.Y)
                    image.speed.Y *= -1;
            }
        }

        /*Controla al DrBostrom de botón en botón con el mouse
         * */
        private void MouseControl()
        {
            int index = 0;
            foreach (Button btn in buttons)
            {
                if (btn.IsMouseIn())
                {
                    drBostromButton = index;
                    break;
                }

                else
                    index++;
            }
        }

        /*Controla al DrBostrom de botón en botón con las arrowkeys
                 * */
        private void KeysControl()
        {
            if (Managers.InputManager.Instance.KeyPressed(Keys.Up) || Managers.InputManager.Instance.KeyPressed(Keys.Left))
                drBostromButton--;

            else if (Managers.InputManager.Instance.KeyPressed(Keys.Down) || Managers.InputManager.Instance.KeyPressed(Keys.Right))
                drBostromButton++;

            if (drBostromButton < 0)
                drBostromButton = 2;

            else if (drBostromButton > 2)
                drBostromButton = 0;
        }

    }
}
