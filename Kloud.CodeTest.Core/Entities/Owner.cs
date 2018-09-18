using System.Collections.Generic;

namespace Kloud.CodeTest.Core.Entities
{
    /// <summary>
    /// Owner Entity
    /// </summary>
    public class Owner
    {
        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}