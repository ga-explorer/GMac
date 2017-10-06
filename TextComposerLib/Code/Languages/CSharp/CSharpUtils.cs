using TextComposerLib.Code.SyntaxTree.Expressions;

namespace TextComposerLib.Code.Languages.CSharp
{
    public static class CSharpUtils
    {
        /// <summary>
        /// Most important C# operators
        /// </summary>
        public static class Operators
        {
            public static SteOperatorSpecs MemberAccess { get; private set; }
            public static SteOperatorSpecs ReadThenIncrement { get; private set; }
            public static SteOperatorSpecs ReadThenDecrement { get; private set; }

            public static SteOperatorSpecs UnaryPlus { get; private set; }
            public static SteOperatorSpecs UnaryMinus { get; private set; }
            public static SteOperatorSpecs IncrementThenRead { get; private set; }
            public static SteOperatorSpecs DecrementThenRead { get; private set; }
            public static SteOperatorSpecs BitwiseNot { get; private set; }
            public static SteOperatorSpecs LogicalNot { get; private set; }

            public static SteOperatorSpecs Multiply { get; private set; }
            public static SteOperatorSpecs Divide { get; private set; }
            public static SteOperatorSpecs Remainder { get; private set; }

            public static SteOperatorSpecs Add { get; private set; }
            public static SteOperatorSpecs Subtract { get; private set; }

            public static SteOperatorSpecs ShiftLeft { get; private set; }
            public static SteOperatorSpecs ShiftRight { get; private set; }

            public static SteOperatorSpecs Less { get; private set; }
            public static SteOperatorSpecs More { get; private set; }
            public static SteOperatorSpecs LessOrEqual { get; private set; }
            public static SteOperatorSpecs MoreOrEqual { get; private set; }
            public static SteOperatorSpecs IsOfType { get; private set; }
            public static SteOperatorSpecs AsType { get; private set; }

            public static SteOperatorSpecs Equal { get; private set; }
            public static SteOperatorSpecs NotEqual { get; private set; }

            public static SteOperatorSpecs BitwiseAnd { get; private set; }
            public static SteOperatorSpecs LogicalAnd { get; private set; }

            public static SteOperatorSpecs BitwiseXor { get; private set; }
            public static SteOperatorSpecs LogicalXor { get; private set; }

            public static SteOperatorSpecs BitwiseOr { get; private set; }
            public static SteOperatorSpecs LogicalOr { get; private set; }

            public static SteOperatorSpecs ConditionalAnd { get; private set; }

            public static SteOperatorSpecs ConditionalOr { get; private set; }

            public static SteOperatorSpecs NullCoalescing { get; private set; }

            public static SteOperatorSpecs Conditional { get; private set; }

            public static SteOperatorSpecs Assign { get; private set; }
            public static SteOperatorSpecs AddThenAssign { get; private set; }
            public static SteOperatorSpecs SubtractThenAssign { get; private set; }
            public static SteOperatorSpecs MultiplyThenAssign { get; private set; }
            public static SteOperatorSpecs DivideThenAssign { get; private set; }
            public static SteOperatorSpecs RemainderThenAssign { get; private set; }
            public static SteOperatorSpecs BitwiseAndThenAssign { get; private set; }
            public static SteOperatorSpecs BitwiseXorThenAssign { get; private set; }
            public static SteOperatorSpecs BitwiseOrThenAssign { get; private set; }
            public static SteOperatorSpecs ShiftLeftThenAssign { get; private set; }
            public static SteOperatorSpecs ShiftRightThenAssign { get; private set; }

            static Operators()
            {
                var precedence = 0;

                Assign =
                    new SteOperatorSpecs(" = ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                AddThenAssign =
                    new SteOperatorSpecs(" += ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                SubtractThenAssign =
                    new SteOperatorSpecs(" -= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                MultiplyThenAssign =
                    new SteOperatorSpecs(" *= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                DivideThenAssign =
                    new SteOperatorSpecs(" /= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                RemainderThenAssign =
                    new SteOperatorSpecs(" %= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                BitwiseAndThenAssign =
                    new SteOperatorSpecs(" &= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                BitwiseXorThenAssign =
                    new SteOperatorSpecs(" ^= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                BitwiseOrThenAssign =
                    new SteOperatorSpecs(" |= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                ShiftLeftThenAssign =
                    new SteOperatorSpecs(" <<= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                ShiftRightThenAssign =
                    new SteOperatorSpecs(" >>= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                precedence++;

                Conditional =
                    new SteOperatorSpecs(" ?: ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Right);
                precedence++;

                NullCoalescing =
                    new SteOperatorSpecs(" ?? ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                ConditionalOr =
                    new SteOperatorSpecs(" || ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                ConditionalAnd =
                    new SteOperatorSpecs(" && ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                BitwiseOr =
                    new SteOperatorSpecs(" | ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                LogicalOr =
                    new SteOperatorSpecs(" | ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                BitwiseXor =
                    new SteOperatorSpecs(" ^ ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                LogicalXor =
                    new SteOperatorSpecs(" ^ ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                BitwiseAnd =
                    new SteOperatorSpecs(" & ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                LogicalAnd =
                    new SteOperatorSpecs(" & ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                Equal =
                    new SteOperatorSpecs(" == ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                NotEqual =
                    new SteOperatorSpecs(" != ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                Less =
                    new SteOperatorSpecs(" < ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                More =
                    new SteOperatorSpecs(" > ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                LessOrEqual =
                    new SteOperatorSpecs(" <= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                MoreOrEqual =
                    new SteOperatorSpecs(" >= ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                IsOfType =
                    new SteOperatorSpecs(" is ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                AsType =
                    new SteOperatorSpecs(" as ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                ShiftLeft =
                    new SteOperatorSpecs(" << ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                ShiftRight =
                    new SteOperatorSpecs(" >> ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                Add =
                    new SteOperatorSpecs(" + ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                Subtract =
                    new SteOperatorSpecs(" - ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                Multiply =
                    new SteOperatorSpecs(" * ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                Divide =
                    new SteOperatorSpecs(" / ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                Remainder =
                    new SteOperatorSpecs(" % ", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                precedence++;

                UnaryPlus =
                    new SteOperatorSpecs("+", precedence, TccOperatorPosition.Prefix, TccOperatorAssociation.None);
                UnaryMinus =
                    new SteOperatorSpecs("-", precedence, TccOperatorPosition.Prefix, TccOperatorAssociation.None);
                IncrementThenRead =
                    new SteOperatorSpecs("++", precedence, TccOperatorPosition.Prefix, TccOperatorAssociation.None);
                DecrementThenRead =
                    new SteOperatorSpecs("--", precedence, TccOperatorPosition.Prefix, TccOperatorAssociation.None);
                BitwiseNot =
                    new SteOperatorSpecs("~", precedence, TccOperatorPosition.Prefix, TccOperatorAssociation.None);
                LogicalNot =
                    new SteOperatorSpecs("!", precedence, TccOperatorPosition.Prefix, TccOperatorAssociation.None);
                precedence++;

                MemberAccess =
                    new SteOperatorSpecs(".", precedence, TccOperatorPosition.Infix, TccOperatorAssociation.Left);
                ReadThenIncrement =
                    new SteOperatorSpecs("++", precedence, TccOperatorPosition.Suffix, TccOperatorAssociation.None);
                ReadThenDecrement =
                    new SteOperatorSpecs("--", precedence, TccOperatorPosition.Suffix, TccOperatorAssociation.None);
            }
        }

        
        public static LanguageInfo CSharp4Info { get; private set; }


        static CSharpUtils()
        {
            CSharp4Info = new LanguageInfo("CSharp", "4.0", "C# 4.0");
        }


        public static CSharpCodeGenerator CSharp4CodeGenerator()
        {
            return new CSharpCodeGenerator();
        }

        public static CSharpSyntaxFactory CSharp4SyntaxFactory()
        {
            return new CSharpSyntaxFactory();
        }

        //public static class Modifiers
        //{
        //    public static string[] Public { get; private set; }

        //    public static string[] PublicStatic { get; private set; }

        //    //public static string[] 

            
        //    static Modifiers()
        //    {
        //        Public = new[]
        //        {
        //            TccModifierNames.PublicModifier, TccModifierNames.
        //        };

        //        PublicStatic = new[]
        //        {
        //            TccModifierNames.PublicModifier,
        //            TccModifierNames.StaticModifier
        //        };
        //    }
        //}
    }
}
