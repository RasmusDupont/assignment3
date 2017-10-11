using System;
namespace questionAserver
{
    public class Response
    {

		public Response(string status)
		{
			Status = status;
		}
        public Response(string status, string body)
        {
            Status = status;
            Body = body;
        }


        public string Status
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
