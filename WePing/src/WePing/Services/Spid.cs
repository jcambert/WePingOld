using MicroS_Common.Types;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WePing.domain.ClubDetails.Dto;
using WePing.domain.ClubDetails.Queries;
using WePing.domain.Clubs.Dto;
using WePing.domain.Clubs.Queries;
using WePing.domain.Equipes.Dto;
using WePing.domain.Equipes.Queries;
using WePing.domain.JoueurDetails.Dto;
using WePing.domain.JoueurDetails.Queries;
using WePing.domain.Joueurs.Dto;
using WePing.domain.Joueurs.Queries;
using WePing.domain.Licences.Dto;
using WePing.domain.Licences.Queries;
using WePing.domain.Organismes.Dto;
using WePing.domain.Organismes.Queries;
using WePing.domain.Parties.Dto;
using WePing.domain.Parties.Queries;
using WePing.domain.ResultatEquipeRencontres.Dto;
using WePing.domain.ResultatEquipeRencontres.Queries;

namespace WePing.Services
{
    public class PageLink
    {


        private readonly string _prev;
        private readonly string _next;
        private readonly string _last;
        private readonly string _first;

        public PageLink(string link)
        {
            link.TryLinksFromHeader(out _first, out _prev, out _next, out _last);
        }

        private PageLink()
        {
            _first = _next = _last = _first = string.Empty;
        }
        public string FirstLink => _first;
        public string PrevLink => _prev;
        public string NextLink => _next;

        public string LastLink => _last;

        public static PageLink Blank => new PageLink();
    }

    public interface IPagedResultWithLinks<T>
    {
        PageLink PageLink { get; set; }

        List<T> Items { get; set; }
        int TotalResults { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        int ResultsPerPage { get; set; }

        bool HasItems { get; }
    }
    public class PagedResultWithLinks<T> : IPagedResultWithLinks<T>
    {


        public PagedResultWithLinks()
        {

        }

        public PageLink PageLink { get; set; }

        public List<T> Items { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int ResultsPerPage { get; set; }
        public bool HasItems => Items == null ? false : Items.Count() > 0;
        public static PagedResultWithLinks<T> Create(T item)
        {
            return new PagedResultWithLinks<T>()
            {
                CurrentPage = 1,
                PageLink = PageLink.Blank,
                ResultsPerPage = 1,
                TotalPages = 1,
                TotalResults = 1,
                Items = new List<T>() { item }
            };
        }
        public static PagedResultWithLinks<T> Blank()
        {
            return new PagedResultWithLinks<T>()
            {
                CurrentPage = 0,
                PageLink = PageLink.Blank,
                ResultsPerPage = 0,
                TotalPages = 0,
                TotalResults = 0,
                Items = new List<T>()
            };
        }
    }

    public interface ISpidService
    {
        Task<IPagedResultWithLinks<LicenceDto>> Licences(BrowseLicences query);
        Task<IPagedResultWithLinks<LicenceDto>> Licence(GetLicence query);
        Task<IPagedResultWithLinks<JoueurDto>> GetJoueurs(BrowseJoueur query);
        Task<IPagedResultWithLinks<ClubDto>> GetClubs(BrowseClubs query);
        Task<JoueurDetailDto> GetJoueurDetail(GetJoueurDetail query);
        Task<ClubDetailDto> GetClubDetail(GetClubDetail query);
        Task<ClubDto> GetClub(GetClub query);
        Task<LicenceDto> GetLicence(GetLicence query);
        Task<IPagedResultWithLinks<LicenceDto>> GetLicencesForClub(BrowseLicences query);
        Task<IPagedResultWithLinks<EquipeDto> >GetEquipes(BrowseEquipes query);
        Task<IPagedResultWithLinks<OrganismeDto>> GetOrganismes(BrowseOrganismes query);
        Task<IPagedResultWithLinks<ResultatEquipeClassementDto>> GetResultatEquipeClassements(BrowseResultatEquipeClassements query);
        Task<IPagedResultWithLinks<PartieDto>> GetParties(BrowseParties query); 
        

    }
    public class SpidService : ISpidService
    {
        public const string SPID = "spid";
        private readonly HttpClient _http;
        private readonly string _api_endpoint;
        private readonly IServiceProvider _serviceProvider;
        public SpidService(IConfiguration config, IServiceProvider serviceProfider, HttpClient client)
        {
            _http = client;
            _api_endpoint = config.GetValue<string>(SPID + ":api");
            _serviceProvider = serviceProfider;

        }

        public async Task<IPagedResultWithLinks<LicenceDto>> Licences(BrowseLicences query)
        {
            // new PagedResultWithLinks<LicenceDto, BrowseLicences>(_http, query, _api_endpoint,"licences",refresh);
            return await GetRequest<LicenceDto, BrowseLicences>($"{_api_endpoint}/licences", query);
        }
        public async Task<IPagedResultWithLinks<LicenceDto>> Licence(GetLicence query)
        {
            var res = await GetRequest<LicenceDto>($"{_api_endpoint}/licence/{query.Licence}");
            return PagedResultWithLinks<LicenceDto>.Create(res);
        }

        public async Task<IPagedResultWithLinks<JoueurDto>> GetJoueurs(BrowseJoueur query)
        {
            return await GetRequest<JoueurDto, BrowseJoueur>($"{_api_endpoint}/joueurs", query);
        }

        public async Task<IPagedResultWithLinks<ClubDto>> GetClubs(BrowseClubs query)
        {
            return await GetRequest<ClubDto, BrowseClubs>($"{_api_endpoint}/clubs", query);
        }

        public async Task<JoueurDetailDto> GetJoueurDetail(GetJoueurDetail query)
        {
            var res = await GetRequest<JoueurDetailDto>($"{_api_endpoint}/joueur/{query.Licence}");
            return res;
        }
        public async Task<ClubDto> GetClub(GetClub query)
        {
            BrowseClubs _query = new BrowseClubs() { Numero = query.Numero };
            var res = await GetRequest<ClubDto, BrowseClubs>($"{_api_endpoint}/clubs",_query);
            return res.Items.FirstOrDefault();
        }
        public async Task<LicenceDto> GetLicence(GetLicence query)
        {
            var res = await GetRequest<LicenceDto>($"{_api_endpoint}/licence/{query.Licence}");
            return res;
        }

        public async Task<ClubDetailDto> GetClubDetail(GetClubDetail query)
        {
            var res = await GetRequest<ClubDetailDto>($"{_api_endpoint}/club/{query.Club}");
            return res;
        }

        public async Task<IPagedResultWithLinks<LicenceDto>>GetLicencesForClub(BrowseLicences query)
        {
            var res = await GetRequest<LicenceDto, BrowseLicences>($"{_api_endpoint}/licences",query);
            return res;
        }
        public async Task<IPagedResultWithLinks<EquipeDto>> GetEquipes(BrowseEquipes query)
        {
            var res = await GetRequest<EquipeDto, BrowseEquipes>($"{_api_endpoint}/equipes", query);
            return res;
        }
        public async Task<IPagedResultWithLinks<OrganismeDto>> GetOrganismes(BrowseOrganismes query)
        {
            var res = await GetRequest<OrganismeDto, BrowseOrganismes>($"{_api_endpoint}/organismes", query);
            return res;
        }

        public async Task<IPagedResultWithLinks<ResultatEquipeClassementDto>> GetResultatEquipeClassements(BrowseResultatEquipeClassements query)
        {
            var res = await GetRequest<ResultatEquipeClassementDto, BrowseResultatEquipeClassements>($"{_api_endpoint}/resultat_equipe_classement", query);
            return res;
        }

        public async Task<IPagedResultWithLinks<PartieDto>> GetParties(BrowseParties query)
        {
            var res = await GetRequest<PartieDto, BrowseParties>($"{_api_endpoint}/parties", query);
            return res;
        }

        private async Task<IPagedResultWithLinks<T>> GetRequest<T, TQuery>(string url, TQuery query)
            where TQuery : IPagedQuery
        {
            PageLink _pageLink = PageLink.Blank;
            int _totalResults = 0, _totalPages;
            var result = _serviceProvider.GetRequiredService<IPagedResultWithLinks<T>>();
            url = QueryHelpers.AddQueryString(url, query.ToDictionnary());
            var response = await _http.GetAsync(url);
            if (response.Headers.TryGetValues("link", out IEnumerable<string> headerlink))
                _pageLink = new PageLink(headerlink.ToList().FirstOrDefault());
            if (response.Headers.TryGetValues("X-Total-Count", out IEnumerable<string> headerTotal))
                _totalResults = int.Parse(headerTotal.ToList().FirstOrDefault());

            double dd = (Convert.ToDouble(_totalResults) / query.Results);
            //int rr = (_totalResults / query.Results);
            _totalPages = query.Results > 0 ? (int)Math.Ceiling(dd) : 0;
            if (_totalResults > 0 && _totalPages == 0) _totalPages = 1;

            var _currentPage = query.Page < 1 ? 1 : query.Page;
            var _resultsPerPage = query.Results < 0 ? 0 : query.Results;

            var content = await response.Content.ReadAsStringAsync();
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(content);

            result.Items = items;
            result.PageLink = _pageLink;
            result.TotalResults = _totalResults;
            result.TotalPages = _totalPages;
            result.CurrentPage = _currentPage;
            result.ResultsPerPage = _resultsPerPage;

            return result;
        }

        private async Task<T> GetRequest<T>(string url)
        {
            var response = await _http.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
            return items;
        }

       
    }

    internal static class Extensions
    {
        public static bool TryLinksFromHeader(this string linkHeaderStr, out string first, out string prev, out string next, out string last)
        {

            first = prev = next = last = string.Empty;
            if (!string.IsNullOrWhiteSpace(linkHeaderStr))
            {
                string[] linkStrings = linkHeaderStr.Split(',');

                if (linkStrings != null && linkStrings.Any())
                {


                    foreach (string linkString in linkStrings)
                    {
                        var relMatch = Regex.Match(linkString, "(?<=rel=\").+?(?=\")", RegexOptions.IgnoreCase);
                        var linkMatch = Regex.Match(linkString, "(?<=<).+?(?=>)", RegexOptions.IgnoreCase);

                        if (relMatch.Success && linkMatch.Success)
                        {
                            string rel = relMatch.Value.ToUpper();
                            string link = linkMatch.Value;

                            switch (rel)
                            {
                                case "FIRST":
                                    first = link;
                                    break;
                                case "PREV":
                                    prev = link;
                                    break;
                                case "NEXT":
                                    next = link;
                                    break;
                                case "LAST":
                                    last = link;
                                    break;
                            }
                        }
                    }
                    return first.Any() && next.Any() && prev.Any() && last.Any();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        public static Dictionary<string, string> ToDictionnary(this Object o)
        {
            var result = new Dictionary<string, string>();
            var props = o.GetType().GetProperties();
            props.ToList().ForEach(p =>
            {
                string key = p.Name.ToLower();
                string value = p.GetGetMethod().Invoke(o, null)?.ToString().Trim();
                if (!string.IsNullOrEmpty(value) && value.Length > 0)
                    result[key] = value;
            });
            return result;
        }

    }
}
