using ePunkt.Utilities;
using System;
using System.Globalization;
using System.Web;

namespace ePunkt.IssueTracker.Code
{
    public class UserOptions
    {
        private readonly HttpCookieCollection _requestCookies;
        private readonly HttpCookieCollection _responseCookies;

        public UserOptions(HttpCookieCollection requestCookies, HttpCookieCollection responseCookies)
        {
            _responseCookies = responseCookies;
            _requestCookies = requestCookies;
        }


        private string GetValue(string key)
        {
            return _requestCookies[key] != null ? _requestCookies[key].Value : null;
        }

        private void SetValue(string key, string value)
        {
            if (value.HasValue())
                _responseCookies.Add(new HttpCookie(key)
                {
                    Value = value,
                    Expires = DateTime.MaxValue
                });
            else if (_responseCookies[key] != null)
                _responseCookies[key].Expires = DateTime.MinValue;
        }


        public int? DateFilter
        {
            get { return GetValue("dateFilter").GetIntOrNull(); }
            set { SetValue("dateFilter", value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : null); }
        }

        public string Sorting
        {
            get { return GetValue("sorting"); }
            set { SetValue("sorting", value); }
        }

        public string StatusFilter
        {
            get { return GetValue("statusFilter"); }
            set { SetValue("statusFilter", value); }
        }

        public string UserFilter
        {
            get { return GetValue("userFilter"); }
            set { SetValue("userFilter", value); }
        }

        public string TextFilter
        {
            get { return GetValue("textFilter"); }
            set { SetValue("textFilter", value); }
        }
    }
}