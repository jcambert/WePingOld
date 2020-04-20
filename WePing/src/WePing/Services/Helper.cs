using System;

namespace WePing.Services
{
    public  class Helper
    {
        public  int Percentage(float value, float total) => (int)Math.Round(value / total * 100);
    }
}
