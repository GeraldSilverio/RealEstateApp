namespace RealEstateApp.Core.Application.ViewModel.RealEstate
{
    public class RealEstateViewModel
    {
        public int Id { get; set; }
        public string IdAgent { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int IdTypeOfSale { get; set; }
        public int IdTypeOfRealEstate { get; set; }
        public string TypeOfSaleName { get; set; }
        public string TypeOfRealEstateName { get; set; }
        public List<string> Images { get; set; } = null!;
        public int PropertiesImprovementsId { get; set; }
        public List<string> ImprovementName {  get; set; }
        public List<int> ImprovementId {  get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

        #region AgentInfo
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        #endregion
    }
}
