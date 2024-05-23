namespace Domain.Model
{
    public class Association : IAssociation
    {
        public long Id { get; set; }
        private long _associationId;
        private long _colaboratorId;
        private long _projectId;
        private Period _period;
        private bool _isPendent;
        private bool _fundamental;


        public DateOnly StartDate
        {
            get { return _period.StartDate; }
        }

        public DateOnly EndDate
        {
            get { return _period.EndDate; }
        }

        public long ColaboratorId
        {
            get { return _colaboratorId; }
        }

        public long ProjectId
        {
            get { return _projectId; }
        }

        public long AssociationId
        {
            get { return _associationId; }
        }

        public bool Fundamental
        {
            get { return _fundamental; }
        }

        public Period Period { get { return _period; } set { _period = value; } }

        public bool IsPendent { get { return _isPendent; } set { _isPendent = value; } }

        public Association(long associationId, long colaboratorId, long projectId, DateOnly periodStart, DateOnly periodEnd, bool fundamental, bool isPendent = true)
        {
            _associationId = associationId;
            _colaboratorId = colaboratorId;
            _projectId = projectId;
            _period = new Period(periodStart, periodEnd);
            _fundamental = fundamental;
            _isPendent = isPendent;
        }

        public void UpdatePeriod(DateOnly startDate, DateOnly endDate)
        {
            _period.UpdateStartDate(startDate);
            _period.UpdateEndDate(endDate);
        }


        public bool IsColaboratorInAssociation(long colaboratorId)
        {
            if (colaboratorId.Equals(_colaboratorId))
            {
                return true;
            }

            return false;
        }


        // public List<IColaborator> AddColaboradorEmPeriodo(List<IColaborator> colaborators, DateOnly dataInicio, DateOnly dataFim)
        // {
        //     if (IsAssociationEmPeriodo(dataInicio, dataFim))
        //     {
        //         IColaborator colab = _colaborador;
        //         colaborators.Add(colab);
        //     }

        //     return colaborators;
        // }


        public bool IsAssociationInPeriod(DateOnly startDate, DateOnly endDate)
        {
            if (StartDate >= startDate && StartDate <= endDate ||
            StartDate <= startDate && EndDate > startDate ||
            StartDate < endDate && EndDate >= endDate)
            {
                return true;
            }

            return false;
        }


        public (DateOnly start, DateOnly end) GetDatesAssociationInPeriod(DateOnly startDate, DateOnly endDate)
        {
            if (startDate >= EndDate || endDate <= StartDate)
            {
                return (DateOnly.MinValue, DateOnly.MinValue);
            }

            DateOnly start = startDate >= StartDate ? startDate : StartDate;
            DateOnly end = endDate >= EndDate ? EndDate : endDate;

            return (start, end);
        }
    }
}