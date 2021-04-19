using ClassLibrary;
using DawgSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnitTests
{
    [TestClass]
    public class Dawg_Tests
    {
        //https://boardgames.stackexchange.com/questions/38366/latest-collins-scrabble-words-list-in-text-file
        [TestMethod]
        [Ignore]
        public void BoingDawg_Generate()
        {
            const string textFile = "boing_crosschecks.txt";
            //const string textFile = "englishWords.txt";

            const string binFile = "boingDAWG.bin";
            //const string binFile = "englishDawg.bin";

            string fileContents = File.ReadAllText(textFile);
            List<string> boingWords = Regex.Matches(fileContents, "\\w+").Select(m => m.Value).ToList();

            DawgBuilder<bool> dawgBuilder = new();
            foreach (string word in boingWords) dawgBuilder.Insert(word, true);

            Dawg<bool> dawg = dawgBuilder.BuildDawg(); // Computer is working.  Please wait ...

            using (FileStream file = File.Create(binFile)) dawg.SaveTo(file);

            //Now read the file back in and check if a particular word is in the dictionary:
            Dawg<bool> dawg2 = Dawg<bool>.Load(File.Open(binFile, FileMode.Open));
        }

        [TestMethod]
        public void BoingDawg_ReadDawg_CountMustBe64()
        {
            Dawg<bool> boingDawg = UnitTestGlobals.LoadBoingDawgFile();
            Assert.IsTrue(boingDawg.Count() == 64);
        }
    }
}
