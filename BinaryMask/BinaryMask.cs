using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryMask
{
    public class BinaryMask
    {
        private BinaryOperation[] _bits;

        /// <summary>
        /// Bits length
        /// </summary>
        public int Length
        {
            get { return _bits.Length; }
        }

        /// <summary>
        /// Gets or sets single bit operation
        /// </summary>
        /// <param name="index">0-based bit index</param>
        /// <returns></returns>
        public BinaryOperation this[int index]
        {
            get { return _bits[index]; }
            set { _bits[index] = value; }
        }

        /// <summary>
        /// Creates BinaryMask of specified length and filled with default operation
        /// </summary>
        /// <param name="length">Length in bits</param>
        /// <param name="defaultValue">Default operation of all bits</param>
        public BinaryMask(int length, BinaryOperation defaultValue)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length");

            _bits = new BinaryOperation[length];
            for (int i = 0; i < _bits.Length; i++)
                _bits[i] = defaultValue;
        }

        /// <summary>
        /// Copies specific value into the mask
        /// </summary>
        /// <param name="srcValue">Source value representing bits being copied to the mask</param>
        /// <param name="dstStartBit">First bit of the mask that will accept first bit of source value</param>
        /// <param name="length">Number of bits to copy</param>
        public void CopyValueFrom(UInt64 srcValue, int dstStartBit, int length)
        {
            for (int i = 0; i < length; i++)
            {
                UInt64 mask = (UInt64)1 << i;
                _bits[dstStartBit + i] = (srcValue & mask) != 0 ? BinaryOperation.Set_1 : BinaryOperation.Set_0;
            }
        }

        /// <summary>
        /// Applies prepared mask operations
        /// </summary>
        /// <param name="dstValue">Buffer of 16-bit words to which masking will be applied</param>
        /// <param name="dstStartIndex">First bit of the buffer that will be masked</param>
        /// <param name="srcStartIndex">First bit of the mask that applying should start width</param>
        /// <param name="srcLength">Number of bits to mask</param>
        public void ApplyMask(ushort[] dstValue, int dstStartIndex, int srcStartIndex, int srcLength)
        {
            for (int i = 0; i < srcLength; i++)
            {
                int srcIndex = srcStartIndex + i;
                int dstIndex = i / 16;
                int dstBitIndex = i % 16;

                ushort dstBitMask = (ushort)(1 << dstBitIndex);
                ushort dstBitMaskNeg = (ushort)~dstBitMask;

                if (this[srcIndex] == BinaryOperation.Set_1)
                    dstValue[dstIndex] |= dstBitMask;
                else if (this[srcIndex] == BinaryOperation.Set_0)
                    dstValue[dstIndex] &= dstBitMaskNeg;
                else if (this[srcIndex] == BinaryOperation.NotSrc)
                    dstValue[dstIndex] ^= dstBitMask;
            }
        }

        /// <summary>
        /// Converts mask to string representation in MSB ... LSB order where:
        /// '1' means set logical '1',
        /// '2' means set logical '2',
        /// 'x' means set source bit
        /// '^' means set negation of source bit
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var operation in _bits.Reverse())
                if (operation == BinaryOperation.NotSrc)
                    sb.Append("^");
                else if (operation == BinaryOperation.Src)
                    sb.Append("x");
                else if (operation == BinaryOperation.Set_1)
                    sb.Append("1");
                else if (operation == BinaryOperation.Set_0)
                    sb.Append("0");

            return sb.ToString();
        }
    }
}
