using Domain.Model;

namespace Application.DTO
{
    public class ColaboratorDTO
    {
        public long Id { get; set; }

        public ColaboratorDTO()
        {
        }

        public ColaboratorDTO(long id)
        {
            Id = id;
        }


        static public ColaboratorDTO ToDTO(Colaborator colaborator)
        {
            ColaboratorDTO colaboratorDTO = new ColaboratorDTO(colaborator.Id);
            return colaboratorDTO;
        }
    }
}