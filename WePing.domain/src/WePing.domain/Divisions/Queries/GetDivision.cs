using MicroS_Common.Types;
using System;
using WePing.domain.Divisions.Dto;

namespace WePing.domain.Divisions.Queries
{
    public class GetDivision : IQuery<DivisionDto>
    {
        public Guid Id { get; set; }
    }
}
