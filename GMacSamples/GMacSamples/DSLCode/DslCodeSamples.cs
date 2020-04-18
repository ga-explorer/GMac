namespace GMacSamples.DSLCode
{
    public static class DslCodeSamples
    {
        public static string[] DslCodeStrings = 
            {
                Sample1, 
                Sample2,
                Sample3
            };


        #region DSL Code Sample 1

        public const string Sample1 = @"
namespace common

constant Pi = 'Pi'

constant E = 'E'

macro sin(d : scalar) : scalar
begin
    return 'Sin[$d$]'
end

macro cos(d : scalar) : scalar
begin
    return 'Cos[$d$]'
end

macro sqrt(d : scalar) : scalar
begin
    return 'Sqrt[$d$]'
end

template macro versorInverse( mv : Multivector) : Multivector
begin
    let b = reverse( mv )
    let a = mv gp b
    
    return b / a.#E0#
end

template macro normalize ( mv : Multivector ) : Multivector  
begin 
    return mv / 'Sqrt[$ norm2( mv ) $]'
end 


namespace geometry2d

frame e2d (e1, e2) euclidean

frame cgaOrtho (ep, e1, e2, en) orthonormal '+++-'

    subspace vectors2D = @e1, e2@

    subspace euclidean2D = @ga{e1, e2}@

    subspace bivectors2D = @e1^e2@

frame cga (no, e1, e2, ni) 
    CBM cgaOrtho @'
    {
        {1/2,  0,  0, 1/2},
        {  0,  1,  0,   0},
        {  0,  0,  1,   0},
        { -1,  0,  0,   1}
    }'

    subspace vectors2D = @e1, e2@
    
    subspace bivectors2D = @e1^e2@

    subspace euclidean2D = @ga{e1, e2}@


open common

implement versorInverse, normalize using e2d, cgaOrtho, cga

    
namespace geometry3d

frame e3d (e1, e2, e3) euclidean

frame cgaOrtho (ep, e1, e2, e3, en) orthonormal '++++-'

    subspace vectors3d = @e1, e2, e3@

    subspace bivectors3d = @e1^e2, e1^e3, e2^e3@

    subspace euclidean3d = @ga{e1, e2, e3}@

frame cga (no, e1, e2, e3, ni) 
    CBM cgaOrtho @'
    {
        {1/2,  0,  0,  0, 1/2},
        {  0,  1,  0,  0,   0},
        {  0,  0,  1,  0,   0},
        {  0,  0,  0,  1,   0},
        { -1,  0,  0,  0,   1}
    }'

    subspace vectors3d = @e1, e2, e3@
    
    subspace bivectors3d = @e1^e2, e1^e3, e2^e3@

    subspace euclidean3d = @ga{e1, e2, e3}@


constant cga.I3 = Multivector(#e1^e2^e3# = '1')

constant cga.I3i = Multivector(#e1^e2^e3# = '-1')


open common

implement versorInverse, normalize using e3d, cgaOrtho, cga


structure e3d.ray 
(
    origin : Multivector, 
    direction : Multivector
)

structure e3d.HitInfo 
( 
    Ray : ray, 
    tParameter : scalar,
    HitPoint : Multivector, 
    NormalVector : Multivector 
)


macro cga.ToCgaPoint (point : Multivector) : Multivector 
begin
    let point = point.@vectors3d@
    
    return no + point + (point sp point) / 2 gp ni
end

macro cga.To3DPoint ( p : Multivector ) : Multivector
begin
    return Multivector( #e1# = p.#e1#, #e2# = p.#e2#, #e3# = p.#e3# )
end

macro cga.TranslationVersor (translationVector : Multivector) : Multivector
begin
    return 1 - translationVector.@vectors3d@ gp ni / 2
end

macro cga.RotationVersorUnit (angle : scalar, unitBivector : Multivector) : Multivector
begin
    return cos(angle / 2) - sin(angle / 2) gp unitBivector
end

macro cga.RotationVersor (angle : scalar, bivector : Multivector) : Multivector
begin
    return sin(angle / 2) - cos(angle / 2) gp normalize(bivector)
end

macro cga.ApplyUnitRtor (versor : Multivector, mv : Multivector) : Multivector
begin
    return versor gp mv gp reverse(versor)
end
    
macro cga.ApplyGenlVersor ( versor : Multivector, mv : Multivector) : Multivector
begin
    return versor gp mv gp versorInverse(versor)
end

macro cga.NormalToBivector3D(normal : Multivector) : Multivector
begin
    return normal lcp reverse(I3)
end
    
macro cga.BivectorToNormal3D(bivector : Multivector ) : Multivector
begin
    return bivector lcp I3
end
    

";

        #endregion

        #region DSL Code Sample 2

        public const string Sample2 = @"
//Delimited items:
//  'Sin[2.5]': A mathematica expression
//  frame.&e1^e2& : the basis blade e1^e2 of a frame (this is a multivector)
//  variable.#e1^e2# : the scalar coefficient of the basis blade e1^e2 in a multivector variable (this is a scalar)
//  variable.@e1, e2^e3@ : the sum of two full terms of the basis blades e1 and e1^e3 (this is a multivector)
//  'Sin[$mv1.#e1#$]' : Apply 'Sin[]' to the value of the external GMac expression mv1.#e1#
//  % a % : the target language variable 'a' this is produced as-is in the final generated code

namespace main

constant Zero = 0
//constant Zero1 = 0
constant Pi = 'Pi' 


macro sin(d : scalar) : scalar
begin
    return 'Sin[$d$]'
end

macro cos(d : scalar) : scalar
begin
    return 'Cos[$d$]'
end

macro sqrt(d : scalar) : scalar
begin
    return 'Sqrt[$d$]'
end

//this procedure is generic it can be implemented later on any frame
template macro vinv( inMv : Multivector) : Multivector
begin
    let b = reverse( inMv )
    let a = inMv gp b
    
    return b / a.#G0#
end

//this procedure is generic it can be implemented later on any frame 
template macro div_by_norm ( inMv : Multivector ) : Multivector  
begin 
    return inMv / 'Sqrt[$ norm2( inMv ) $]'
end 


frame e2d (e1, e2) orthogonal '{ 1, 1 }' 

frame e3d (e1, e2, e3) euclidean

constant e3d.Ii = e3d.Multivector(#e1^e2^e3# = '-1') 

structure e3d_Ray ( origin : e3d.Multivector, direction : e3d.Multivector )

structure e3d_HitInfo ( ray : e3d_Ray, hit_point : e3d.Multivector, normal : e3d.Multivector )


frame cga5d_ortho (ep, e1, e2, e3, en) IPM 'DiagonalMatrix[{ 1, 1, 1, 1, -1 }]'

    subspace vectors3D = @e1, e2, e3@

    subspace euclidean3D = @ga{e1, e2, e3}@


frame h3d (e1, e2, e3, e0) orthogonal '{ 1, 1, 1, 1 }'

constant h3d.I3 = h3d.&e1^e2^e3&

frame cga5d (no, e1, e2, e3, ni) CBM cga5d_ortho @'{
        {1/2,  0,  0,  0, 1/2},
        {  0,  1,  0,  0,   0},
        {  0,  0,  1,  0,   0},
        {  0,  0,  0,  1,   0},
        { -1,  0,  0,  0,   1}
    }'

    subspace vectors3D = @e1, e2, e3@
    
    subspace bivectors3D = @e1^e2, e1^e3, e2^e3@

    subspace euclidean3D = @ga{e1, e2, e3}@

constant cga5d.I3 = main.cga5d.Multivector(#e1^e2^e3# = '1')

constant cga5d.I3i = main.cga5d.Multivector(#e1^e2^e3# = '-1')

constant cga5d.Ii = main.cga5d.Multivector(#B11111# = '-1')

constant h3d.Ii = h3d.Multivector(#B1111# = '-1')

breakpoint

implement vinv, div_by_norm using e2d, e3d, cga5d_ortho, h3d, cga5d

/*
association class cga5d_quat on cga5d =
   define variable {q0, qx, qy, qz} on @E0, e1^e2, e2^e3, e1^e3@

association class cga5d_csharp on cga5d =
   define variable 'G#grade#I#index#' on @G4, G5@
   define array 'Vectors' on @G1@
   define dictionary 'Trivectors' on @G3@ keys '#id#'
   define varset 'Quaternion_#membername#' using cga5d_quat
*/

macro cga5d.CreateNormalizedPoint (point : Multivector) : Multivector 
begin
    let point = point.@vectors3D@
    
    return no + point + (point sp point) / 2 gp ni
end

macro cga5d.CreateTranslationVersor (tt : Multivector) : Multivector
begin
    return 1 - tt gp ni / 2
end

macro cga5d.CreateRotationVersorUnit (angle : scalar, I : Multivector) : Multivector
begin
    return cos(angle / 2) - sin(angle / 2) gp I
end

macro cga5d.CreateRotationVersor (angle : scalar, I : Multivector) : Multivector
begin
    return sin(angle / 2) - cos(angle / 2) gp div_by_norm(I)  
end

macro cga5d.ApplyUnitRotorNoInverse (Vt : Multivector, ent1 : Multivector) : Multivector
begin
    return Vt gp ent1 gp reverse(Vt)
end
    
macro cga5d.ApplyUnitRotor ( Vt : Multivector, ent1 : Multivector) : Multivector
begin
    return Vt gp ent1 gp vinv(Vt)
end

macro cga5d.ToPoint3D ( p : Multivector ) : Multivector
begin
    return Multivector( #e1# = p.#e1#, #e2# = p.#e2#, #e3# = p.#e3# )
end

macro cga5d.NormalToBivector3D(N : Multivector) : Multivector
begin
    return N lcp reverse(I3)
end
    
macro cga5d.BivectorToNormal3D(B : Multivector ) : Multivector
begin
    return B lcp cga5d.I3
end
    
macro cga5d.RotateVector3D (
    v : Multivector,
    angle : scalar,
    axis : Multivector
) : Multivector
begin
    let Irot = NormalToBivector3D( axis )
    let rotor = CreateRotationVersor( angle, Irot )
    return ApplyUnitRotor( rotor, v )
end
        
structure PointLineDistance2_struct(
    t0 : scalar, 
    dist : scalar, 
    dist0 : scalar, 
    dist1 : scalar
)

macro cga5d.PointLineDistance2 (
    p0 : Multivector,
    p1 : Multivector,
    q : Multivector
) : PointLineDistance2_struct
begin
    let d = p1 - p0
    let t1 = q - p0
    let t0 = (d sp t1) / (d sp d)
    let dist = norm2(t0 * d + p0 - q)
    let dist0 = norm2(t1)
    let dist1 = norm2(p1 - q)

    //This is one method to fill a structure:
    return PointLineDistance2_struct(t0, dist, dist0, dist1)

    //Another method is:
    //return PointLineDistance2_struct(t0 : t0, dist : dist, dist0 : dist0, dist1 : dist1)
end

namespace main.conformal.GGPRC

open main

macro GGPRC_Line (seed_point : cga5d.Multivector, axis : cga5d.Multivector, u : scalar) : main.cga5d.Multivector
begin
    let versor = cga5d.CreateTranslationVersor(u ^ axis)
 
    let orbit = cga5d.ApplyUnitRotor(versor, cga5d.CreateNormalizedPoint(seed_point))

    return orbit
end

macro GGPRC_Line_Tangent(seed_point : cga5d.Multivector, axis : cga5d.Multivector, u : scalar) : main.cga5d.Multivector
begin
    return GGPRC_Line (seed_point, axis, u)
    
    //let orbit = GGPRC_Line (seed_point, axis, u)

    //let tangent = diff(orbit, u)

    //return tangent
end

macro GGPRC_Circle (center : cga5d.Multivector, axis : cga5d.Multivector, rad_axis : cga5d.Multivector, u : scalar) : main.cga5d.Multivector
begin
    let init_pt1 = cga5d.CreateNormalizedPoint(center)

    let tver1 = cga5d.CreateTranslationVersor(rad_axis)

    let init_pt = cga5d.ApplyUnitRotor(tver1, init_pt1)

    let Irot = cga5d.NormalToBivector3D(axis)

    let rotor = cga5d.CreateRotationVersor (2 * Pi * u, Irot)

    let orbit = cga5d.ApplyUnitRotor (rotor, init_pt)

    return orbit
end

macro GGPRC_Circle_Tangent (center : cga5d.Multivector, axis : cga5d.Multivector, rad_axis : cga5d.Multivector, u : scalar) : main.cga5d.Multivector
begin
    return GGPRC_Circle (center, axis, rad_axis, u)
    
    //let orbit = GGPRC_Circle (center, axis, rad_axis, u)

    //let tangent = diff(orbit, u)

    //return tangent
end

macro GGPRC_Helix (
    center : cga5d.Multivector, 
    axis : cga5d.Multivector, 
    rad_axis : cga5d.Multivector, 
    length : scalar, 
    rotations : scalar, 
    u : scalar
    ) : main.cga5d.Multivector
begin
    let init_pt = cga5d.CreateNormalizedPoint(center)

    let tver1 = cga5d.CreateTranslationVersor (rad_axis)

    let init_pt1 = cga5d.ApplyUnitRotor ( tver1, init_pt)

    let angle = 2 * Pi * u * rotations

    let Irot = cga5d.NormalToBivector3D (axis)

    let rotor = cga5d.CreateRotationVersor ( angle, Irot )

    let final_pt = cga5d.ApplyUnitRotor ( rotor, init_pt1 )

    let tver = cga5d.CreateTranslationVersor ( u gp length gp axis )

    let orbit = cga5d.ApplyUnitRotor ( tver, final_pt )

    return orbit
end

macro GGPRC_Helix_Tangent (
    center : cga5d.Multivector,
    axis : cga5d.Multivector,
    rad_axis : cga5d.Multivector,
    length : scalar,
    rotations : scalar,
    u : scalar
    ) : main.cga5d.Multivector
begin
    return GGPRC_Helix ( center, axis, rad_axis, length, rotations, u )
    
    //let tangent = GGPRC_Helix ( center, axis, rad_axis, length, rotations, u )

    //let tangent = diff(tangent, u)

    //return tangent
end

//binding GGPRC_Helix_Tangent_binding on GGPRC_Helix_Tangent = 
//   target csharp double
//
//   bind center.#e1# with %center0%
//   bind center.#e2# with %center1%
//   bind center.#e3# with %center2%
//   bind axis.#e1# with %axis0%
//   bind axis.#e2# with %axis1%
//   bind axis.#e3# with %axis2%
//   bind rad_axis.#e1# with %rad_axis0%
//   bind rad_axis.#e2# with %rad_axis1%
//   bind rad_axis.#e3# with %rad_axis2%
//   bind length with %length%
//   bind rotations with %rotations%
//   bind u with %u%
//   bind result.#scalar# with %result0%
//   bind result.#no# with %result1%
//   bind result.#e1# with %result2%
//   bind result.#no^e1# with %result3%
//   bind result.#e2# with %result4%
//   bind result.#no^e2# with %result5%
//   bind result.#e1^e2# with %result6%
//   bind result.#no^e1^e2# with %result7%
//   bind result.#e3# with %result8%
//   bind result.#no^e3# with %result9%
//   bind result.#e1^e3# with %result10%
//   bind result.#no^e1^e3# with %result11%
//   bind result.#e2^e3# with %result12%
//   bind result.#no^e2^e3# with %result13%
//   bind result.#e1^e2^e3# with %result14%
//   bind result.#no^e1^e2^e3# with %result15%
//   bind result.#ni# with %result16%
//   bind result.#no^ni# with %result17%
//   bind result.#e1^ni# with %result18%
//   bind result.#no^e1^ni# with %result19%
//   bind result.#e2^ni# with %result20%
//   bind result.#no^e2^ni# with %result21%
//   bind result.#e1^e2^ni# with %result22%
//   bind result.#no^e1^e2^ni# with %result23%
//   bind result.#e3^ni# with %result24%
//   bind result.#no^e3^ni# with %result25%
//   bind result.#e1^e3^ni# with %result26%
//   bind result.#no^e1^e3^ni# with %result27%
//   bind result.#e2^e3^ni# with %result28%
//   bind result.#no^e2^e3^ni# with %result29%
//   bind result.#e1^e2^e3^ni# with %result30%
//   bind result.#no^e1^e2^e3^ni# with %result31%

macro GGPRS_GetNormal (
        orbit : cga5d.Multivector,
        u : scalar,
        v : scalar
    ) : main.cga5d.Multivector
begin
    //return cga5d.BivectorToNormal3D ( diff(orbit, u) ^ diff(orbit, v) )
    
    return cga5d.BivectorToNormal3D ( orbit ^ orbit )
end

macro GGPRS_GetNormalFromTangents 
    (
        du : cga5d.Multivector,
        dv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
    return cga5d.BivectorToNormal3D ( du ^ dv )
end
        
macro GGPRS_Cylinder (
        center : cga5d.Multivector,
        axis : cga5d.Multivector,
        rad_axis : cga5d.Multivector,
        length : scalar,
        u : scalar,
        v : scalar
    ) : main.cga5d.Multivector
begin
        let orbit1 = GGPRC_Circle ( center, axis, rad_axis, u )

        let orbit = GGPRC_Line ( orbit1, axis gp length, v )

        return orbit
end

macro GGPRS_Cylinder_Normal (
        center : cga5d.Multivector,
        axis : cga5d.Multivector,
        rad_axis : cga5d.Multivector,
        length : scalar,
        u : scalar,
        v : scalar
    ) : main.cga5d.Multivector
begin
        let orbit = GGPRS_Cylinder ( center, axis, rad_axis, length, u, v )

        let normal = GGPRS_GetNormal ( orbit, u, v )

        return normal
end

macro GGPRS_Cylinder2 (
        center : cga5d.Multivector,
        axis : cga5d.Multivector,
        rad_axis : cga5d.Multivector,
        length : scalar,
        rotations : scalar,
        u : scalar,
        v : scalar
    ) : main.cga5d.Multivector
begin
        let orbit1 = GGPRC_Helix (
            center = center,
            axis = axis,
            rad_axis = rad_axis,
            length = length,
            rotations = rotations,
            u = u
        )
        
        let angle = 2 gp Pi gp v

        let Irot = cga5d.NormalToBivector3D ( axis )

        let rotor = cga5d.CreateRotationVersor ( angle, Irot )

        let orbit = cga5d.ApplyUnitRotor ( rotor, orbit1 )

        return orbit
end

macro GGPRS_Cylinder2_Normal (
        center : cga5d.Multivector,
        axis : cga5d.Multivector,
        rad_axis : cga5d.Multivector,
        length : scalar, 
        rotations : scalar,
        u : scalar,
        v : scalar
    ) : main.cga5d.Multivector
begin
        let orbit = GGPRS_Cylinder2 (
            center = center,
            axis = axis,
            rad_axis = rad_axis,
            length = length,
            rotations = rotations,
            u = u,
            v = v 
        )

        return GGPRS_GetNormal (
            orbit = orbit,
            u = u,
            v = v
        )
end

namespace main.conformal.twist

open main

open cga5d

macro ApplyTwist (
        rot_axis : cga5d.Multivector,
        tr_origin : cga5d.Multivector,
        angle : scalar,
        tr_param : scalar,
        in_mv : main.cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        let in_mv1 = ApplyUnitRotor ( CreateTranslationVersor ( -tr_origin ), in_mv )
        
        let in_mv2 = ApplyUnitRotor ( 
            CreateRotationVersor ( 
                angle, 
                NormalToBivector3D ( rot_axis ) 
            ), 
            in_mv1 
        )

        return ApplyUnitRotor ( 
            CreateTranslationVersor ( tr_param ^ rot_axis + tr_origin ), 
            in_mv2 
        )
end

macro ApplyUnitTwist (
        rot_axis : cga5d.Multivector,
        tr_origin : cga5d.Multivector,
        angle : scalar,
        tr_param : scalar,
        in_mv : main.cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        let in_mv1 = ApplyUnitRotor ( CreateTranslationVersor ( -tr_origin ), in_mv )
        
        let Irot = NormalToBivector3D ( rot_axis )

        let in_mv2 = ApplyUnitRotorNoInverse ( CreateRotationVersorUnit ( angle, Irot ), in_mv1 )

        return ApplyUnitRotor ( 
            CreateTranslationVersor ( tr_param ^ rot_axis + tr_origin ), 
            in_mv2
        )
end

macro ApplyTwistToPoint3D (
        rot_axis : cga5d.Multivector,
        tr_origin : cga5d.Multivector,
        angle : scalar,
        tr_param : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyTwist (
                rot_axis,
                tr_origin,
                angle,
                tr_param,
                CreateNormalizedPoint( in_mv )
            )
end

macro ApplyUnitTwistToPoint3D (
        rot_axis : cga5d.Multivector,
        tr_origin : cga5d.Multivector,
        angle : scalar,
        tr_param : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyUnitTwist (
            rot_axis = rot_axis,
            tr_origin = tr_origin,
            angle = angle,
            tr_param = tr_param,
            in_mv = CreateNormalizedPoint( in_mv )
        )
end

macro ApplyTwistToPoint3DFullJoin (
        rot_axis : cga5d.Multivector,
        tr_origin : cga5d.Multivector,
        angle : scalar,
        tr_param : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyTwistToPoint3D (
            rot_axis = rot_axis,
            tr_origin = tr_origin,
            angle = angle,
            tr_param = tr_param,
            in_mv = in_mv
        )
end

macro ApplyUnitTwistToPoint3DFullJoin
        (
            rot_axis : cga5d.Multivector,
            tr_origin : cga5d.Multivector,
            angle : scalar,
            tr_param : scalar,
            in_mv : cga5d.Multivector
        ) : main.cga5d.Multivector
begin
        return ApplyUnitTwistToPoint3D (
            rot_axis = rot_axis,
            tr_origin = tr_origin,
            angle = angle,
            tr_param = tr_param,
            in_mv = in_mv
        )
end

macro ApplyTwistToVector3D (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        let Irot = NormalToBivector3D ( rot_axis )

        let tt2 = CreateRotationVersor ( angle, Irot )

        let out_mv1 = ApplyUnitRotor ( tt2, in_mv )

        return out_mv1.@vectors3D@
end

macro ApplyUnitTwistToVector3D (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        let Irot = NormalToBivector3D ( rot_axis )

        let tt2 = CreateRotationVersorUnit ( angle, Irot )

        let out_mv1 = ApplyUnitRotorNoInverse ( tt2, in_mv )

        return out_mv1.@vectors3D@
end

structure Ray(
    pos : cga5d.Multivector,
    dir : cga5d.Multivector
)
    
macro ApplyUnitInvTSRToRay3D (
        scale_factor : scalar,
        tr_vec : cga5d.Multivector,
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_pos : cga5d.Multivector,
        in_dir : cga5d.Multivector
    ) : Ray
begin
        let tt2 = CreateRotationVersorUnit ( -angle, NormalToBivector3D ( rot_axis.@vectors3D@ ) )

        let out_pos = ApplyUnitRotorNoInverse ( tt2, (in_pos - tr_vec) / scale_factor )

        let out_dir = ApplyUnitRotorNoInverse ( tt2, in_dir / scale_factor )

        return Ray(pos = out_pos.@vectors3D@, dir = out_dir.@vectors3D@)
end

macro ApplyTwistToVector3DFullJoin (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyTwistToVector3D (rot_axis, angle, in_mv)
end
        
macro ApplyUnitTwistToVector3DFullJoin 
    (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyUnitTwistToVector3D (rot_axis, angle, in_mv)
end

macro ApplyTwistToBivector3D (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : main.cga5d.Multivector
    ) : main.cga5d.Multivector
begin
    let Irot = NormalToBivector3D ( rot_axis.@vectors3D@ )

        let tt2 = CreateRotationVersor ( angle, Irot )

        let out_mv1 = ApplyUnitRotor ( tt2, in_mv )

        return out_mv1.@bivectors3D@
end
    
macro ApplyUnitTwistToBivector3D (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        let Irot = NormalToBivector3D ( rot_axis )

        let tt2 = CreateRotationVersorUnit ( angle, Irot )

        let out_mv1 = ApplyUnitRotorNoInverse ( tt2, in_mv )

        return out_mv1.@bivectors3D@
end

macro ApplyTwistToBivector3DFullJoin (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyTwistToBivector3D (rot_axis, angle, in_mv)
end

macro ApplyUnitTwistToBivector3DFullJoin (
        rot_axis : cga5d.Multivector,
        angle : scalar,
        in_mv : cga5d.Multivector
    ) : main.cga5d.Multivector
begin
        return ApplyUnitTwistToBivector3D (rot_axis, angle, in_mv)
end

namespace main.conformal

open main

open cga5d

macro RayPlaneIntersect (
        ray_orgn : cga5d.Multivector,
        ray_dir : cga5d.Multivector,
        plane_normal : cga5d.Multivector,
        plane_orgn : cga5d.Multivector
    ) : scalar
begin
    let B = plane_normal lcp I3

    let A1 = B ^ (plane_orgn - ray_orgn)
    let A2 = B ^ ray_dir

    return A1.#e1^e2^e3# / A2.#e1^e2^e3#
end
    
namespace main.geometry

open main
open main.h3d

structure RayTriangleIntersection(
    ray_param : scalar, 
    tri_a : scalar, 
    tri_b : scalar, 
    tri_c : scalar, 
    bc_a : scalar, 
    bc_b : scalar, 
    bc_c : scalar
)
    
macro RayTriangleIntersect (
    ray_orgn : h3d.Multivector,
    ray_dir : h3d.Multivector,
    plane_normal : h3d.Multivector,
    v1 : h3d.Multivector,
    v2 : h3d.Multivector,
    v3 : h3d.Multivector
) : RayTriangleIntersection
begin
    let n_ray_orgn = ray_orgn + h3d.e0

    let n_v1 = v1 + h3d.e0
    let n_v2 = v2 + h3d.e0
    let n_v3 = v3 + h3d.e0

    let ray_line = n_ray_orgn ^ ray_dir

    let edge1 = n_v1 ^ n_v2
    let edge2 = n_v2 ^ n_v3
    let edge3 = n_v3 ^ n_v1

    let Ii = h3d.&e1^e2^e3^e0&

    let tri_a = ray_line ^ edge1 lcp Ii
    let tri_b = ray_line ^ edge2 lcp Ii
    let tri_c = ray_line ^ edge3 lcp Ii

    let B = plane_normal lcp h3d.I3

    let A1 = B ^ (v1 - ray_orgn)
    let A2 = B ^ ray_dir

    let ray_param = A1.#e1^e2^e3# / A2.#e1^e2^e3#

    let tri_sum = tri_a + tri_b + tri_c
    let bc_a = tri_a.#E0# / tri_sum.#E0#
    let bc_b = tri_b.#E0# / tri_sum.#E0#
    let bc_c = tri_c.#E0# / tri_sum.#E0#

    return RayTriangleIntersection( ray_param, tri_a.#E0#, tri_b.#E0#, tri_c.#E0#, bc_a, bc_b, bc_c )
end
        
structure PointTriangleIntersection(
    d : scalar, 
    tri_a : scalar, 
    tri_b : scalar, 
    tri_c : scalar
)

macro PointTriangleIntersect (
    ray_orgn : h3d.Multivector,
    plane_normal : h3d.Multivector,
    v1 : h3d.Multivector,
    v2 : h3d.Multivector,
    v3 : h3d.Multivector
) : PointTriangleIntersection
begin
    let d = (ray_orgn - v1) sp plane_normal
    let ray_line = (ray_orgn + e0) op plane_normal
    let Ii = h3d.&e1^e2^e3^e0&
    let tri_a = ray_line ^ ((v1 + e0) op (v2 + e0)) lcp Ii
    let tri_b = ray_line ^ ((v2 + e0) op (v3 + e0)) lcp Ii
    let tri_c = ray_line ^ ((v3 + e0) op (v1 + e0)) lcp Ii

    return PointTriangleIntersection( d, tri_a.#E0#, tri_b.#E0#, tri_c.#E0# )
end
        
macro LineDistance (
    p1 : h3d.Multivector,
    p2 : h3d.Multivector,
    p3 : h3d.Multivector,
    p4 : h3d.Multivector
) : scalar
begin
    let d = h3d.&((p1 + e0) ^ (p2 + e0) ^ (p3 + e0) ^ (p4 + e0)) lcp (e1^e2^e3^e0)&

    return d.#E0#
end

namespace main

open cga5d

structure SegmentSegmentIntersection(
    l1 : scalar, 
    q3 : cga5d.Multivector, 
    q4 : cga5d.Multivector
)
    
macro SegmentSegmentIntersect (
    p1 : cga5d.Multivector,
    p2 : cga5d.Multivector,
    p3 : cga5d.Multivector,
    p4 : cga5d.Multivector
) : SegmentSegmentIntersection
begin
    let v1 = p2 - p1
    let l1 = 'Sqrt[$norm2(v1)$]'
    let vv1 = v1 / l1
    let n4 = 1 + e2 sp vv1
    let r = (1 + e2 gp vv1) / 'Sqrt[$n4$ / 2]'
    let rr = reverse(r)
    let q3 = r gp (p3 - p1) gp rr
    let q4 = r gp (p4 - p1) gp rr

    return SegmentSegmentIntersection ( l1, q3, q4 )
end
        
structure RaySphereIntersection(
    q : scalar, 
    z2 : scalar
)


namespace main

open h3d

macro RaySphereIntersect (
    ray_orgn : h3d.Multivector,
    ray_dir : h3d.Multivector,
    center : h3d.Multivector,
    radius : scalar
) : RaySphereIntersection
begin
    let q = (center lcp ray_dir + ray_orgn ^ ray_dir) gp ray_dir
    let z2 = radius gp radius - norm2(q - center)

    return RaySphereIntersection (q.#E0#, z2)
end

namespace main

open cga5d

structure AnalyzeRotation_struct(
    axis : cga5d.Multivector, 
    sin_angle : scalar, 
    cos_angle : scalar, 
    scale : scalar
)
    
macro AnalyzeRotation (
    v1 : Multivector,
    v2 : Multivector
) : AnalyzeRotation_struct
begin
    let q = v2 gp v1 / sqrt(norm2(v1))
    let scale = sqrt(norm2(q))
    let qn = q / scale
    let cos_angle = qn.#E0#
    let sin_angle_I = qn.@G2@
    let sin_angle = sqrt(norm2(sin_angle_I))
    let axis = sin_angle_I / sin_angle lcp I3i
    
    return AnalyzeRotation_struct(axis, sin_angle, cos_angle, scale)
end
        
macro AnalyzeRotation3D (
    v1 : Multivector,
    v2 : Multivector
) : AnalyzeRotation_struct
begin
    let mv2 = 1 + v1 gp e1 + v2 gp e2
    let n2 = sqrt(norm2(mv2))
    let qn = mv2 / n2
    let cos_angle = qn.#E0#
    let sin_angle_I = qn.@G2@
    let sin_angle = sqrt(norm2(sin_angle_I))
    let axis = sin_angle_I / sin_angle lcp cga5d.I3i
    
    return AnalyzeRotation_struct(axis, sin_angle, cos_angle, 1)
end        

macro MergeRotation3D (
    axis1 : Multivector,
    angle1 : scalar,
    axis2 : Multivector,
    angle2 : scalar
) : AnalyzeRotation_struct
begin
    let qn = CreateRotationVersorUnit ( 
        angle2, 
        NormalToBivector3D ( axis2 ) 
    ) gp CreateRotationVersorUnit ( 
        angle1, 
        NormalToBivector3D ( axis1 )
    )
    let cos_angle = qn.#E0#
    let sin_angle_I = qn.@G2@
    let sin_angle = sqrt(norm2(sin_angle_I))
    let axis = sin_angle_I / sin_angle lcp cga5d.I3i
    
    return AnalyzeRotation_struct(axis, sin_angle, cos_angle, 1)
end

macro e3d.GetNormalToVectors (
        u : e3d.Multivector, 
        v : e3d.Multivector
    ) : e3d.Multivector
begin
    return (u ^ v) lcp e3d.Ii
end

namespace main.test

open main

macro test1(s1 : scalar, s2 : scalar) : scalar
begin
    let t = s1 * 2
    let s1 = s1 + 5
    let x = s1
    let n = x + 4
    let f = n * 8
    
    return s1 gp s2
end

macro test2(s1 : scalar, s2 : scalar) : scalar
begin
    //let s1 = 'Pi'
    //let s2 = '2 * Pi'
    let t = s1 + s2
    let k = t - 5
    let s = s1 + s2
    let m = s - 5
    
    return k * m
end

//Test SSA form on lhs partial value access operations
macro test3(mv1 : cga5d.Multivector) : scalar
begin
    let t1 = -mv1
    let t1.#E5# = t1.#E0# * 2
    let t2 = t1 gp t1
    let t1.#E2# = t1 sp t1
    let t3 = t1 gp t2
    
    return t3.#E0#
end

macro test4(mv1 : cga5d.Multivector, mv2 : cga5d.Multivector) : cga5d.Multivector
begin
    return mv1 gp mv2
end
";

        #endregion

        #region DSL Code Sample 3

        public const string Sample3 = @"
namespace geometry3d
frame e3d (e1, e2, e3) euclidean
";
        #endregion
    }
}
