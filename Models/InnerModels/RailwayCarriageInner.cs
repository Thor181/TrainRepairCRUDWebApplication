namespace TrainRepairCRUDWebApplication.Models.InnerModels
{
    public class RailwayCarriageInner
    {
        public int Id { get; set; }
        public string? Railway { get; set; }
        public int? IdRailway { get; set; }
        public string? Repair { get; set; }
        public int? IdRepair { get; set; } = null!;
        public string? TypeCarriage { get; set; }
        public int? IdTypeCarriage { get; set; }
        public int Year { get; set; }
        public string? Directorate { get; set; }
        public int? IdDirectorate { get; set; }
        public string? Picture { get; set; }
    }
}
