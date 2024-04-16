using Domain.Model;

namespace DataModel.Model
{
    public class PeriodDataModel
    {
        public long Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public PeriodDataModel() { }

        public PeriodDataModel(Period period)
        {
            Id = period.Id;
            StartDate = period.StartDate;
            EndDate = period.EndDate;
        }
    }
}