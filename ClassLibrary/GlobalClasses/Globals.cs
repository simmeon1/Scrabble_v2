using DawgSharp;
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
        public static Dawg<bool> LoadDawgFile(string path)
        {
            return Dawg<bool>.Load(File.Open(path, FileMode.Open));
        }
    }
}
