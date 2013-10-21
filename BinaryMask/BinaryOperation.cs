using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryMask
{
    /// <summary>
    /// Enum that represents binary operation performed on single bit
    /// </summary>
    public enum BinaryOperation
    {
        /// <summary>
        /// Set to logic '1'
        /// </summary>
        Set_1,

        /// <summary>
        /// Set to logic '0'
        /// </summary>
        Set_0,

        /// <summary>
        /// Set to source value
        /// </summary>
        Src,

        /// <summary>
        /// Set to source value inverted
        /// </summary>
        NotSrc
    }
}
