using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Organismes.Domain;
using WePing.domain.Organismes.Dto;
using WePing.domain.Organismes.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseOrganismesHandler : BaseBrowse<Organisme, BrowseOrganismes, OrganismeDto>
    {
        public BrowseOrganismesHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseOrganismes, Task<List<Organisme>>> Execute => Spid.GetOrganismes;
    }
    /* public class BrowseOrganismesHandler : BrowseHandler<Organisme, BrowseOrganismes, OrganismeDto>
     {
         private readonly ISpidRequest _spid;

         public BrowseOrganismesHandler(IMapper mapper, ISpidRequest request) : base(mapper)
         {
             this._spid = request;
         }

         protected override async Task<PagedResult<Organisme>> BrowseAsync(BrowseOrganismes query)
         {
             var result = await _spid.GetOrganismes(query);
             return Paginate(result,query.Page,query.Results);
         }

         PagedResult<Organisme> Paginate(List<Organisme> clubs, int page = 1, int resultsPerPage = 10)
         {
             if (page <= 0)
             {
                 page = 1;
             }
             if (resultsPerPage <= 0)
             {
                 resultsPerPage = 10;
             }
             var isEmpty = clubs.Any() == false;
             if (isEmpty)
             {
                 return PagedResult<Organisme>.Empty;
             }
             var totalResults = clubs.Count();
             var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);

             return PagedResult<Organisme>.Create(clubs.ToArray().Skip((page - 1) * resultsPerPage).Take(resultsPerPage), page, resultsPerPage, totalPages, totalResults);
         }
     }*/
}
