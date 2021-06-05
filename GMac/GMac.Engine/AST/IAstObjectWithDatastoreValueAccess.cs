using GMac.Engine.AST.Expressions;

namespace GMac.Engine.AST
{
    public interface IAstObjectWithDatastoreValueAccess : IAstObjectWithExpression
    {
        /// <summary>
        /// The AST datastore value access of this object
        /// </summary>
        AstDatastoreValueAccess DatastoreValueAccess { get; }
    }
}