namespace WePing.Types
{
    public sealed class SearchOption
    {
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public SearchOption()
        {

        }
        public override string ToString()
        {
            return Description ?? PropertyName;
        }
    }
}
