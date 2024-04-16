using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public interface IPeriod
    {
        bool IsStartDateIsValid(DateOnly startDate, DateOnly endDate);
        void UpdateStartDate(DateOnly startDate);
        void UpdateEndDate(DateOnly endDate);
    }
}