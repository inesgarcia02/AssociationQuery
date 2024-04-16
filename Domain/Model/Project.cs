using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Project
    {
        public long Id { get; set; }
        private DateOnly _startDate;
        private DateOnly? _endDate;

        public DateOnly StartDate
        {
            get { return _startDate; }
        }

        public DateOnly? EndDate
        {
            get { return _endDate; }
        }


        public Project(long id, DateOnly startDate, DateOnly? endDate)
        {
            Id = id;
            _startDate = startDate;
            _endDate = endDate;
        }
    }
}