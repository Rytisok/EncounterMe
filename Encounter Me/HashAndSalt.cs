using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encounter_Me
{
    public struct HashAndSalt
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }

    }
}
