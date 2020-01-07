//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;

namespace Wolfram.NETLink {

/// <exclude/>
/// <summary>
/// ComplexClassHandler is a utility class for use by programmers who are creating their own <see cref="IMathLink"/> implementations.
/// </summary>
/// <remarks>
/// You use it by holding an instance of this class in your IMathLink implementation class and delegating to it
/// calls to the IMathLink.ComplexType property and the methods IMathLink.GetComplex and IMathLink.Put where
/// the argument is an object of the complex class.
/// <para>
/// Naturally, this class is used by .NET/Link's own implementation of the IMathLink interface.
/// </para>
/// The type supplied for the ComplexType property must have appropriate members. It must have each of the following:
/// <para>
///     A constructor with one of these signatures:</para>
///     <code>
///         (double re, double im)
///         (float re, float im)
///     </code>
///     OR, a Create(double, double) method  (for F#)
/// 
///     A method with one of the following signatures:
///     <code>
///         double Re()
///         double Real()
///         float Re()
///         float Real()
///     </code>
///     OR, a property or field:
///     <code>
///         double Re
///         double Real
///         double r  (for F#, where it is called a "float")
///         float Re
///         float Real
///     </code>
/// 
///     A method with one of the following signatures:
///     <code>
///         double Im()
///         double Imag()
///         double Imaginary()
///         float Im()
///         float Imag()
///         float Imaginary()
///     </code>
///     OR, a property or field:
///     <code>
///         double Im
///         double Imag
///         double Imaginary
///         double i    (for F#, where it is called a "float")
///         float Im
///         float Imag
///         float Imaginary
///     </code>
/// </remarks>
/// 
public class ComplexClassHandler {

    private Type complexClass;
    // some cached methods.
 
    protected ConstructorInfo complexCtor;
    protected MethodInfo complexCreateMethod; // For F#, which has no ctor for its Complex type
    protected MethodInfo complexReMethod;
    protected MethodInfo complexImMethod;
    protected FieldInfo complexReField;
    protected FieldInfo complexImField;
    protected bool ctorUsesFloat = false;

        
    /// <summary>
    /// Gets or sets the type to be used to represent <i>Mathematica</i> complex numbers in .NET.
    /// </summary>
    ///         
    public Type ComplexType {

        get {
            return complexClass;
        }

        set {
            ctorUsesFloat = false;

            ConstructorInfo newComplexCtor = null;
            MethodInfo newComplexCreateMethod = null;
            MethodInfo newComplexReMethod = null;
            MethodInfo newComplexImMethod = null;
            FieldInfo newComplexReField = null;
            FieldInfo newComplexImField = null;
        
            Type[] argTypes = new Type[]{};

            if (value != null) {
                 newComplexCtor = value.GetConstructor(new Type[]{typeof(double), typeof(double)});
                 if (newComplexCtor == null) {
                     newComplexCtor = value.GetConstructor(new Type[]{typeof(float), typeof(float)});
                     if (newComplexCtor != null)
                         ctorUsesFloat = true;
                }
                // Check for a Create method (this is for F# compatibility).
                if (newComplexCtor == null)
                    newComplexCreateMethod = value.GetMethod("Create", new Type[]{typeof(double), typeof(double)});

                newComplexReMethod = value.GetMethod("Re", argTypes);
                if (newComplexReMethod == null)
                    newComplexReMethod = value.GetMethod("Real", argTypes);
                if (newComplexReMethod == null)
                    newComplexReMethod = value.GetMethod("get_Re", argTypes);
                if (newComplexReMethod == null)
                    newComplexReMethod = value.GetMethod("get_Real", argTypes);
                if (newComplexReMethod == null)
                    newComplexReMethod = value.GetMethod("get_r", argTypes); // For F#
                if (newComplexReMethod == null)
                    newComplexReField = value.GetField("Re");
                if (newComplexReMethod == null && newComplexReField == null)
                    newComplexReField = value.GetField("Real");

                newComplexImMethod = value.GetMethod("Im", argTypes);
                if (newComplexImMethod == null)
                    newComplexImMethod = value.GetMethod("Imag", argTypes);
                if (newComplexImMethod == null)
                    newComplexImMethod = value.GetMethod("Imaginary", argTypes);
                if (newComplexImMethod == null)
                    newComplexImMethod = value.GetMethod("get_Im", argTypes);
                if (newComplexImMethod == null)
                    newComplexImMethod = value.GetMethod("get_Imag", argTypes);
                if (newComplexImMethod == null)
                    newComplexImMethod = value.GetMethod("get_Imaginary", argTypes);
                if (newComplexImMethod == null)
                    newComplexImMethod = value.GetMethod("get_i", argTypes); // For F#
                if (newComplexImMethod == null)
                    newComplexImField = value.GetField("Im");
                if (newComplexImMethod == null && newComplexImField == null)
                    newComplexImField = value.GetField("Imag");
                if (newComplexImMethod == null && newComplexImField == null)
                    newComplexImField = value.GetField("Imaginary");

                if (newComplexCtor == null && newComplexCreateMethod == null ||
                        newComplexReMethod == null && newComplexReField == null ||
                            newComplexImMethod == null && newComplexImField == null)
                    throw new ArgumentException("The specified Type does not have the necessary members to represent complex numbers in .NET/Link.");
            }

            complexClass = value;
            complexCtor = newComplexCtor;
            complexCreateMethod = newComplexCreateMethod;
            complexReMethod = newComplexReMethod;
            complexImMethod = newComplexImMethod;
            complexReField = newComplexReField;
            complexImField = newComplexImField;
        }
    }
  

    /// <summary>
    /// Reads a Complex number from the link and returns an object of the class set as the
    /// <see cref="IMathLink.ComplexType">ComplexType</see>.
    /// </summary>
    ///         
    public object GetComplex(IMathLink ml) {
        
        double re = 0.0;
        double im = 0.0;

        if (ComplexType == null)
            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);

        ExpressionType type = ml.GetNextExpressionType();
        switch (type) {
            case ExpressionType.Integer:
            case ExpressionType.Real: {
                re = ml.GetDouble();
                break;
            }
            case ExpressionType.Complex: {
                ml.CheckFunctionWithArgCount("Complex", 2);
                re = ml.GetDouble();
                im = ml.GetDouble();
                break;
            }
            default:
                throw new MathLinkException(MathLinkException.MLE_BAD_COMPLEX);
        }
        return ConstructComplex(re, im);
    }


    /// <summary>
    /// Writes an instance of the class specified as the <see cref="IMathLink.ComplexType">ComplexType</see> on the link.
    /// </summary>
    ///         
    public void PutComplex(IMathLink ml, object obj) {
        
        if (ComplexType == null)
            throw new MathLinkException(MathLinkException.MLE_NO_COMPLEX);

        double re = 0;
        double im = 0;
        try {
            re = GetRealPart(obj);
            im = GetImaginaryPart(obj);
        } catch (Exception e) {
            // Shouldn't get here.
            ml.PutSymbol("$Failed");
            throw e;
        }
        ml.PutFunction("Complex", 2);
        // Use Put(double), because it has code to handle NaN, infinity.
        ml.Put(re);
        ml.Put(im);
    }
    

    /*************************  Private  ******************************/
   
    private object ConstructComplex(double re, double im) {

        try {
            if (complexCreateMethod == null)
                return ctorUsesFloat ?
                        complexCtor.Invoke(new object[] { (float)re, (float)im }) :
                        complexCtor.Invoke(new object[] { re, im });
            else
                // F# uses a Create method, not a ctor, in its Complex type.
                return complexCreateMethod.Invoke(null, new object[] { re, im });
        } catch (Exception) {
            return null;
        }
    }
    
    private double GetRealPart(object complex) {

        if (complex.GetType() == complexClass) {
            object real;
            if (complexReMethod != null)
                real = complexReMethod.Invoke(complex, null);
            else
                real = complexReField.GetValue(complex);
            return Convert.ToDouble(real);
        } else {
            throw new ArgumentException("Object passed to PutComplex is not of the type set with SetComplexType().");
        }
    }
    
    private double GetImaginaryPart(object complex) {

        if (complex.GetType() == complexClass) {
            object im;
            if (complexImMethod != null)
                im = complexImMethod.Invoke(complex, null);
            else
                im = complexImField.GetValue(complex);
            return Convert.ToDouble(im);
        } else {
            throw new ArgumentException("Object passed to PutComplex is not of the type set with SetComplexType().");
        }
    }

}

}
