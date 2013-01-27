﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQLGeneration.Expressions;
using SQLGeneration.Properties;

namespace SQLGeneration
{
    /// <summary>
    /// Builds a string of a select statement.
    /// </summary>
    public class SelectBuilder : ISelectBuilder, IFilteredCommand
    {
        private readonly List<IJoinItem> _from;
        private readonly List<IProjectionItem> _projection;
        private readonly FilterGroup _where;
        private readonly List<OrderBy> _orderBy;
        private readonly List<IGroupByItem> _groupBy;
        private readonly FilterGroup _having;

        /// <summary>
        /// Initializes a new instance of a QueryBuilder.
        /// </summary>
        public SelectBuilder()
        {
            _from = new List<IJoinItem>();
            _projection = new List<IProjectionItem>();
            _where = new FilterGroup();
            _orderBy = new List<OrderBy>();
            _groupBy = new List<IGroupByItem>();
            _having = new FilterGroup();
        }

        /// <summary>
        /// Gets or sets the alias of the command.
        /// </summary>
        public string Alias
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to return distinct results.
        /// </summary>
        public bool IsDistinct
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the TOP clause.
        /// </summary>
        public Top Top
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new column under the sub-select.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>The column.</returns>
        public Column CreateColumn(string columnName)
        {
            return new Column(this, columnName);
        }

        /// <summary>
        /// Creates a new column under the sub-select with the given alias.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <param name="alias">The alias to give the column.</param>
        /// <returns>The column.</returns>
        public Column CreateColumn(string columnName, string alias)
        {
            return new Column(this, columnName) { Alias = alias };
        }

        /// <summary>
        /// Gets the items that are part of the projection.
        /// </summary>
        public IEnumerable<IProjectionItem> Projection
        {
            get 
            {
                return new ReadOnlyCollection<IProjectionItem>(_projection);
            }
        }

        /// <summary>
        /// Adds a projection item to the projection.
        /// </summary>
        /// <param name="item">The projection item to add.</param>
        /// <returns>The item that was added.</returns>
        public void AddProjection(IProjectionItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _projection.Add(item);
        }

        /// <summary>
        /// Removes the projection item from the projection.
        /// </summary>
        /// <param name="item">The projection item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool RemoveProjection(IProjectionItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return _projection.Remove(item);
        }

        /// <summary>
        /// Gets the tables, joins or sub-queries that are projected from.
        /// </summary>
        public IEnumerable<IJoinItem> From
        {
            get { return new ReadOnlyCollection<IJoinItem>(_from); }
        }

        /// <summary>
        /// Adds the given join item to the from clause.
        /// </summary>
        /// <param name="joinItem">The join item to add.</param>
        public void AddJoinItem(IJoinItem joinItem)
        {
            if (joinItem == null)
            {
                throw new ArgumentNullException("joinItem");
            }
            _from.Add(joinItem);
        }

        /// <summary>
        /// Removes the given join item from the from clause.
        /// </summary>
        /// <param name="joinItem">The join item to remove.</param>
        public bool RemoveJoinItem(IJoinItem joinItem)
        {
            if (joinItem == null)
            {
                throw new ArgumentNullException("joinItem");
            }
            return _from.Remove(joinItem);
        }

        /// <summary>
        /// Gets the items used to sort the results.
        /// </summary>
        public IEnumerable<OrderBy> OrderBy
        {
            get { return new ReadOnlyCollection<OrderBy>(_orderBy); }
        }

        /// <summary>
        /// Adds a sort criteria to the query.
        /// </summary>
        /// <param name="item">The sort criteria to add.</param>
        public void AddOrderBy(OrderBy item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _orderBy.Add(item);
        }

        /// <summary>
        /// Removes the sort criteria from the query.
        /// </summary>
        /// <param name="item">The order by item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool RemoveOrderBy(OrderBy item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return _orderBy.Remove(item);
        }

        /// <summary>
        /// Gets the items that the query is grouped by.
        /// </summary>
        public IEnumerable<IGroupByItem> GroupBy
        {
            get { return new ReadOnlyCollection<IGroupByItem>(_groupBy); }
        }

        /// <summary>
        /// Adds the item to the group by clause of the query.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddGroupBy(IGroupByItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _groupBy.Add(item);
        }

        /// <summary>
        /// Removes the item from the group by clause of the query.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool RemoveGroupBy(IGroupByItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return _groupBy.Remove(item);
        }

        /// <summary>
        /// Gets the filters in the filter group.
        /// </summary>
        public IEnumerable<IFilter> Where
        {
            get { return _where.Filters; }
        }

        /// <summary>
        /// Adds the filter to the where clause.
        /// </summary>
        /// <param name="filter">The filter to add.</param>
        public void AddWhere(IFilter filter)
        {
            _where.AddFilter(filter);
        }

        /// <summary>
        /// Removes the filter from the where clause.
        /// </summary>
        /// <param name="filter">The filter to remove.</param>
        /// <returns>True if the filter was removed; otherwise, false.</returns>
        public bool RemoveWhere(IFilter filter)
        {
            return _where.RemoveFilter(filter);
        }

        /// <summary>
        /// Gets the filters in the having clause.
        /// </summary>
        public IEnumerable<IFilter> Having
        {
            get { return _having.Filters; }
        }

        /// <summary>
        /// Adds the filter to the having clause.
        /// </summary>
        /// <param name="filter">The filter to add.</param>
        public void AddHaving(IFilter filter)
        {
            _having.AddFilter(filter);
        }

        /// <summary>
        /// Removes the filter from the having clause.
        /// </summary>
        /// <param name="filter">The filter to remove.</param>
        /// <returns>True if the filter was removed; otherwise, false.</returns>
        public bool RemoveHaving(IFilter filter)
        {
            return _having.RemoveFilter(filter);
        }

        /// <summary>
        /// Gets the SQL that represents the query.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        public IExpressionItem GetCommandExpression(CommandOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            options = options.Clone();
            options.IsSelect = true;
            options.IsInsert = false;
            options.IsUpdate = false;
            options.IsDelete = false;
            return getCommandExpression(options);
        }

        private IExpressionItem getCommandExpression(CommandOptions options)
        {
            if (_projection.Count == 0)
            {
                throw new SQLGenerationException(Resources.NoProjections);
            }
            Expression expression = new Expression();
            expression.AddItem(new Token("SELECT"));
            if (IsDistinct)
            {
                expression.AddItem(new Token("DISTINCT"));
            }
            if (Top != null)
            {
                expression.AddItem(Top.GetTopExpression(options));
            }
            ProjectionItemFormatter projectionFormatter = new ProjectionItemFormatter(options);
            IEnumerable<IExpressionItem> projections = _projection.Select(item => projectionFormatter.GetDeclaration(item));
            expression.AddItem(Expression.Join(new Token(","), projections));
            if (_from.Count != 0)
            {
                expression.AddItem(new Token("FROM"));
                IEnumerable<IExpressionItem> froms = _from.Select(joinItem => joinItem.GetDeclarationExpression(options, _where));
                expression.AddItem(Expression.Join(new Token(","), froms));
            }
            if (_where.HasFilters)
            {
                expression.AddItem(new Token("WHERE"));
                expression.AddItem(_where.GetFilterExpression(options));
            }
            if (_orderBy.Count > 0)
            {
                expression.AddItem(new Token("ORDER BY"));
                IEnumerable<IExpressionItem> orderBys = _orderBy.Select(orderBy => orderBy.GetOrderByExpression(options));
                expression.AddItem(Expression.Join(new Token(","), orderBys));
            }
            if (_groupBy.Count > 0)
            {
                expression.AddItem(new Token("GROUP BY"));
                IEnumerable<IExpressionItem> groupBys = _groupBy.Select(groupBy => groupBy.GetGroupByExpression(options));
                expression.AddItem(Expression.Join(new Token(","), groupBys));
            }
            if (_having.HasFilters)
            {
                expression.AddItem(new Token("HAVING"));
                expression.AddItem(_having.GetFilterExpression(options));
            }
            return expression;
        }

        IExpressionItem IProjectionItem.GetProjectionExpression(CommandOptions options)
        {
            return getSelectContent(options);
        }

        IExpressionItem IJoinItem.GetDeclarationExpression(CommandOptions options, FilterGroup where)
        {
            Expression expression = new Expression();
            expression.AddItem(getSelectContent(options));
            if (!String.IsNullOrWhiteSpace(Alias))
            {
                if (options.AliasColumnSourcesUsingAs)
                {
                    expression.AddItem(new Token("AS"));
                }
                expression.AddItem(new Token(Alias));
            }
            return expression;
        }

        IExpressionItem IColumnSource.GetReferenceExpression(CommandOptions options)
        {
            if (String.IsNullOrWhiteSpace(Alias))
            {
                throw new SQLGenerationException(Resources.ReferencedQueryWithoutAlias);
            }
            return new Token(Alias);
        }

        IExpressionItem IFilterItem.GetFilterExpression(CommandOptions options)
        {
            return getSelectContent(options);
        }

        private Expression getSelectContent(CommandOptions options)
        {
            Expression expression = new Expression();
            expression.AddItem(new Token("("));
            expression.AddItem(getCommandExpression(options));
            expression.AddItem(new Token(")"));
            return expression;
        }

        bool IValueProvider.IsQuery
        {
            get { return true; }
        }
    }
}
