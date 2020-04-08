using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WePing.domain.Clubs.Domain;
using WePing.domain.Clubs.Dto;
using WePing.domain.Clubs.Queries;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid.Handlers
{
    public class BrowseClubsHandler : BaseBrowse<Club, BrowseClubs, ClubDto>
    {
        public BrowseClubsHandler(IMapper mapper, ISpidRequest request) : base(mapper, request)
        {
        }

        public override Func<BrowseClubs, Task<List<Club>>> Execute => Spid.GetClubs;
    }

}
