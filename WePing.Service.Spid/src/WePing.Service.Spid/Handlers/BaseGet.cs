using AutoMapper;
using MicroS_Common.Handlers;
using MicroS_Common.Types;
using System;
using System.Threading.Tasks;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{


    public abstract class BaseGet<TDomain, TGetQuery, TDto> : IQueryHandler<TGetQuery, TDto>
        where TGetQuery : IQuery<TDto>
    {
        private readonly IMapper _mapper;
        public BaseGet(IMapper mapper, ISpidRequest request)
        {
            this._mapper = mapper;
            this.Spid = request;
        }

        public ISpidRequest Spid { get; }
        public abstract Func<TGetQuery, Task<TDomain>> Execute { get; }

        public async Task<TDto> HandleAsync(TGetQuery query)
        {
            var model = await Execute(query);
            return _mapper.Map<TDomain, TDto>(model);
            //return model == null ? null : _mapper.Map<TDomain, TDto>(model);
        }
    }
}
