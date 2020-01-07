namespace GMacBlade.cga5d
{
    public sealed partial class cga5dBlade
    {
        //public static cga5dBlade operator -(cga5dBlade blade)
        //{
        //    if (blade.IsZeroBlade)
        //        return blade;

        //    var coefs = new double[GradeToKvSpaceDim[blade.Grade]];

        //    coefs[0] = -blade.Coefs[0];
        //    coefs[1] = -blade.Coefs[1];
        //    coefs[2] = -blade.Coefs[2];
        //    coefs[3] = -blade.Coefs[3];
        //    coefs[4] = -blade.Coefs[4];
        //    coefs[5] = -blade.Coefs[5];

        //    return new cga5dBlade(blade.Grade, coefs);
        //}

        //public static cga5dBlade operator +(cga5dBlade blade1, cga5dBlade blade2)
        //{
        //    if (blade1.IsZeroBlade)
        //        return blade2;

        //    if (blade2.IsZeroBlade)
        //        return blade1;

        //    if (blade1.Grade != blade2.Grade)
        //        throw new InvalidOperationException("Grade mismatch for blade operarion");

        //    return new cga5dBlade(
        //        blade1.Grade,
        //        blade1.Coefs.Zip(blade2.Coefs, (c1, c2) => c1 + c2).ToArray()
        //        );
        //}

        //public static cga5dBlade operator -(cga5dBlade blade1, cga5dBlade blade2)
        //{
        //    if (blade1.IsZeroBlade)
        //        return -blade2;

        //    if (blade2.IsZeroBlade)
        //        return blade1;

        //    if (blade1.Grade != blade2.Grade)
        //        throw new InvalidOperationException("Grade mismatch for blade operarion");

        //    return new cga5dBlade(
        //        blade1.Grade,
        //        blade1.Coefs.Zip(blade2.Coefs, (c1, c2) => c1 - c2).ToArray()
        //        );
        //}

        //public static cga5dBlade operator *(cga5dBlade blade1, double c2)
        //{
        //    if (blade1.IsZeroBlade)
        //        return blade1;

        //    return new cga5dBlade(
        //        blade1.Grade,
        //        blade1.Coefs.Select(c => c * c2).ToArray()
        //        );
        //}

        //public static cga5dBlade operator *(double c1, cga5dBlade blade2)
        //{
        //    if (blade2.IsZeroBlade)
        //        return blade2;

        //    return new cga5dBlade(
        //        blade2.Grade,
        //        blade2.Coefs.Select(c => c * c1).ToArray()
        //        );
        //}

        //public static cga5dBlade operator /(cga5dBlade blade1, double c2)
        //{
        //    if (blade1.IsZeroBlade)
        //        return blade1;

        //    return new cga5dBlade(
        //        blade1.Grade,
        //        blade1.Coefs.Select(c => c / c2).ToArray()
        //        );
        //}

        //public static bool operator ==(cga5dBlade blade1, cga5dBlade blade2)
        //{
        //    if (ReferenceEquals(blade1, null) || ReferenceEquals(blade2, null))
        //        throw new ArgumentNullException();

        //    if (ReferenceEquals(blade1, blade2))
        //        return true;

        //    if (blade1.IsZeroBlade)
        //        return blade2.IsZero();

        //    if (blade2.IsZeroBlade)
        //        return blade1.IsZero();

        //    if (blade1.Grade != blade2.Grade)
        //        return false;

        //    return
        //        blade1
        //        .Coefs
        //        .Zip(blade2.Coefs, (c1, c2) => c1 - c2)
        //        .Any(c => (c <= -Epsilon || c >= Epsilon)) == false;
        //}

        //public static bool operator !=(cga5dBlade blade1, cga5dBlade blade2)
        //{
        //    if (ReferenceEquals(blade1, null) || ReferenceEquals(blade2, null))
        //        throw new ArgumentNullException();

        //    if (ReferenceEquals(blade1, blade2))
        //        return false;

        //    if (blade1.IsZeroBlade)
        //        return !blade2.IsZero();

        //    if (blade2.IsZeroBlade)
        //        return !blade1.IsZero();

        //    if (blade1.Grade != blade2.Grade)
        //        return true;

        //    return
        //        blade1
        //        .Coefs
        //        .Zip(blade2.Coefs, (c1, c2) => c1 - c2)
        //        .Any(c => (c <= -Epsilon || c >= Epsilon));
        //}
    }
}
