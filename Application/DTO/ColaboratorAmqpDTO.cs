using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ColaboratorAmqpDTO
    {
        public long Id { get; set; }

        public ColaboratorAmqpDTO()
        {
        }

        public ColaboratorAmqpDTO(long id)
        {
            Id = id;
        }

        public static string Serialize(ColaboratorDTO colabDTO)
        {
            ColaboratorAmqpDTO colabGateway = new ColaboratorAmqpDTO(colabDTO.Id);
            var jsonMessage = JsonSerializer.Serialize(colabGateway);
            return jsonMessage;
        }

        public static ColaboratorDTO Deserialize(string colabDTOString)
        {
            return JsonSerializer.Deserialize<ColaboratorDTO>(colabDTOString)!;
        }

        public static ColaboratorDTO ToDTO(string colabDTOString)
        {
            ColaboratorDTO colabGatewayDTO = Deserialize(colabDTOString);
            ColaboratorDTO colabDTO = new ColaboratorDTO(colabGatewayDTO.Id);
            return colabDTO;
        }
    }
}