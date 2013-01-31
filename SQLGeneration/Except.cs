﻿using System;
using SQLGeneration.Expressions;

namespace SQLGeneration
{
    /// <summary>
    /// Removes the items returned by the second query from the first query.
    /// </summary>
    public class Except : SelectCombiner
    {
        /// <summary>
        /// Initializes a new instance of a Except.
        /// </summary>
        /// <param name="leftHand">The left hand SELECT command.</param>
        /// <param name="rightHand">The right hand SELECT command.</param>
        public Except(ISelectBuilder leftHand, ISelectBuilder rightHand)
            : base(leftHand, rightHand)
        {
        }

        /// <summary>
        /// Retrieves the text used to combine two queries.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        /// <returns>The text used to combine two queries.</returns>
        protected override Token GetCombinationName(CommandOptions options)
        {
            // "EXCEPT"
            return new Token("EXCEPT", TokenType.SelectCombiner);
        }
    }
}
