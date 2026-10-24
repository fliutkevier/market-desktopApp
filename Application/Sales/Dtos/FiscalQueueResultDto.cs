namespace Application.Sales.Dtos
{
    public class FiscalQueueResultDto
    {
        public int Processed { get; set; }
        public int Approved { get; set; }
        public int StillPending { get; set; }
    }
}
