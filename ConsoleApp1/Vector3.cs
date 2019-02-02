using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myVector3
{
    struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3 sum
        {
            get
            {
                Vector3 v2;
                v2.x = 10;
                v2.y = 5;
                v2.z = 1;

                Vector3 v3;
                v3.x = x + v2.x;
                v3.y = y + v2.y;
                v3.z = z + v2.z;

                return v3;
            }
        }

        public double magnitude
        {
            get
            {
                double temp = (x * x) + (y  *y) + (z * z);
                temp = Math.Sqrt(temp); 
                return temp;
            }
        }

        public Vector3 difference
        {
            get
            {
                Vector3 v2;
                v2.x = 10;
                v2.y = 5;
                v2.z = 1;

                Vector3 v3;
                v3.x = x - v2.x;
                v3.y = y - v2.y;
                v3.z = z - v2.z;

                return v3;
            }
        }

        public Vector3 normalize
        {
            get
            {
                float setMag = (float)magnitude;

                x /= setMag;
                y /= setMag;
                z /= setMag;

                Vector3 temp;
                temp.x = x;
                temp.y = y;
                temp.z = z;

                return temp;
            }
        }

        public float dotProduct
        {
            get
            {
                Vector3 temp;
                temp.x = 10;
                temp.y = 5;
                temp.z = 1;

                float dot = (x * temp.x) + (y * temp.y) + (z + temp.z);
                return dot;
            }
        }
    }
}
