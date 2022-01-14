using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TCL_Framework.Interfaces;
using TCL_Framework.Attributes;

namespace TCL_Framework.TCLPostgreSQL
{
    public class TCLPostgreSQLSelectQuery<T> : TCLPostgreSQLQuery, IWherable<T>, IGroupable<T>, IHavingable<T>, IRunable<T> where T : new()
    {
        private TCLPostgreSQLSelectQuery(string connectionString) : base(connectionString)
        {
            TCLPostgreSQLMapper mapper = new TCLPostgreSQLMapper();

            query = "SELECT";

            foreach (var column in mapper.GetColumns<T>())
                query = $"{query} {column.Name},";

            query = query.Substring(0, query.Length - 1);

            query = $"{query} from {mapper.GetTableName<T>()}";
        }

        public static IWherable<T> Create(string connectionString)
        {
            return new TCLPostgreSQLSelectQuery<T>(connectionString);
        }

        public IGroupable<T> AllRow()
        {
            return this;
        }

        public IHavingable<T> GroupBy(string columnNames)
        {
            query = $"{query} GROUP BY {columnNames}";
            return this;
        }

        public IRunable<T> Having(Expression<Func<T, bool>> expression)
        {
            var condition = GetValueAsString(expression.Body);
            query = $"{query} HAVING {condition}";
            return this;
        }

        public List<T> Run()
        {
            return ExecuteQuery<T>();
        }

        public IGroupable<T> Where(Expression<Func<T, bool>> expression)
        {
            var condition = GetValueAsString(expression.Body);
            query = $"{query} WHERE {condition}";
            return this;
        }

        private static string GetValueAsString(Expression expression)
        {
            var value = "";
            var equality = "";
            var left = GetLeftNode(expression);
            var right = GetRightNode(expression);
            if (expression.NodeType == ExpressionType.Equal)
            {
                equality = "=";
            }
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                equality = "AND";
            }
            if (expression.NodeType == ExpressionType.OrElse)
            {
                equality = "OR";
            }
            if (expression.NodeType == ExpressionType.NotEqual)
            {
                equality = "<>";
            }
            if (left is MemberExpression)
            {
                var leftMem = left as MemberExpression;
                value = string.Format("({0}{1}'{2}')", leftMem.Member.Name, equality, "{0}");
            }
            if (right is ConstantExpression)
            {
                var rightConst = right as ConstantExpression;
                value = string.Format(value, rightConst.Value);
            }
            if (right is MemberExpression)
            {
                var rightMem = right as MemberExpression;
                var rightConst = rightMem.Expression as ConstantExpression;
                var member = rightMem.Member.DeclaringType;
                var type = rightMem.Member.MemberType;
                var val = member.GetField(rightMem.Member.Name).GetValue(rightConst.Value);
                value = string.Format(value, val);
            }
            if (value == "")
            {
                var leftVal = GetValueAsString(left);
                var rigthVal = GetValueAsString(right);
                value = string.Format("({0} {1} {2})", leftVal, equality, rigthVal);
            }
            return value;
        }

        private static Expression GetLeftNode(Expression expression)
        {
            dynamic exp = expression;
            return ((Expression)exp.Left);
        }

        private static Expression GetRightNode(Expression expression)
        {
            dynamic exp = expression;
            return ((Expression)exp.Right);
        }
    }
}
