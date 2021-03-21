using DawgSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DawgHelper
    {
        private Dawg<bool> Dawg { get; set; }
        public DawgHelper(Dawg<bool> dawg)
        {
            Dawg = dawg;
        }
    }
}
