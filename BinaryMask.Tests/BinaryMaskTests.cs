using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BinaryMask.Tests
{
    [TestFixture]
    public class BinaryMaskTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_constructor_throws_exception_when_invalid_params()
        {
            var mask = new BinaryMask(-1, BinaryOperation.Src);
        }

        [Test]
        public void Test_constructor_sets_mask_properly()
        {
            var mask1 = new BinaryMask(16, BinaryOperation.Set_1);
            var mask2 = new BinaryMask(16, BinaryOperation.Set_0);

            for (int i = 0; i < 16; i++)
            {
                Assert.IsTrue(mask1[i] == BinaryOperation.Set_1);
                Assert.IsTrue(mask2[i] == BinaryOperation.Set_0);
            }
        }
    }
}
