using MicroS_Common.Types;
using System;
using WePing.domain.ResultatEquipeRencontres.Dto;

namespace WePing.domain.ResultatEquipeRencontres.Queries
{
    public class GetResultatEquipeRencontre : IQuery<ResultatEquipeRencontreDto>
    {
        public Guid Id { get; set; }
    }
}
