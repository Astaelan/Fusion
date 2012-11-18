using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class MetadataToken
    {
        public byte Table = 0;
        public bool IsUserString = false;
        public object Data = null;
    }
}
