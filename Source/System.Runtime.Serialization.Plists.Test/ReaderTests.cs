﻿//-----------------------------------------------------------------------
// <copyright file="ReaderTests.cs" company="Tasty Codes">
//     Copyright (c) 2011 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------

namespace System.Runtime.Serialization.Plists.Test
{
    using System;
    using System.Collections;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Binary plist reader tests.
    /// </summary>
    [TestClass]
    public class ReaderTests
    {
        /// <summary>
        /// Read object from Calculator.plist tests.
        /// </summary>
        [TestMethod]
        public void ReaderReadObjectCalculator()
        {
            BinaryPlistReader reader = new BinaryPlistReader();
            IDictionary dictionary;

            using (Stream stream = File.OpenRead(Paths.CalculatorPlistPath))
            {
                dictionary = reader.ReadObject(stream);
            }

            Assert.AreEqual(5, dictionary.Count);
            Assert.AreEqual("520 71 228 289 0 0 1440 878 ", dictionary["NSWindow Frame Calc_History_Window"]);
            Assert.AreEqual("368 425 423 283 0 0 1440 878 ", dictionary["NSWindow Frame Calc_Main_Window"]);
            Assert.AreEqual(true, dictionary["Programmer_BinaryIsHidden"]);
            Assert.AreEqual((short)10, dictionary["Programmer_InputMode"]);
            Assert.AreEqual("Scientific", dictionary["ViewDefaultsKey"]);
        }

        /// <summary>
        /// Read object from Nested.plist tests.
        /// </summary>
        [TestMethod]
        public void ReaderReadObjectNested()
        {
            BinaryPlistReader reader = new BinaryPlistReader();
            IDictionary dictionary;

            using (Stream stream = File.OpenRead(Paths.NestedPlistPath))
            {
                dictionary = reader.ReadObject(stream);
            }

            Assert.AreEqual(2, dictionary.Count);
            Assert.IsInstanceOfType(dictionary["Array"], typeof(object[]));
            Assert.IsInstanceOfType(dictionary["Dictionary"], typeof(IDictionary));

            object[] arr = dictionary["Array"] as object[];
            IDictionary dict = dictionary["Dictionary"] as IDictionary;
            Assert.AreEqual(3, arr.Length);
            Assert.AreEqual(2, dictionary.Count);

            Assert.AreEqual("First", arr[0]);
            Assert.AreEqual("Second", arr[1]);
            Assert.AreEqual("Third", arr[2]);

            Assert.AreEqual(1.0000000000000011, dict["Double"]);
            Assert.AreEqual(4, ((object[])dict["Array"]).Length);

            arr = dict["Array"] as object[];
            Assert.AreEqual("1", arr[0]);
            Assert.AreEqual((short)2, arr[1]);
            Assert.AreEqual(true, arr[2]);
            Assert.AreEqual("4", arr[3]);
        }

        /// <summary>
        /// Read object from Types.plist tests.
        /// </summary>
        [TestMethod]
        public void ReaderReadObjectTypes()
        {
            BinaryPlistReader reader = new BinaryPlistReader();
            IDictionary dictionary;

            using (Stream stream = File.OpenRead(Paths.TypesPlistPath))
            {
                dictionary = reader.ReadObject(stream);
            }

            Assert.AreEqual(9, dictionary.Count);
            Assert.AreEqual("Lorem ipsum dolor sit amet, consectetur adipiscing elit imperdiet ornare.", dictionary["LongAscii"]);
            Assert.AreEqual("¡™£¢∞§¶•ªº–≠πøˆ¨¥†®´∑œ", dictionary["Unicode"]);
            Assert.AreEqual(false, dictionary["False"]);
            Assert.IsInstanceOfType(dictionary["Data"], typeof(byte[]));
            Assert.AreEqual(new DateTime(1982, 05, 28, 14, 0, 0, DateTimeKind.Utc).ToLocalTime(), dictionary["Date"]);
            Assert.AreEqual(true, dictionary["True"]);
            Assert.AreEqual(3.14159, dictionary["Pi"]);
            Assert.AreEqual("World", dictionary["Hello"]);
            Assert.AreEqual(72, ((object[])dictionary["LongArray"]).Length);
        }

        /// <summary>
        /// Read Profile2.plist tests.
        /// </summary>
        [TestMethod]
        public void ReaderReadProfile2()
        {
            BinaryPlistReader reader = new BinaryPlistReader();
            IDictionary dictionary;

            using (Stream stream = File.OpenRead(Paths.Profile2PlistPath))
            {
                dictionary = reader.ReadObject(stream);
            }

            Assert.AreEqual(2, dictionary.Count);

            object[] disorders = (object[])dictionary["disorders"];
            object[] educationLevels = (object[])dictionary["educationLevels"];

            Assert.AreEqual(1, disorders.Length);
            Assert.AreEqual(6, educationLevels.Length);

            IDictionary obj = (IDictionary)disorders[0];
            Assert.AreEqual((short)1, obj["id"]);
            Assert.AreEqual(false, obj["isAlias"]);
            Assert.AreEqual("Autism", obj["name"]);
            Assert.AreEqual((short)1, obj["order"]);

            obj = (IDictionary)educationLevels[1];
            Assert.AreEqual((short)2, obj["id"]);
            Assert.AreEqual("Elementary School", obj["name"]);
            Assert.AreEqual((short)2, obj["order"]);

            obj = (IDictionary)educationLevels[2];
            Assert.AreEqual((short)3, obj["id"]);
            Assert.AreEqual("Middle School", obj["name"]);
            Assert.AreEqual((short)3, obj["order"]);

            obj = (IDictionary)educationLevels[3];
            Assert.AreEqual((short)4, obj["id"]);
            Assert.AreEqual("High School", obj["name"]);
            Assert.AreEqual((short)4, obj["order"]);

            obj = (IDictionary)educationLevels[4];
            Assert.AreEqual((short)5, obj["id"]);
            Assert.AreEqual("Post High School", obj["name"]);
            Assert.AreEqual((short)5, obj["order"]);

            obj = (IDictionary)educationLevels[5];
            Assert.AreEqual((short)6, obj["id"]);
            Assert.AreEqual("Higher Education", obj["name"]);
            Assert.AreEqual((short)6, obj["order"]); 
        }
    }
}
