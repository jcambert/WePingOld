using MicroS_Common.Types;
using System;
using WePing.domain.Organismes.Dto;

namespace WePing.domain.Organismes.Queries
{
    public class GetOrganisme : IQuery<OrganismeDto>
    {
        public Guid Id { get; set; }
    }
}
