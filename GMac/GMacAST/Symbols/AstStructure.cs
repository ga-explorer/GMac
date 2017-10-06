using System.Collections.Generic;
using System.Linq;
using GMac.GMacAST.Expressions;
using GMac.GMacCompiler.Semantic.AST;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Symbol;

namespace GMac.GMacAST.Symbols
{
    public sealed class AstStructure : AstSymbol, IAstObjectWithType
    {
        #region Static members
        #endregion


        internal GMacStructure AssociatedStructure { get; }

        internal override LanguageSymbol AssociatedSymbol => AssociatedStructure;

        public override bool IsValidStructure => AssociatedStructure != null;

        public override bool IsValidType => AssociatedStructure != null;

        public AstType GMacType => new AstType(AssociatedStructure);

        public string GMacTypeSignature => AssociatedStructure.TypeSignature;


        /// <summary>
        /// True if the given type is the same as this structure
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public bool IsSameType(AstType typeInfo)
        {
            return AssociatedStructure.IsSameType(typeInfo.AssociatedType);
        }

        /// <summary>
        /// True if the given type is the same as this structure
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public bool IsSameType(AstStructure typeInfo)
        {
            return AssociatedStructure.IsSameType(typeInfo.AssociatedStructure);
        }

        /// <summary>
        /// The default value of this structure type
        /// </summary>
        public AstValueStructure DefaultValue => new AstValueStructure(
            (ValueStructureSparse)
                AssociatedStructure
                    .RootAst
                    .CreateDefaultValue(AssociatedStructure)
            );

        /// <summary>
        /// The data members of this structure
        /// </summary>
        public IEnumerable<AstStructureDataMember> DataMembers
        {
            get
            {
                return AssociatedStructure.DataMembers.Select(item => new AstStructureDataMember(item));
            }
        }

        /// <summary>
        /// The data member names of this structure
        /// </summary>
        public IEnumerable<string> DataMemberNames
        {
            get
            {
                return AssociatedStructure.DataMembers.Select(item => item.ObjectName);
            }
        }

        /// <summary>
        /// The data member types op this structure
        /// </summary>
        public IEnumerable<AstType> DataMemberTypes
        {
            get
            {
                var dict = new Dictionary<string, AstType>();

                foreach (var dataMember in AssociatedStructure.DataMembers)
                {
                    var typeSignature = dataMember.SymbolTypeSignature;

                    if (dict.ContainsKey(typeSignature) == false)
                        dict.Add(typeSignature, new AstType(dataMember.SymbolType));
                }

                return dict.Values;
            }
        }


        internal AstStructure(GMacStructure structure)
        {
            AssociatedStructure = structure;
        }


        /// <summary>
        /// The data members of this structure grouped by type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<AstType, List<AstStructureDataMember>>> GroupDataMembersByType()
        {
            var dict =
                new Dictionary<string, List<AstStructureDataMember>>();

            foreach (var dataMember in AssociatedStructure.DataMembers)
            {
                List<AstStructureDataMember> list;

                var typeSignature = dataMember.SymbolTypeSignature;

                if (dict.TryGetValue(typeSignature, out list) == false)
                {
                    list = new List<AstStructureDataMember>();

                    dict.Add(typeSignature, list);
                }

                list.Add(new AstStructureDataMember(dataMember));
            }

            return 
                dict.Select(
                    pair => new KeyValuePair<AstType, List<AstStructureDataMember>>(
                        pair.Value[0].GMacType,
                        pair.Value
                        )
                    );
        }

        /// <summary>
        /// Find a data member of this structure by its name
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstStructureDataMember DataMember(string accessName)
        {
            SymbolStructureDataMember symbol;

            AssociatedStructure.LookupDataMember(accessName, out symbol);

            return new AstStructureDataMember(symbol);
        }

        /// <summary>
        /// Find a data member type of this structure by its name
        /// </summary>
        /// <param name="accessName"></param>
        /// <returns></returns>
        public AstType DataMemberType(string accessName)
        {
            SymbolStructureDataMember symbol;

            return 
                AssociatedStructure.LookupDataMember(accessName, out symbol) 
                ? new AstType(symbol.SymbolType) 
                : null;
        }
    }
}
