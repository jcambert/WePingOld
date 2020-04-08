using MicroS_Common.Authentication;

namespace weping.api.Framework
{
    public class AdminAuthAttribute : JwtAuthAttribute
    {
        public AdminAuthAttribute() : base("admin")
        {
        }
    }
}
