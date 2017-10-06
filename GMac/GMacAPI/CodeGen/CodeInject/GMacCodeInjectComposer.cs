namespace GMac.GMacAPI.CodeGen.CodeInject
{
    //public class GMacCodeInjectComposer : ICodeInjectSource
    //{
    //    private readonly Regex _whitespaceGMacRegex = new Regex(@"^\s+", RegexOptions.IgnoreCase);
    //    private readonly Regex _beginGMacRegex = new Regex(@"\bbegin\s+gmac\s+\b", RegexOptions.IgnoreCase);
    //    private readonly Regex _endGMacRegex = new Regex(@"\bend\s+gmac\s*$", RegexOptions.IgnoreCase);


    //    public string ProgressSourceId
    //    {
    //        get { return "GMac Code Inject Composer"; }
    //    }

    //    public ProgressComposer Progress { get; set; }



    //    public CodeLineType GetCodeLineType(string codeLine)
    //    {
    //        var m = _beginGMacRegex.Match(codeLine);

    //        if (m.Success) return CodeLineType.BeginInjectBlock;

    //        m = _endGMacRegex.Match(codeLine);

    //        return m.Success ? CodeLineType.EndInjectBlock : CodeLineType.Fixed;
    //    }

    //    public RcSlotBlock CreateSlotBlock(string codeLine)
    //    {
    //        var m = _whitespaceGMacRegex.Match(codeLine);

    //        var leadingWhitespace = m.Success ? codeLine.Substring(0, m.Length) : "";

    //        m = _beginGMacRegex.Match(codeLine);

    //        var linePrefix = m.Index > 0 ? codeLine.Substring(0, m.Index) : "";

    //        var slotTextExpr = codeLine.Substring(m.Index + m.Length).Trim();

    //        return new GMacCodeSlot()
    //        {
    //            BeginBlockLine = codeLine,
    //            LeadingWhitespace = leadingWhitespace,
    //            EndBlockLine = linePrefix + "End GMac",
    //            SlotExpression = TextExpressionParser.ParseToTextExpression(slotTextExpr)
    //        };
    //    }

    //    public void InjectCode(LinearComposer textComposer, RcSlotBlock codeBlock)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
