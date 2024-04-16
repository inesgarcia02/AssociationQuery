using DataModel.Model;

namespace DataModel.Mapper
{
    public class ColaboratorsIdMapper
    {

        public ColaboratorsIdMapper()
        {
        }

        public long ToDomain(ColaboratorsIdDataModel colaboratorsIdDM)
        {
            return colaboratorsIdDM.Id;
        }

        public IEnumerable<long> ToDomain(IEnumerable<ColaboratorsIdDataModel> colaboratorsIdDataModel)
        {

            List<long> colaboratorsIdDomain = new List<long>();

            foreach(ColaboratorsIdDataModel colaboratorIdDomain in colaboratorsIdDataModel)
            {
                long id = ToDomain(colaboratorIdDomain);

                colaboratorsIdDomain.Add(id);
            }

            return colaboratorsIdDomain.AsEnumerable();
        }

        public ColaboratorsIdDataModel ToDataModel(long colaboratorId)
        {
            ColaboratorsIdDataModel colaboratorsIdDataModel = new ColaboratorsIdDataModel(colaboratorId);

            return colaboratorsIdDataModel;
        }
    }
}