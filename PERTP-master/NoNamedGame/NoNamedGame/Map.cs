﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NoNamedGame
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer;
        public Vector2 TileDimensions;
        private Vector2 mapDimensions;
        public Vector2 MapDimensions { get { return mapDimensions; } }

        public Map()
        {
            Layer = new List<Layer>();
            TileDimensions = Vector2.Zero;
        }

        public void LoadContent()
        {
            //cargo las capas
            foreach (Layer l in Layer)
                l.LoadContent(TileDimensions);

            //cambia el ancho del mapa dependiendo de lo largo que sea
            mapDimensions.X = (Layer[0].tiles.Count * TileDimensions.X) / Layer[0].Tile.Rows.Count;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Layer l in Layer)
                l.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer l in Layer)
                l.Draw(spriteBatch);
        }
    }
}
