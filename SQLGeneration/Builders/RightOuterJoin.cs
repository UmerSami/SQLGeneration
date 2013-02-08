﻿using System;
using System.Text;

namespace SQLGeneration.Builders
{
    /// <summary>
    /// Represents an right-outer join in a select statement.
    /// </summary>
    public class RightOuterJoin : FilteredJoin
    {
        /// <summary>
        /// Initializes a new instance of a RightOuterJoin.
        /// </summary>
        /// <param name="leftHand">The left hand item in the join.</param>
        /// <param name="rightHand">The right hand table in the join.</param>
        internal RightOuterJoin(Join leftHand, AliasedSource rightHand)
            : base(leftHand, rightHand)
        {
        }

        /// <summary>
        /// Gets the name of the join type.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        /// <returns>The name of the join type.</returns>
        protected override string GetJoinType(CommandOptions options)
        {
            // { "RIGHT OUTER JOIN" | "RIGHT JOIN" }
            StringBuilder result = new StringBuilder("RIGHT ");
            if (options.VerboseOuterJoin)
            {
                result.Append("OUTER ");
            }
            result.Append("JOIN");
            return result.ToString();
        }
    }
}