using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Joueurs.Domain;
using WePing.domain.Joueurs.Dto;
using WePing.domain.Joueurs.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseJoueursHandler : BaseBrowse<Joueur, BrowseJoueur, JoueurDto>
    {
        public BrowseJoueursHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseJoueur, Task<List<Joueur>>> Execute => Spid.GetJoueurs;
    }

}
