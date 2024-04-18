using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Colaborator
    {
        public long Id { get; set; }


        public Colaborator(long id)
        {
            Id = id;
        }
    }
}