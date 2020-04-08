using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.ClassementJoueurs.Domain;
using WePing.domain.ClassementJoueurs.Dto;
using WePing.domain.ClassementJoueurs.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseClassementJoueursHandler : BaseBrowse<ClassementJoueur, BrowseClassementJoueurs, ClassementJoueurDto>
    {
        public BrowseClassementJoueursHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseClassementJoueurs, Task<List<ClassementJoueur>>> Execute => Spid.GetClassementJoueurs;
    }

}
