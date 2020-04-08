using MicroS_Common.Types;
using System;
using WePing.domain.Rencontres.Dto;

namespace WePing.domain.Rencontres.Queries
{
    public class GetRencontre : IQuery<RencontreDto>
    {
        public Guid Id { get; set; }
    }
}
