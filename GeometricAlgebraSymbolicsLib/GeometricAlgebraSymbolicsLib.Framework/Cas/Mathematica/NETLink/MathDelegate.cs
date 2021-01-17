using System;
using Wolfram.NETLink.Internal;

namespace Wolfram.NETLink {

/// <summary>
/// Contains the <see cref="MathDelegate.CreateDelegate">CreateDelegate</see> method, which creates
/// delegate objects that invoke a specified <i>Mathematica</i> function.
/// </summary>
/// <remarks>
/// Use this class to easily create <i>Mathematica</i> event handler functions from a .NET language.
/// </remarks>
/// 
public class MathDelegate {

    /// <overloads>
    /// <summary>
    /// Creates a delegate object that calls a specified <i>Mathematica</i> function.
    /// </summary>
    /// <remarks>
    /// This method is comparable to the <see cref="Delegate.CreateDelegate"/> method, except that it
    /// creates delegates whose action is to call a <i>Mathematica</i> function.
    /// <para>
    /// Use this method to easily create <i>Mathematica</i> event handler functions from a .NET language.
    /// It provides similar functionality to the AddEventHandler <i>Mathematica</i> function.
    /// </para>
    /// <code>
    /// // C# example:
    /// myTextBox.KeyPress += (KeyPressEventHandler) MathDelegate.CreateDelegate(typeof(KeyPressEventHandler), "KeyPressHandlerFunction", ml); 
    ///
    /// ' VB example:
    /// AddHandler myTextBox.KeyPress, CType(MathDelegate.CreateDelegate(GetType(KeyPressEventHandler), "KeyPressHandlerFunction", ml), KeyPressEventHandler)
    /// </code>
    /// <para>
    /// The <i>Mathematica</i> function can be the name of a function as a string, or a pure function like "(x = #)&amp;".
    /// The function will be passed all the arguments that the corresponding delegate type takes,
    /// and it should return a result of the same type as the delegate type. If the delegate type returns
    /// void, the result from the <i>Mathematica</i> function is ignored.
    /// </para>
    /// The call is automatically wrapped in NETBlock unless you use the one overload that allows you
    /// to specify otherwise.
    /// </remarks>
    /// </overloads>
    /// 
    /// <summary>
    /// The link that is used is the one given by <see cref="StdLink.Link">StdLink.Link</see>.
    /// </summary>
    /// <param name="delegateType">The type of the delegate to create.</param>
    /// <param name="mFunc">The <i>Mathematica</i> function to evaluate.</param>
    /// <returns>The delegate object.</returns>
    /// 
    public static Delegate CreateDelegate(Type delegateType, string mFunc) {
        return CreateDelegate(delegateType, mFunc, null, false, true);
    }

    /// <summary>
    /// Uses the specified link.
    /// </summary>
    /// <param name="delegateType">The type of the delegate to create.</param>
    /// <param name="mFunc">The <i>Mathematica</i> function to evaluate.</param>
    /// <param name="ml">The link to use.</param>
    /// <returns>The delegate object.</returns>
    /// 
    public static Delegate CreateDelegate(Type delegateType, string mFunc, IKernelLink ml) {
        return CreateDelegate(delegateType, mFunc, ml, false, true);
    }

    /// <summary>
    /// Use this overload in cases where you need to specify advanced behavior.
    /// </summary>
    /// <remarks>
    /// Set callsUnhsare to true if your <i>Mathematica</i> function calls UnshareKernel or UnshareFrontEnd.
    /// Set wrapInNETBlock to false if you do not want the call to be automatically wrapped in NETBlock.
    /// </remarks>
    /// <param name="delegateType">The type of the delegate to create.</param>
    /// <param name="mFunc">The <i>Mathematica</i> function to evaluate.</param>
    /// <param name="ml">The link to use.</param>
    /// <param name="callsUnshare">Whether the <i>Mathematica</i> function calls UnshareKernel or UnshareFrontEnd.</param>
    /// <param name="wrapInNETBlock">Whether to automatically wrap the call to Mathematica in NETBlock.</param>
    /// <returns>The delegate object.</returns>
    /// 
    public static Delegate CreateDelegate(Type delegateType, string mFunc, IKernelLink ml, bool callsUnshare, bool wrapInNETBlock) {
        return Delegate.CreateDelegate(delegateType, DelegateHelper.createDynamicMethod(ml, delegateType, mFunc, -1, callsUnshare, true));
    }

    // So no ctor shows up in docs.
    private MathDelegate() {}
}

}
