using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModel.Model
{
    public class ColaboratorsIdDataModel
    {
        public long Id { get; set; }

        public ColaboratorsIdDataModel() {}

        public ColaboratorsIdDataModel(long id)
        {
            Id = id;
        }
    }
}