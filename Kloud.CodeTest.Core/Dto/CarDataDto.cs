using System.Collections.Generic;
using System.Linq;

namespace Kloud.CodeTest.Core.Dto
{
    /// <summary>
    /// CarDataDto DTO
    /// </summary>
    public class CarDataDto
    {
        public string Brand { get; set; }

        public IList<OwnerDataDto> OwnerData { get; set; }

        public IList<string> Owners
        {
            get
            {
                return OwnerData?.OrderBy(e => e.Colour).Select(e => e.Owners).Distinct().ToList();
            }
        }
    }
}