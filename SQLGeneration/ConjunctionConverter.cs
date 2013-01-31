﻿using System;
using SQLGeneration.Expressions;
using SQLGeneration.Properties;

namespace SQLGeneration
{
    /// <summary>
    /// Joins filters.
    /// </summary>
    public enum Conjunction
    {
        /// <summary>
        /// Joins two filters with an 'and'.
        /// </summary>
        And,
        /// <summary>
        /// Joins two filters with an 'or'.
        /// </summary>
        Or
    }

    /// <summary>
    /// Converts conjunctions to their string representations.
    /// </summary>
    internal class ConjunctionConverter
    {
        /// <summary>
        /// Initializes a new instance of a ConjunctionConverter.
        /// </summary>
        public ConjunctionConverter()
        {
        }

        /// <summary>
        /// Gets a string representation of the given conjunction.
        /// </summary>
        /// <param name="conjunction">The conjunction to convert to a string.</param>
        /// <returns>The string representation.</returns>
        public Token ToToken(Conjunction conjunction)
        {
            switch (conjunction)
            {
                case Conjunction.And:
                    return new Token("AND", TokenType.Conjunction);
                case Conjunction.Or:
                    return new Token("OR", TokenType.Conjunction);
                default:
                    throw new ArgumentException(Resources.UnknownConjunction, "conjunction");
            }
        }
    }
}
