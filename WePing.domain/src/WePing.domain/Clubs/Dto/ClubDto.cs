using MicroS_Common.Dto;
namespace WePing.domain.Clubs.Dto
{
    public class ClubDto : IDto
    {


        #region public properties

        public string Id { get; set; }

        public string Numero { get; set; }

        public string Nom { get; set; }

        public string Validation { get; set; }

        #endregion


    }
}
