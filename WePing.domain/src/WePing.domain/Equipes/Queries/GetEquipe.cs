using MicroS_Common.Types;
using System;
using WePing.domain.Equipes.Dto;

namespace WePing.domain.Equipes.Queries
{
    public class GetEquipe : IQuery<EquipeDto>
    {
        public Guid Id { get; set; }
    }
}
