﻿using System;

namespace Classes
{
    public class BoardTile
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public CharacterTile CharacterTile { get; set; }
    }
}
