﻿using DawgSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class Globals
    {
        public static Dawg<bool> BoingDawg = Dawg<bool>.Load(File.Open("boingDAWG.bin", FileMode.Open));
        public static Dawg<bool> EnglishDawg = Dawg<bool>.Load(File.Open("englishDawg.bin", FileMode.Open));
        public static char[] GetEnglishCharactersArray()
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }
    }
}