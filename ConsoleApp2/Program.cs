using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compare
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public float x;
        public float y;

        public bool Equals(Vector2 other)
        {
            if (x == other.x && y == other.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(Object obj)
        {
            
            if (obj == null || !(obj is Vector2)) // if is not a vector2 or is a null obj then return false because cant be equal to
            {
                return false;
            }

            Vector2 copyObj = (Vector2)obj; // cast the obj to be a vector2 since check above passed
            if (x == copyObj.x && y == copyObj.y) // compare if the objects are the same
                {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }

    public struct Polygon2D
    {
        public Vector2[] vertices;
        public float vertexCount { get { return vertices.Length; } }
    }

    class main
    {
        static void Main()
        {
        }
    }
}
