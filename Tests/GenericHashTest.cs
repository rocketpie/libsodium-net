﻿using System.Text;
using Sodium;
using NUnit.Framework;

namespace Tests
{
  /// <summary>
  /// Tests for the GenericHash class
  /// </summary>
  [TestFixture]
  public class GenericHashTest
  {
    /// <summary>
    /// Verify that the length of the returned key is correct.
    /// </summary>
    [Test]
    public void TestGenerateKey()
    {
      Assert.AreEqual(64, GenericHash.GenerateKey().Length);
    }
    
    /// <summary>
    /// BLAKE2b, 32 bytes, no key
    /// </summary>
    [Test]
    public void GenericHashNoKey()
    {
      var expected = Utilities.HexToBinary("53e27925e5786abe74e6bb7004980a6a38a8da2478efa1b6b2ae73964cfe4876");
      var actual = GenericHash.Hash(Encoding.ASCII.GetBytes("Adam Caudill"), null, 32);
      CollectionAssert.AreEqual(expected, actual);
    }

    /// <summary>
    /// BLAKE2b, 32 bytes, with key
    /// </summary>
    [Test]
    public void GenericHashWithKey()
    {
      var expected = Utilities.HexToBinary("8866267f985204ae511980704ac85ec4936ee535c37541f342976b2cb3ac62fd");
      var actual = GenericHash.Hash("Adam Caudill", "This is a test key", 32);
      CollectionAssert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Generics the hash salt personal.
    /// </summary>
    [Test]
    public void GenericHashSaltPersonalShouldFail()
    {
      byte[] output, key, message, personal, salt;

      salt = Encoding.UTF8.GetBytes(new char[]{
        '5', 'b', '6', 'b', '4', '1', 'e', 'd', '9', 'b', '3', '4', '3', 'f', 'e', '0'
      });
      personal = Encoding.UTF8.GetBytes(new char[]{
        '5', '1', '2', '6', 'f', 'b', '2', 'a', '3', '7', '4', '0', '0', 'd', '2', 'a'
      });

      key = GenericHash.GenerateKey();

      output = null;
      key = GenericHash.GenerateKey();
      message = Encoding.UTF8.GetBytes("this is a message to hash with salt and personal");
      salt = Encoding.UTF8.GetBytes("put some salt");
      personal = Encoding.UTF8.GetBytes("personal test");

      Assert.AreEqual(1, GenericHash.HashSaltPersonal(out output, message, key, salt, personal));

    }
  }
}
