using System.Collections.Generic;

namespace Data
{
    public abstract class Target<T>
    {
        public abstract List<T> Values { get; set; }
    }
}
