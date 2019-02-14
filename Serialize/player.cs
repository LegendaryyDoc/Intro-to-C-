using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize
{
    [Serializable]
    class player
    {
        public string name = "Dug";
        public float curHealth = 0;
        public float maxHealth = 100;
        public float gold = 325;

        //[NonSerialized]
        public bool isRare = false;
    }
}
