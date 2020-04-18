//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System.Drawing;
using System.Windows.Forms;

namespace Wolfram.NETLink.UI {

/// <summary>
/// A PictureBox subclass intended for displaying images of <i>Mathematica</i> graphics and typeset expressions.
/// </summary>
/// <remarks>
/// This class can be used from <i>Mathematica</i> or .NET code. Use it like any other PictureBox, and simply set the
/// <see cref="MathPictureBox.MathCommand">MathCommand</see> property to have the resulting image displayed in the box.
/// <para>
/// To have the MathPictureBox display the result of a computation, set the MathCommand property to a string of
/// <i>Mathematica</i> code (for example, "Plot[x, {x,0,1}]"). Alternatively, you can directly assign an Image to
/// display using the inherited <see cref="PictureBox.Image">Image</see> property. In that case, no computation
/// is done in <i>Mathematica</i> to generate the image. You would choose that technique if you were drawing into
/// an offscreen image from <i>Mathematica</i> code and then placing that Image into the MathPictureBox.
/// </para>
/// </remarks>
/// 
public class MathPictureBox : PictureBox {

    private IKernelLink ml;
    private bool usesFE;
    private string mathCommand;
    private string pictureType;


    /// <summary>
    /// Initializes a new instance of the MathPictureBox class.
    /// </summary>
    /// <remarks>
    /// This is the constructor typically called from <i>Mathematica</i> code. It sets the link to use to be the one back
    /// to <i>Mathematica</i> (given by <see cref="StdLink.Link"/>). You can also use the <see cref="Link"/> property
    /// to set the link later.
    /// </remarks>
    /// 
    public MathPictureBox() {

        // These SetStyle calls enable double-buffered drawing, although this would only be relevant
        // if you were drawing directly onto the surface in the Paint event handler, which is not a typical
        // use for this class.
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);

        SizeMode = PictureBoxSizeMode.CenterImage;
        Link = StdLink.Link;
        UseFrontEnd = true;
        PictureType = "Automatic";
    }


    /// <summary>
    /// Initializes a new instance of the MathPictureBox class.
    /// </summary>
    /// <param name="ml">The link to use for all computations.</param>
    /// 
    public MathPictureBox(IKernelLink ml) {
        Link = ml;
    }
    

    /// <summary>
    /// Sets or gets the link that will be used for computations.
    /// </summary>
    /// 
    public IKernelLink Link {
        get { return ml; }
        set { ml = value; }
    }
    

    /// <summary>
    /// Sets the type of picture that the box should display.
    /// </summary>
    /// <remarks>
    /// The legal values are "Automatic", "GIF", "JPEG", "Metafile", "StandardForm", and "TraditionalForm".
    /// The values "Automatic", "GIF", "JPEG", and "Metafile" specify a graphics format. If you use one of these,
    /// then the <see cref="MathCommand"/> property should be something that evaluates to a Graphics expression (or Graphics3D,
    /// SurfaceGraphics, etc.) It must <bold>evaluate</bold> to a Graphics expression, not merely produce a plot
    /// as a side effect. A common error is to end the code with a semicolon, which causes the expression to
    /// evaluate to Null, not the intended Graphics:
    /// <code>
    /// // BAD
    /// myMathPictureBox.MathCommand = "Plot[x, {x,0,1}];";
    ///     
    /// // GOOD
    /// myMathPictureBox.MathCommand = "Plot[x, {x,0,1}]";
    /// </code>
    /// The "Automatic" setting is the default, which assumes you are trying to display graphics, not typeset
    /// expressions, and it will automatically select the best graphics format. If you want the graphic to
    /// dynamically resize as the MathPictureBox is being resized, you should use the "Metafile" format.
    /// <para>
    /// The values "StandardForm" or "TraditionalForm" indicate that you want the result of the MathCommand to
    /// be typeset in that format and displayed. These settings are not used if you want a <i>Mathematica</i> plot or other
    /// graphic.
    /// </para>
    /// </remarks>
    /// <seealso cref="MathCommand"/>
    /// 
    public string PictureType {
        get { return pictureType; }
        set {
            // Fix case if user got it wrong. Also set PictureBoxSizeMode.
            string lowerCasePictType = value.ToLower();
            switch (lowerCasePictType) {
                case "gif":
                    pictureType = "GIF";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case "jpeg":
                    pictureType = "JPEG";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case "metafile":
                    pictureType = "Metafile";
                    SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case "standardform":
                    pictureType = "StandardForm";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case "traditionalform":
                    pictureType = "TraditionalForm";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                default:
                    // Automatic, and fall-through for bogus values.
                    pictureType = "Automatic";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
            }
        }
    }

    
    /// <summary>
    /// Specifies whether to use the services of the <i>Mathematica</i> notebook front end in rendering the image to display.
    /// </summary>
    /// <remarks>
    /// Using the front end for rendering services will result in better quality graphics, and it will allow expressions
    /// in plot labels and the like to be typeset. The default is true.
    /// <para>
    /// If the front end is used, then depending on various circumstances you might see a special "Mathematica Server"
    /// instance of the front end appear in the Windows taskbar. This separate instance of the front end is used only for
    /// rendering services in the background. It has no user interface and cannot be brought the foreground.
    /// It is completely managed for you by .NET/Link.
    /// </para>
    /// <para>
    /// This setting is ignored if the <see cref="PictureType"/> property is set to "StandardForm" or "TraditionalForm",
    /// as typeset output always requires the services of the front end.
    /// </para>
    /// </remarks>
    /// <seealso cref="MathCommand"/>
    /// <seealso cref="PictureType"/>
    /// 
    public bool UseFrontEnd {
        get { return usesFE; }
        set { usesFE = value; }
    }


    /// <summary>
    /// Specifies the <i>Mathematica</i> command that is used to generate the image to display.
    /// </summary>
    /// <remarks>
    /// For graphics output, this will typically be a plotting command, such as "Plot[x,{x,0,1}]". For
    /// typeset output (i.e., the <see cref="PictureType"/> property is set to "StandardForm" or "TraditionalForm"),
    /// any expression can be given; its result will be typeset and displayed. Note that it is the <i>result</i>
    /// of the expression that is displayed, so do not make the mistake of ending the expression with a
    /// semicolon, as this will make the expression evaluate to Null.
    /// <para>
    /// You might find it more convenient to define the command in <i>Mathematica</i> as a function and then
    /// specify only the function call as the MathCommand. For example, when using this class from
    /// a <i>Mathematica</i> program, you might do this:
    /// <code>
    ///     plotFunc[] := Plot[...complex plot command...];
    ///     myMathPictureBox@MathCommand = "plotFunc[]";
    /// </code>
    /// </para>
    /// </remarks>
    /// <seealso cref="PictureType"/>
    /// 
    public string MathCommand {
        get { return mathCommand; }
        set {
            mathCommand = value;
            if (Link == null) {
                Image = null;
            } else {
                if (Link == StdLink.Link)
                    StdLink.RequestTransaction();
                if (PictureType == "Automatic" || PictureType == "GIF" || PictureType == "JPEG" || PictureType == "Metafile") {
                    bool oldUseFEValue = Link.UseFrontEnd;
                    Link.UseFrontEnd = UseFrontEnd;
                    string oldGraphicsFmt = Link.GraphicsFormat;
                    Link.GraphicsFormat = PictureType;
                    // Graphics rendered by Mathematica often spill out of their bounding box a bit, so we'll compensate
                    // a bit by subtracting from the true width and height of the hosting component.
                    Image im = Link.EvaluateToImage(mathCommand, Width - 4, Height - 4);
                    Image = im;
                    Link.UseFrontEnd = oldUseFEValue;
                    Link.GraphicsFormat = oldGraphicsFmt;
                } else {
                    bool oldUseStdFormValue = Link.TypesetStandardForm;
                    Link.TypesetStandardForm = PictureType != "TraditionalForm";
                    Image = Link.EvaluateToTypeset(mathCommand, Width);
                    Link.TypesetStandardForm = oldUseStdFormValue;
                }
            }
        }
    }


    /// <summary>
    /// If a <see cref="MathCommand"/> is being used to create the image to display, this method causes it to
    /// be recomputed to produce a new image.
    /// </summary>
    /// <remarks>
    /// Call Recompute if your MathCommand depends on values in <i>Mathematica</i> that have changed since
    /// the last time you set the MathCommand property or called Recompute.
    /// </remarks>
    /// <seealso cref="MathCommand"/>
    /// 
    public void Recompute() {
        // Ignore if mathCommand is null (we are in "manual" Image mode).
        if (MathCommand != null)
            MathCommand = MathCommand;
    }

}


// This is an NDOC trick. Define a class with the special name NamespaceDoc and 
// the summary for that class will be used on the summary page for the namespace.
/// <summary>
/// Contains some types useful in Windows Forms applications.
/// </summary>
class NamespaceDoc {}

}
