﻿using System;

namespace SQLGeneration.Parsing
{
    /// <summary>
    /// Provides the methods that must be overridden in order to properly process SQL expressions.
    /// </summary>
    public abstract class SqlResponder
    {
        private readonly SqlGrammar grammar;

        /// <summary>
        /// Initializes a new instance of a SqlResponder.
        /// </summary>
        /// <param name="grammar">The grammar to use.</param>
        protected SqlResponder(SqlGrammar grammar)
        {
            if (grammar == null)
            {
                grammar = new SqlGrammar();
            }
            this.grammar = grammar;
        }

        /// <summary>
        /// Gets the grammar.
        /// </summary>
        protected SqlGrammar Grammar
        {
            get { return grammar; }
        }

        /// <summary>
        /// Extracts expressions from the token stream and calls the corresponding handler.
        /// </summary>
        /// <param name="tokenSource">The source of SQL tokens.</param>
        /// <param name="context">The context to pass among the expressions.</param>
        /// <returns>The results of the parse.</returns>
        protected MatchResult GetResult(ITokenSource tokenSource, object context)
        {
            Parser parser = new Parser(grammar);
            return parser.Parse(SqlGrammar.Start.Name, tokenSource, context);
        }
    }
}
