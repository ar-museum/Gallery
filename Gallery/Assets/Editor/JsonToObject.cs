using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;

namespace Tests
{
    public class JsonToObjectTests
    {
        // A Test behaves as an ordinary method
        // Use the Assert class to test conditions
        [Test]
        public void loadJsonTestTitle()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualTitle = '"' + "titlu" + '"' + ": " + exhibit.titlu;
                string expectedTitle = r.ReadLine();

                Assert.AreEqual(actualTitle, expectedTitle);
            }
        }

        [Test]
        public void loadJsonTestDescription()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualDescription = '"' + "descriere" + '"' + ": " + exhibit.descriere;
                string expectedDescription = r.ReadLine();

                Assert.AreEqual(actualDescription, expectedDescription);
            }
        }

        [Test]
        public void loadJsonTestName()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualName = '"' + "denumire" + '"' + ": " + exhibit.denumire;
                string expectedName = r.ReadLine();

                Assert.AreEqual(actualName, expectedName);
            }
        }

        [Test]
        public void loadJsonTestId()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualId = '"' + "media_id" + '"' + ": " + exhibit.media_id;
                string expectedId = r.ReadLine();

                Assert.AreEqual(actualId, expectedId);
            }
        }

        [Test]
        public void loadJsonTestPath()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualPath = '"' + "path" + '"' + ": " + exhibit.path;
                string expectedPath = r.ReadLine();

                Assert.AreEqual(actualPath, expectedPath);
            }
        }

        [Test]
        public void loadJsonTestWidth()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualWidth = '"' + "width" + '"' + ": " + exhibit.width;
                string expectedWidth = r.ReadLine();

                Assert.AreEqual(actualWidth, expectedWidth);
            }
        }

        [Test]
        public void loadJsonTestHeight()
        {
            var json = new JsonToObject();
            ExhibitData exhibit;
            exhibit = json.loadJson();

            using (StreamReader r = new StreamReader("Assets/StreamingAssets/op2.json"))
            {
                string actualHeight = '"' + "height" + '"' + ": " + exhibit.height;
                string expectedHeight = r.ReadLine();

                Assert.AreEqual(actualHeight, expectedHeight);
            }
        }
    }



}