using MicroS_Common.Actions;
using WePing.domain.Equipes.Dto;
namespace WePing.Actions
{
    public class BrowseResultatEquipeClassementAction:IAction
    {
        //public EquipeDto Equipe { get; set; }
        public string Division { get; set; }

        public string Poule { get; set; }
    }
}
