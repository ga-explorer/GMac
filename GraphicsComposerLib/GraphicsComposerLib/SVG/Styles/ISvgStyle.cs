using System.Collections.Generic;
using GraphicsComposerLib.SVG.Attributes;
using GraphicsComposerLib.SVG.Styles.Properties;

namespace GraphicsComposerLib.SVG.Styles
{
    public interface ISvgStyle
    {
        IEnumerable<SvgAttributeInfo> PropertyInfos { get; }

        IEnumerable<SvgAttributeInfo> ActivePropertyInfos { get; }

        IEnumerable<SvgStylePropertyValue> ActivePropertyValues { get; }

        string ActivePropertyValuesText { get; }

        SvgStyle BaseStyle { get; }

        bool IsSubStyle { get; }
    }
}