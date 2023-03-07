namespace TrainRepairCRUDWebApplication.Models.InnerModels
{
    public class RepairInner
    {
        public int Id { get; set; }
        
        public int? IdTypeRepair { get; set; }
        public string? TypeRepair { get; set; }
        public decimal? Money { get; set; }
        
        public bool? Bonus { get; set; }
        
        public int? BonusPercent { get; set; }
        
        public DateOnly? DateStart { get; set; }
        
        public DateOnly? DateStop { get; set; }
        
        public string? Reason { get; set; }
        
        public int? IdBrigade { get; set; }
        public string? BrigadeName { get; set; }
    }
}
