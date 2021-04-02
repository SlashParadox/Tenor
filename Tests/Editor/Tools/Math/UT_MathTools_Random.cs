using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Tenor.Tools.Math;
using Tenor.TestTools;
using Tenor.Tools.Collection;
using System;

public class UT_MathTools_Random
{
  [TestCategory("Tenor", "Tools", "Math")]
  [Test(Author = "Craig Williams", Description = "A test for generating random ints via RejectionRandom.", TestOf = typeof(Tenor.Tools.Math.Math))]
  public void TestRejectionRandom()
  {
    System.Random randStandard = new System.Random();
    RejectionRandom randRejection = new RejectionRandom();

    byte[] bytes = new byte[4] { 255, 255, 255, 254 };
    Randomization.GetRandomBytes(StandardRandomGenerator.RejectionRandom, bytes);
    //Debug.Log(BitConverter.ToInt32(bytes, 0));
    //Debug.Log(BitConverter.GetBytes(int.MaxValue - 1).Print());
    //return;
    long sumStandard = 0;
    long sumRejection = 0;

    int minValue = int.MinValue;
    int maxValue = int.MaxValue;

    int lowest = int.MaxValue;
    int greatest = int.MinValue;
                         
    for (long i = 0; i < 4000000000L; i++)
    {
      Randomization.GetRandomBytes(StandardRandomGenerator.NETStandard, bytes);
      int add = BitConverter.ToInt32(bytes, 0);
      sumStandard += add;

      if (add == int.MaxValue)
        Debug.Log("Hit!!");

      if (add > greatest)
        greatest = add;

      if (add < lowest)
        lowest = add;

      Randomization.GetRandomBytes(StandardRandomGenerator.NETStandard, bytes);
      add = BitConverter.ToInt32(bytes, 0);
      sumRejection += add;

      if (add == int.MaxValue)
        Debug.Log("Hit!!");

      if (add > greatest)
        greatest = add;
      if (add < lowest)
        lowest = add;
    }

    long expectedAverage = (long)(uint.MaxValue) / 2;
    double avgStandard = sumStandard / 1000000.0;
    double avgRejection = sumRejection / 1000000.0;
    Debug.Log(greatest);
    Debug.Log(lowest);
   // Debug.Log("Standard: " + expectedAverage + ", Rejection: " + avgRejection);

   // Assert.IsTrue(Math.InRangeII(avgStandard, expectedAverage - 50, expectedAverage + 50), "Expected: {0}, Got: {1}", expectedAverage, avgStandard);
   // Assert.IsTrue(Math.InRangeII(avgRejection, expectedAverage - 50, expectedAverage + 50), "Expected: {0}, Got: {1}", expectedAverage, avgRejection);
  }
}
