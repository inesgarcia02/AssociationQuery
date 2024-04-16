using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public interface IAssociation
    {
        bool IsColaboratorInAssociation(long colaboratorId);
        (DateOnly start, DateOnly end) GetDatesAssociationInPeriod(DateOnly startDate, DateOnly endDate);
        //List<IColaborator> AddColaboradorEmPeriodo(List<IColaborator> colaborators, DateOnly startDate, DateOnly endDate);
        bool IsAssociationInPeriod(DateOnly startDate, DateOnly endDate);

        Period Period{ get; set; }
    }
}