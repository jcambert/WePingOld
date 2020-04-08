using AutoMapper;
using MicroS_Common.Handlers;
using MicroS_Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{


    public abstract class BaseBrowse<TDomain, TBrowseQuery, TDto> : BrowseHandler<TDomain, TBrowseQuery, TDto>
        where TBrowseQuery : PagedQueryBase, IQuery<PagedResult<TDto>>
    {
        private readonly ISpidRequest _spid;

        public BaseBrowse(IMapper mapper, ISpidRequest request) : base(mapper)
        {
            this._spid = request;
        }
        public ISpidRequest Spid => _spid;
        public abstract Func<TBrowseQuery, Task<List<TDomain>>> Execute { get; }

        protected override async Task<PagedResult<TDomain>> BrowseAsync(TBrowseQuery query)
        {

            var result = await Execute(query);
            return Paginate(result, query.Page, query.Results);
        }

        PagedResult<TDomain> Paginate(List<TDomain> clubs, int page = 1, int resultsPerPage = 10)
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
                return PagedResult<TDomain>.Empty;
            }
            var totalResults = clubs.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);

            return PagedResult<TDomain>.Create(clubs.ToArray().Skip((page - 1) * resultsPerPage).Take(resultsPerPage), page, resultsPerPage, totalPages, totalResults);
        }
    }

}
