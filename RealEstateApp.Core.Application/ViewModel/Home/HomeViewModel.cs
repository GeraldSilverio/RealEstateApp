using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModel.Home
{
    public class HomeViewModel
    {
        public int RealStatesRegistered { get; set; }
        public int ActiveAgents { get; set; }
        public int ActiveClients { get; set; }
        public int ActiveDevelopers { get; set; }
        public int InActiveAgents { get; set; }
        public int InActiveClients { get; set; }
        public int InActiveDevelopers { get; set; }
    }
}
