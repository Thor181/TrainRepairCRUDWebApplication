namespace TrainRepairCRUDWebApplication.Models.InnerModels
{
    public class BrigadeInner
    {
        public int Id { get; set; }
        public int? IdWorker { get; set; }
        public string? WorkerName { get; set; }
        public int? IdBrigade { get; set; }
        public string? BrigadeName { get; set; }
    }
}
