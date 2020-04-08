using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;

namespace WePing.Components
{
    public class DesktopComponent : ComponentBase
    {
        #region Parameters
        [Parameter]
        public string Class { get; set; } = "";
        [Parameter]
        public string ExtraSmallMediaWidth { get; set; }
        [Parameter]
        public string SmallMediaWidth { get; set; }
        [Parameter]
        public string MediumMediaWidth { get; set; }
        [Parameter]
        public string LargeMediaWidth { get; set; }
        [Parameter]
        public string ExtraLargeMediaWidth { get; set; }
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }
        [Parameter]
        public RenderFragment ToolBox { get; set; }

        [Parameter]
        public int FixedHeight { get; set; }
        #endregion
        #region protected Variables
        protected string MediaWidth = "";
        protected string FixedHeightClass = "";
        #endregion


        #region CTOR
        public DesktopComponent()
        {

        }
        #endregion

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        private void SetMediaWith(Media media, string value, StringBuilder sb)
        {

            if (!string.IsNullOrEmpty(value) && int.TryParse(value, out int result) && result > 0 && result <= 12)
                sb.Append($"col-{media.GetDescription()}-{result}");
        }
        private void SetFixedHeight()
        {

            if (FixedHeight >= 200 && FixedHeight <= 400 && FixedHeight % 10 == 0)
                FixedHeightClass = $"fixed_height_{FixedHeight}";
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();


        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            StringBuilder sb = new StringBuilder();
            SetMediaWith(Media.ExtraSmall, ExtraSmallMediaWidth, sb);
            SetMediaWith(Media.Small, SmallMediaWidth, sb);
            SetMediaWith(Media.Medium, MediumMediaWidth, sb);
            SetMediaWith(Media.Large, LargeMediaWidth, sb);
            SetMediaWith(Media.ExtraLarge, ExtraLargeMediaWidth, sb);

            MediaWidth = sb.ToString();

            SetFixedHeight();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            //JsRuntime.InvokeAsync<object>("showAlert");
        }
    }
}
