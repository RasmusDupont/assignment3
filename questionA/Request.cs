using System;
namespace questionAserver
{
    public class Request
    {

		public Request(string method, string path, string date, string body)
		{
			Method = method;
			Path = path;
			Date = date;
            Body = body;
		}

        public string Method
        {
            get;
            set;
        }
        public string Path
        {
            get;
            set;
        }
        public string Date

        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }
    }
}
