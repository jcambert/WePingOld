using WeRedux;
using WePing.domain.Equipes.Dto;
namespace WePing.Actions
{
    public class BrowseResultatEquipeClassementAction:IAction
    {
        public EquipeDto Equipe { get; set; }
        public string D1 =>Equipe.GetD1();

        public string Cx_poule => Equipe.GetCxPoule();
    }
}
