namespace WePing.Components
{
    public class DropDownItem<T>
    {
        public string Header { get; set; } = "";
        public bool Divider { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public T Value { get; set; }
    }
}
