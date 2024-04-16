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
        public string Email { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public ColaboratorAmqpDTO()
        {
        }

        public ColaboratorAmqpDTO(long id, string strName, string strEmail, string strStreet, string strPostalCode)
        {
            Id = id;
            Name = strName;
            Email = strEmail;
            Street = strStreet;
            PostalCode = strPostalCode;
        }

        public static string Serialize(ColaboratorDTO colabDTO)
        {
            ColaboratorAmqpDTO colabGateway = new ColaboratorAmqpDTO(colabDTO.Id, colabDTO.Name, colabDTO.Email,
                                                                    colabDTO.Street, colabDTO.PostalCode);
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
            ColaboratorDTO colabDTO = new ColaboratorDTO(colabGatewayDTO.Id, colabGatewayDTO.Name, colabGatewayDTO.Email,
                                                    colabGatewayDTO.Street, colabGatewayDTO.PostalCode);
            return colabDTO;
        }
    }
}