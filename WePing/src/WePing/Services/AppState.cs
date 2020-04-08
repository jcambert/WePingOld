namespace WePing.Services
{
    /*
    public class AppState
    {
        #region ctor
        public AppState(ISpidService spid, QueryService query, LocalStorage localStorage)
        {
            Searching = false;
            SearchClubResult = PagedResultWithLinks<ClubDto>.Blank();
            SearchJoueurResult = PagedResultWithLinks<JoueurDto>.Blank();
            SearchLicenceResult = PagedResultWithLinks<LicenceDto>.Blank();
            SpidService = spid;
            QueryService = query;
            LocalStorage = localStorage;
        }
        #endregion

        #region Services Properties
        internal ISpidService SpidService { get; private set; }
        internal QueryService QueryService { get; private set; }
        internal LocalStorage LocalStorage { get; private set; }
        #endregion

        #region Event Notification
        private void NotifyStateChanged() => OnChange?.Invoke();

        public event Action OnChange;

        private void NotifyStateChangedAsync(string key) => OnChangeAsync?.Invoke(key);

        public event Func<string, Task> OnChangeAsync;
        #endregion

        #region Searching
        public bool Searching { get; private set; }

        public void SetSearching(bool searching)
        {
            Searching = searching;

            NotifyStateChangedAsync("Searching");

        }
        #endregion

        #region Searching Results Methods
        internal IPagedResultWithLinks<LicenceDto> SearchLicenceResult { get; private set; }
        internal void SetSearchLicenceResult(IPagedResultWithLinks<LicenceDto> searchResult)
        {
            SearchLicenceResult = searchResult;
            SearchClubResult = PagedResultWithLinks<ClubDto>.Blank();
            SearchJoueurResult = PagedResultWithLinks<JoueurDto>.Blank();
            NotifyStateChangedAsync("SearchLicenceResult");
        }

        internal IPagedResultWithLinks<JoueurDto> SearchJoueurResult { get; private set; }


        internal void SetSearchJoueurResult(IPagedResultWithLinks<JoueurDto> searchResult)
        {
            SearchJoueurResult = searchResult;
            SearchLicenceResult = PagedResultWithLinks<LicenceDto>.Blank();
            SearchClubResult = PagedResultWithLinks<ClubDto>.Blank();
            NotifyStateChangedAsync("SearchJoueurResult");
        }

        internal IPagedResultWithLinks<ClubDto> SearchClubResult { get; private set; }
        internal void SetSearchClubResult(IPagedResultWithLinks<ClubDto> searchResult)
        {
            SearchClubResult = searchResult;
            SearchLicenceResult = PagedResultWithLinks<LicenceDto>.Blank();
            SearchJoueurResult = PagedResultWithLinks<JoueurDto>.Blank();
            NotifyStateChangedAsync("SearchClubResult");
        }

        internal Dictionary<string, JoueurDetailDto> JoueurDetail { get; } = new Dictionary<string, JoueurDetailDto>();
        #endregion

        #region Searching Managment

        #region Properties
        internal Dictionary<string, SearchOption> SearchOptions { get; private set; } = new Dictionary<string, SearchOption>();
        private IQuery _query;
        public IQuery Query => _query;
        internal SearchType SearchType { get; private set; } = SearchType.All;
        internal SearchOption SelectedSearchOptions { get; set; }
        internal bool HasSelectedSearchoption(string key) => SearchOptions.ContainsKey(key) && !string.IsNullOrEmpty(SearchOptions[key].Value);
        private string _searchTerm = "";
        public string SearchValueType => SearchType.GetDescription();
        internal string SearchTerm
        {
            get
            {
                return _searchTerm;
            }
            set
            {
                _searchTerm = value.Trim();

                Task.Run(async () =>
                {
                    await LocalStorage.SetItemAsync("searchTerm", _searchTerm);
                });
            }
        }
        #endregion

        #region Public Methods
        internal void AddSelectedSearchOption(string searchTerm, string key = null)
        {
            if (!string.IsNullOrEmpty(key))
                SearchOptions[key].Value = searchTerm;
            else
                SearchOptions[SelectedSearchOptions.PropertyName].Value = searchTerm;
        }
        internal void RemoveSelectedSearchOption(string key)
        {
            SearchOptions[key].Value = string.Empty;
        }
        internal void ClearSelectedSearchOption()
        {
            var keys = SearchOptions.Keys;
            foreach (var key in keys)
            {
                SearchOptions[key].Value = string.Empty;
            }
        }
        internal async Task ExecuteSearch(string searchTerm)
        {
            SetSearching(true);
            bool empty = true;
            foreach ((string key, SearchOption opt) in SearchOptions)
            {
                if (!string.IsNullOrEmpty(opt.Value.Trim()))
                {
                    _query.GetType().GetProperty(key).GetSetMethod().Invoke(_query, new object[] { opt.Value.Trim() });
                    empty = false;
                }
                else
                {
                    _query.GetType().GetProperty(key).GetSetMethod().Invoke(_query, new object[] { null });
                }
            }

            if (empty && !string.IsNullOrEmpty(searchTerm.Trim()))
                _query.GetType().GetProperty(_query.GetType().GetDefault()).GetSetMethod().Invoke(_query, new object[] { searchTerm.Trim() });
            await ExecuteQuery(_query);
            SetSearching(false);
        }
        internal async Task ExecuteQuery(IQuery query)
        {
            try
            {
                SetSearching(true);
                QueryService.Format(query);
                switch (SearchType)
                {
                    case SearchType.All:
                        break;
                    case SearchType.Player:
                        var res0 = await SpidService.GetJoueurs(query as BrowseJoueur);
                        SetSearchJoueurResult(res0);

                        foreach (var item in SearchJoueurResult.Items)
                        {
                            var res3 = await SpidService.GetJoueurDetail(new domain.JoueurDetails.Queries.GetJoueurDetail() { Licence = item.Licence });
                            if (res3 != null && !string.IsNullOrEmpty(res3.Licence))
                                JoueurDetail[res3.Licence] = res3;
                        }
                        break;
                    case SearchType.Licence:
                        var res1 = await SpidService.Licence(query as GetLicence);
                        SetSearchLicenceResult(res1);

                        break;
                    case SearchType.Club:
                        var res2 = await SpidService.GetClubs(query as BrowseClubs);
                        SetSearchClubResult(res2);
                        break;
                    default:
                        break;
                }
                SetSearching(false);
            }
            catch (Exception)
            {
                SetSearching(false);
                throw;
            }

        }
        internal async Task SetSearchType(SearchType type)
        {
            await LocalStorage.SetItemAsync("searchType", type.ToString());
            SearchType = type;
            _query = type.GetQuery();

            SearchOptions.Clear();
            if (_query == null) return;
            foreach (var s in _query.GetType().GetPropertiesNames())
            {
                if (!DomainHelper.HasDisableSearchAttribute(_query.GetType(), s))
                {
                    var desc = DomainHelper.GetDescription(_query.GetType(), s);
                    SearchOptions[s] = new SearchOption() { Description = desc, PropertyName = s, Value = string.Empty };
                }
            }
            var default_property = _query.GetType().GetDefault();
            SelectedSearchOptions = SearchOptions[_query.GetType().GetDefault()];
        }
        #endregion
        #endregion

    }*/
}
