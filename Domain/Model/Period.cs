using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Period
    {
        public long Id { get; set; }
        private DateOnly _startDate;
        private DateOnly _endDate;

        public DateOnly StartDate
        {
            get { return _startDate; }
        }

        public DateOnly EndDate
        {
            get { return _endDate; }
        }


        public Period(DateOnly startDate, DateOnly endDate)
        {
            if (!IsStartDateIsValid(startDate, endDate))
            {
                throw new ArgumentException("invalid arguments: start date >= end date.");
            }

            this._startDate = startDate;
            this._endDate = endDate;
        }

        public bool IsStartDateIsValid(DateOnly startDate, DateOnly endDate)
        {
            if (startDate >= endDate)
            {
                return false;
            }
            return true;
        }
    }
}