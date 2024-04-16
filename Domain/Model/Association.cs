using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Association : IAssociation
    {
        public long Id { get; set; }
        private long _colaboratorId;
        private long _projectId;
        private Period _period;


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

        public Period Period { get { return _period; } set { _period = value; } }

        public Association(long colaboratorId, long projectId, DateOnly periodStart, DateOnly periodEnd)
        {
            _colaboratorId = colaboratorId;
            _projectId = projectId;
            _period = new Period(periodStart, periodEnd);
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
            if (StartDate >= startDate && EndDate <= endDate ||
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