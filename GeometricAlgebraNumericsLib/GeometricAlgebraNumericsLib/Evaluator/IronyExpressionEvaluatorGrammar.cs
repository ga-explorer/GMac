namespace GeometricAlgebraNumericsLib.Evaluator
{
//    //class
//    [Language("ExpressionEvaluator", "1.0", "Multi-line expression evaluator")]
//    public class IronyExpressionEvaluatorGrammar : InterpretedLanguageGrammar
//    {
//        public IronyExpressionEvaluatorGrammar()
//            : base(caseSensitive: false)
//        {
//            this.GrammarComments =
//                @"Irony expression evaluator. Case-insensitive. Supports big integers, float data types, variables, assignments,
//arithmetic operations, augmented assignments (+=, -=), inc/dec (++,--), strings with embedded expressions; 
//bool operations &,&&, |, ||; ternary '?:' operator.";

//            // 1. Terminals
//            var number = new NumberLiteral("number");

//            //Let's allow big integers (with unlimited number of digits):
//            number.DefaultIntTypes = new[] { TypeCode.Int32, TypeCode.Int64, NumberLiteral.TypeCodeBigInt };

//            var identifier = new IdentifierTerminal("identifier");

//            var comment = new CommentTerminal("comment", "#", "\n", "\r");
//            //comment must be added to NonGrammarTerminals list; it is not used directly in grammar rules,
//            // so we add it to this list to let Scanner know that it is also a valid terminal. 
//            NonGrammarTerminals.Add(comment);

//            var comma = ToTerm(",");

//            //String literal with embedded expressions  ------------------------------------------------------------------
//            var stringLit = new StringLiteral("string", "\"", StringOptions.AllowsAllEscapes | StringOptions.IsTemplate);
//            stringLit.AddStartEnd("'", StringOptions.AllowsAllEscapes | StringOptions.IsTemplate);
//            stringLit.AstConfig.NodeType = typeof(StringTemplateNode);

//            var expr = new NonTerminal("Expr"); //declare it here to use in template definition 

//            var templateSettings = new StringTemplateSettings(); //by default set to Ruby-style settings 
//            templateSettings.ExpressionRoot = expr; //this defines how to evaluate expressions inside template
//            SnippetRoots.Add(expr);
//            stringLit.AstConfig.Data = templateSettings;
//            //--------------------------------------------------------------------------------------------------------

//            // 2. Non-terminals
//            var term = new NonTerminal("Term");
//            var binExpr = new NonTerminal("BinExpr", typeof(BinaryOperationNode));
//            var parExpr = new NonTerminal("ParExpr");
//            var unExpr = new NonTerminal("UnExpr", typeof(UnaryOperationNode));
//            var ternaryIfExpr = new NonTerminal("TernaryIf", typeof(IfNode));
//            var argList = new NonTerminal("ArgList", typeof(ExpressionListNode));
//            var functionCall = new NonTerminal("FunctionCall", typeof(FunctionCallNode));
//            var memberAccess = new NonTerminal("MemberAccess", typeof(MemberAccessNode));
//            var indexedAccess = new NonTerminal("IndexedAccess", typeof(IndexedAccessNode));
//            var objectRef = new NonTerminal("ObjectRef"); // foo, foo.bar or f['bar']
//            var unOp = new NonTerminal("UnOp");
//            var binOp = new NonTerminal("BinOp", "operator");
//            var prefixIncDec = new NonTerminal("PrefixIncDec", typeof(IncDecNode));
//            var postfixIncDec = new NonTerminal("PostfixIncDec", typeof(IncDecNode));
//            var incDecOp = new NonTerminal("IncDecOp");
//            var assignmentStmt = new NonTerminal("AssignmentStmt", typeof(AssignmentNode));
//            var assignmentOp = new NonTerminal("AssignmentOp", "assignment operator");
//            var statement = new NonTerminal("Statement");
//            var program = new NonTerminal("Program", typeof(StatementListNode));

//            // 3. BNF rules
//            expr.Rule = term | unExpr | binExpr | prefixIncDec | postfixIncDec | ternaryIfExpr;
//            term.Rule = number | parExpr | stringLit | functionCall | identifier | memberAccess | indexedAccess;
//            parExpr.Rule = "(" + expr + ")";
//            unExpr.Rule = unOp + term + ReduceHere();
//            unOp.Rule = ToTerm("+") | "-" | "!";
//            binExpr.Rule = expr + binOp + expr;
//            binOp.Rule = ToTerm("+") | "-" | "*" | "/" | "**" | "==" | "<" | "<=" | ">" | ">=" | "!=" | "&&" | "||" | "&" | "|";
//            prefixIncDec.Rule = incDecOp + identifier;
//            postfixIncDec.Rule = identifier + PreferShiftHere() + incDecOp;
//            incDecOp.Rule = ToTerm("++") | "--";
//            ternaryIfExpr.Rule = expr + "?" + expr + ":" + expr;
//            memberAccess.Rule = expr + PreferShiftHere() + "." + identifier;
//            assignmentStmt.Rule = objectRef + assignmentOp + expr;
//            assignmentOp.Rule = ToTerm("=") | "+=" | "-=" | "*=" | "/=";
//            statement.Rule = assignmentStmt | expr | Empty;
//            argList.Rule = MakeStarRule(argList, comma, expr);
//            functionCall.Rule = expr + PreferShiftHere() + "(" + argList + ")";
//            functionCall.NodeCaptionTemplate = "call #{0}(...)";
//            objectRef.Rule = identifier | memberAccess | indexedAccess;
//            indexedAccess.Rule = expr + PreferShiftHere() + "[" + expr + "]";

//            program.Rule = MakePlusRule(program, NewLine, statement);

//            Root = program;       // Set grammar root

//            // 4. Operators precedence
//            RegisterOperators(10, "?");
//            RegisterOperators(15, "&", "&&", "|", "||");
//            RegisterOperators(20, "==", "<", "<=", ">", ">=", "!=");
//            RegisterOperators(30, "+", "-");
//            RegisterOperators(40, "*", "/");
//            RegisterOperators(50, Associativity.Right, "**");
//            RegisterOperators(60, "!");
//            // For precedence to work, we need to take care of one more thing: BinOp. 
//            //For BinOp which is or-combination of binary operators, we need to either 
//            // 1) mark it transient or 2) set flag TermFlags.InheritPrecedence
//            // We use first option, making it Transient.  

//            // 5. Punctuation and transient terms
//            MarkPunctuation("(", ")", "?", ":", "[", "]");
//            RegisterBracePair("(", ")");
//            RegisterBracePair("[", "]");
//            MarkTransient(term, expr, statement, binOp, unOp, incDecOp, assignmentOp, parExpr, objectRef);

//            // 7. Syntax error reporting
//            MarkNotReported("++", "--");
//            AddToNoReportGroup("(", "++", "--");
//            AddToNoReportGroup(NewLine);
//            AddOperatorReportGroup("operator");
//            AddTermsReportGroup("assignment operator", "=", "+=", "-=", "*=", "/=");

//            //8. Console
//            ConsoleTitle = "Irony Expression Evaluator";
//            ConsoleGreeting =
//                @"Irony Expression Evaluator 

//  Supports variable assignments, arithmetic operators (+, -, *, /),
//    augmented assignments (+=, -=, etc), prefix/postfix operators ++,--, string operations. 
//  Supports big integer arithmetic, string operations.
//  Supports strings with embedded expressions : ""name: #{name}""

//Press Ctrl-C to exit the program at any time.
//";
//            ConsolePrompt = "?";
//            ConsolePromptMoreInput = "?";

//            //9. Language flags. 
//            // Automatically add NewLine before EOF so that our BNF rules work correctly when there's no final line break in source
//            LanguageFlags = LanguageFlags.NewLineBeforeEOF | LanguageFlags.CreateAst | LanguageFlags.SupportsBigInt;
//        }

//        public override LanguageRuntime CreateRuntime(LanguageData language)
//        {
//            return new ExpressionEvaluatorRuntime(language);
//        }

//        #region Running in Grammar Explorer
//        private static IronyExpressionEvaluator _evaluator;
//        public override string RunSample(RunSampleArgs args)
//        {
//            if (_evaluator == null)
//            {
//                _evaluator = new IronyExpressionEvaluator(this);
//                _evaluator.Globals.Add("null", _evaluator.Runtime.NoneValue);
//                _evaluator.Globals.Add("true", true);
//                _evaluator.Globals.Add("false", false);

//            }
//            _evaluator.ClearOutput();
//            //for (int i = 0; i < 1000; i++)  //for perf measurements, to execute 1000 times
//            _evaluator.Evaluate(args.ParsedSample);
//            return _evaluator.GetOutput();
//        }
//        #endregion
//    }
}