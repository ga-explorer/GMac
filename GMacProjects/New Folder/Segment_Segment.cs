        public bool SegmentSegment(
            double px1, double py1, double pz1,     //First Segment End Point
            double px2, double py2, double pz2,     //First Segment Other End Point
            double px3, double py3, double pz3,     //Second Segment End Point
            double px4, double py4, double pz4,     //Second Segment Other End Point
            double h)                               //Test Distance (must be positive)
        {
            double qx3, qy3, qz3, d;
            double qx4, qy4, qz4;

            //The following test is used for rapid elimination of far segments and can be removed if found to
            //  have much overhead

            //Find distance between lines containing the two segments
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
            double var1013 = -(py1 * px2) + px1 * py2;
            double var1014 = -(pz1 * px2) + px1 * pz2;
            double var1015 = -(pz1 * py2) + py1 * pz2;
            double var1016 = px1 - px2;
            double var1017 = py1 - py2;
            double var1018 = pz1 - pz2;
            double var1019 = -(py3 * px4) + px3 * py4;
            double var1020 = -(pz3 * px4) + px3 * pz4;
            double var1021 = -(pz3 * py4) + py3 * pz4;
            double var1022 = px3 - px4;
            double var1023 = py3 - py4;
            double var1024 = pz3 - pz4;
            d = var1018 * var1019 - var1017 * var1020 + var1016 * var1021 + var1015 * var1022 - var1014 * var1023 + var1013 * var1024;
            #endregion

            //If the distance between the lines is more than h the segments are certainly far away
            if (d > h || d < -h)
                return false;

            //Transform two segments so that the first segment coincides with y-axis
            if (px1 == px2 && py1 == -py2 && pz1 == pz2)
            {
                //This is to handle the special case when the first line segment is parallel to the y-axis
                //  in the oposite direction (180 degrees)

                qx3 = px1 - px3;
                qy3 = py1 - py3;
                qz3 = pz1 - pz3;
                qx4 = px1 - px4;
                qy4 = py1 - py4;
                qz4 = pz1 - pz4;
            }
            else
            {
                //This is to handle all other cases

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
                d = Math.Sqrt(var0001 * var0001 + var0002 * var0002 + var0003 * var0003);
                double var0004 = var0001 / d;
                double var0005 = var0002 / d;
                double var0006 = var0003 / d;
                double var0007 = Math.Sqrt(1 + var0005);
                double var0008 = Math.Sqrt(2);
                double var0018 = -(var0004 / (var0008 * var0007));
                double var0019 = var0006 / (var0008 * var0007);
                double var0023 = -px1 + px3;
                double var0024 = -py1 + py3;
                double var0025 = -pz1 + pz3;
                double var0026 = -px1 + px4;
                double var0027 = -py1 + py4;
                double var0028 = -pz1 + pz4;
                double var0029 = (var0007 * var0023) / var0008 + var0018 * var0024;
                double var0030 = -(var0018 * var0023) + (var0007 * var0024) / var0008 + var0019 * var0025;
                double var0031 = -(var0019 * var0024) + (var0007 * var0025) / var0008;
                double var0032 = var0019 * var0023 + var0018 * var0025;
                qx3 = (var0007 * var0029) / var0008 + var0018 * var0030 + var0019 * var0032;
                qy3 = -(var0018 * var0029) + (var0007 * var0030) / var0008 + var0019 * var0031;
                qz3 = -(var0019 * var0030) + (var0007 * var0031) / var0008 + var0018 * var0032;
                double var0033 = (var0007 * var0026) / var0008 + var0018 * var0027;
                double var0034 = -(var0018 * var0026) + (var0007 * var0027) / var0008 + var0019 * var0028;
                double var0035 = -(var0019 * var0027) + (var0007 * var0028) / var0008;
                double var0036 = var0019 * var0026 + var0018 * var0028;
                qx4 = (var0007 * var0033) / var0008 + var0018 * var0034 + var0019 * var0036;
                qy4 = -(var0018 * var0033) + (var0007 * var0034) / var0008 + var0019 * var0035;
                qz4 = -(var0019 * var0034) + (var0007 * var0035) / var0008 + var0018 * var0036;
                #endregion
            }

            //This part finds if the second line segment intersects an axis-aligned box arround the first segment
            //  which is now alligned with the y-axis. This part is not generated by GMac
            double tx_min, ty_min, tz_min;
            double tx_max, ty_max, tz_max;

            double a = 1.0 / (qx4 - qx3);
            if (a >= 0)
            {
                tx_min = (-h - qx3) * a;
                tx_max = (h - qx3) * a;
            }
            else
            {
                tx_min = (h - qx3) * a;
                tx_max = (-h - qx3) * a;
            }

            double b = 1.0 / (qy4 - qy3);
            if (b >= 0)
            {
                ty_min = (-h - qy3) * b;
                ty_max = (d + h - qy3) * b;
            }
            else
            {
                ty_min = (d + h - qy3) * b;
                ty_max = (-h - qy3) * b;
            }

            double c = 1.0 / (qz4 - qz3);
            if (c >= 0)
            {
                tz_min = (-h - qz3) * c;
                tz_max = (h - qz3) * c;
            }
            else
            {
                tz_min = (h - qz3) * c;
                tz_max = (-h - qz3) * c;
            }

            double t0, t1;

            // find largest entering t value
            if (tx_min > ty_min)
                t0 = tx_min;
            else
                t0 = ty_min;

            if (tz_min > t0)
                t0 = tz_min;

            // find smallest exiting t value
            if (tx_max < ty_max)
                t1 = tx_max;
            else
                t1 = ty_max;

            if (tz_max < t1)
                t1 = tz_max;

            return (t0 < t1 && t1 >= 0);
        }
