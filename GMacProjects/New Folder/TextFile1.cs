        public static bool SegmentSegment(
            double px1, double py1, double pz1,     //First Segment End Point
            double px2, double py2, double pz2,     //First Segment Other End Point
            double px3, double py3, double pz3,     //Second Segment End Point
            double px4, double py4, double pz4,     //Second Segment Other End Point
            double h)                               //Test Distance (must be positive)
        {
            double qx3, qy3, qz3, d;
            double qx4, qy4, qz4;

            #region GMac : LineDistance
            //GMac.Bind("p1.e1", px1);
            //GMac.Bind("p1.e2", py1);
            //GMac.Bind("p1.e3", pz1);
            //GMac.Bind("p2.e1", px2);
            //GMac.Bind("p2.e2", py2);
            //GMac.Bind("p2.e3", pz2);
            //GMac.Bind("p3.e1", px3);
            //GMac.Bind("p3.e2", py3);
            //GMac.Bind("p3.e3", pz3);
            //GMac.Bind("p4.e1", px4);
            //GMac.Bind("p4.e2", py4);
            //GMac.Bind("p4.e3", pz4);
            //GMac.Bind("d.1", d);
            double var0013 = -(py1*px2) + px1*py2;
            double var0014 = -(pz1*px2) + px1*pz2;
            double var0015 = -(pz1*py2) + py1*pz2;
            double var0016 = px1 - px2;
            double var0017 = py1 - py2;
            double var0018 = pz1 - pz2;
            double var0019 = -(py3*px4) + px3*py4;
            double var0020 = -(pz3*px4) + px3*pz4;
            double var0021 = -(pz3*py4) + py3*pz4;
            double var0022 = px3 - px4;
            double var0023 = py3 - py4;
            double var0024 = pz3 - pz4;
            d = var0018*var0019 - var0017*var0020 + var0016*var0021 + var0015*var0022 - var0014*var0023 + var0013*var0024;
            #endregion



            #region GMac : SegmentSegmentIntersect
            //GMac.Bind("p1.e1", px1);
            //GMac.Bind("p1.e2", py1);
            //GMac.Bind("p1.e3", pz1);
            //GMac.Bind("p2.e1", px2);
            //GMac.Bind("p2.e2", py2);
            //GMac.Bind("p2.e3", pz2);
            //GMac.Bind("p3.e1", px3);
            //GMac.Bind("p3.e2", py3);
            //GMac.Bind("p3.e3", pz3);
            //GMac.Bind("p4.e1", px4);
            //GMac.Bind("p4.e2", py4);
            //GMac.Bind("p4.e3", pz4);
            //GMac.Bind("q3.e1", qx3);
            //GMac.Bind("q3.e2", qy3);
            //GMac.Bind("q3.e3", qz3);
            //GMac.Bind("q4.e1", qx4);
            //GMac.Bind("q4.e2", qy4);
            //GMac.Bind("q4.e3", qz4);
            //GMac.Bind("l1.1", d);
            double var0001 = -px1 + px2;
            double var0002 = -py1 + py2;
            double var0003 = -pz1 + pz2;
            d = Math.Sqrt(Math.Pow(var0001,2) + Math.Pow(var0002,2) + Math.Pow(var0003,2));
            double var0004 = var0001/d;
            double var0005 = var0002/d;
            double var0006 = var0003/d;
            double var0018 = -(var0004/(Math.Sqrt(2)*Math.Sqrt(1 + var0005)));
            double var0019 = var0006/(Math.Sqrt(2)*Math.Sqrt(1 + var0005));
            double var0023 = -px1 + px3;
            double var0024 = -py1 + py3;
            double var0025 = -pz1 + pz3;
            double var0026 = -px1 + px4;
            double var0027 = -py1 + py4;
            double var0028 = -pz1 + pz4;
            double var0029 = (Math.Sqrt(1 + var0005)*var0023)/Math.Sqrt(2) + var0018*var0024;
            double var0030 = -(var0018*var0023) + (Math.Sqrt(1 + var0005)*var0024)/Math.Sqrt(2) + var0019*var0025;
            double var0031 = -(var0019*var0024) + (Math.Sqrt(1 + var0005)*var0025)/Math.Sqrt(2);
            double var0032 = var0019*var0023 + var0018*var0025;
            qx3 = (Math.Sqrt(1 + var0005)*var0029)/Math.Sqrt(2) + var0018*var0030 + var0019*var0032;
            qy3 = -(var0018*var0029) + (Math.Sqrt(1 + var0005)*var0030)/Math.Sqrt(2) + var0019*var0031;
            qz3 = -(var0019*var0030) + (Math.Sqrt(1 + var0005)*var0031)/Math.Sqrt(2) + var0018*var0032;
            double var0033 = (Math.Sqrt(1 + var0005)*var0026)/Math.Sqrt(2) + var0018*var0027;
            double var0034 = -(var0018*var0026) + (Math.Sqrt(1 + var0005)*var0027)/Math.Sqrt(2) + var0019*var0028;
            double var0035 = -(var0019*var0027) + (Math.Sqrt(1 + var0005)*var0028)/Math.Sqrt(2);
            double var0036 = var0019*var0026 + var0018*var0028;
            qx4 = (Math.Sqrt(1 + var0005)*var0033)/Math.Sqrt(2) + var0018*var0034 + var0019*var0036;
            qy4 = -(var0018*var0033) + (Math.Sqrt(1 + var0005)*var0034)/Math.Sqrt(2) + var0019*var0035;
            qz4 = -(var0019*var0034) + (Math.Sqrt(1 + var0005)*var0035)/Math.Sqrt(2) + var0018*var0036;
            #endregion







            if ((qy3 < 0 && qy4 < 0) || (qy3 > d + h && qy4 > d + h))
                return false;

            return false;
        }
