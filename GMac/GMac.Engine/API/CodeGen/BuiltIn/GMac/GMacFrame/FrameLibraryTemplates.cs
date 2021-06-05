namespace GMac.Engine.API.CodeGen.BuiltIn.GMac.GMacFrame
{
    public sealed partial class FrameLibrary
    {
        #region GMacCode

        internal const string GMacCodeTemplates =
@"
delimiters < >

begin structure_member
<name> : <type>
end structure_member

begin structure
structure <frame>.<name>
(
    <members>
)


end structure

begin constant
constant <frame>.<name> = <value>


end constant

//Unary Single-Expression Macro with Multivector input
begin unary_macro
macro <frame>.<name>(mv : Multivector) : <output_type>
begin
    return <expr>
end


end unary_macro

//Binary Single-Expression Macro with Multivector inputs
begin binary_macro
macro <frame>.<name>(mv1 : Multivector, mv2 : Multivector) : <output_type>
begin
    return <expr>
end


end binary_macro

//General Single-Expression Macro
begin general_macro
macro <frame>.<name>(<inputs>) : <output_type>
begin
    return <expr>
end


end general_macro

//General Multi-command Macro
begin macro
macro <frame>.<name>(<inputs>) : <output_type>
begin
    <commands>
    return <expr>
end


end macro
";

//@"
//delimiters < >
//
//begin factor_struct_member
//    f<num> : <frame>.Multivector
//end factor_struct_member
//
//begin factor_struct
//structure <frame>BladeFactorStruct(
//    <members>
//    )
//end factor_struct
//
//begin factor_macro_step
//let final.f<num> = (inputVectors.f<num> lcp B) lcp B
//let B = final.f<num> lcp B
//
//end factor_macro_step
//
//begin factor_macro
//macro <frame>.Factor<num>(B : Multivector, inputVectors : <frame>BladeFactorStruct) : <frame>BladeFactorStruct
//begin
//    declare final : <frame>BladeFactorStruct
//    
//    <steps>
//    
//    let final.f<num> = B
//    
//    let result = final
//end
//end factor_macro
//
//begin vectors_op_macro
//macro <frame>.VectorsOP(<vop_inputs>) : Multivector
//begin
//    let result = <vop_expr>
//end
//end vectors_op_macro
//
//begin edual_macro
//macro <frame>.EDual(mv : Multivector) : Multivector
//begin
//    let result = mv elcp reverse(I)
//end
//end edual_macro
//
//begin self_egp_macro
//macro <frame>.SelfEGP(mv : Multivector) : Multivector
//begin
//    let result = mv egp mv
//end
//end self_egp_macro
//
//begin egp_dual_macro
//macro <frame>.EGPDual(mv1 : Multivector, mv2 : Multivector) : Multivector
//begin
//    let result = (mv1 egp mv2) elcp reverse(I)
//end
//end egp_dual_macro
//";

        #endregion

    }
}
