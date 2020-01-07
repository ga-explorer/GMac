using GMac.GMacAST.Expressions;

namespace GMac.GMacAST
{
    public interface IAstObjectWithDatastoreValueAccess : IAstObjectWithExpression
    {
        /// <summary>
        /// The AST datastore value access of this object
        /// </summary>
        AstDatastoreValueAccess DatastoreValueAccess { get; }
    }
}