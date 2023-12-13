using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Dtos.API.RealState
{
    public class RealEstateDto
    {
        public int Id { get; set; }
        public string IdAgent { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Address { get; set; }
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string TypeOfSaleName { get; set; }
        public string TypeOfRealEstateName { get; set; }
        public List<string> ImprovementName { get; set; }

        #region AgentInfo
        public string AgentName { get; set; } = null!;
        public string AgentEmail { get; set; } = null!;
        #endregion
    }
}
