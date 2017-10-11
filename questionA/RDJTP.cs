using System;
using Newtonsoft.Json;
namespace questionAserver
{
    public class RDJTP
    {
        Categories categories;

        public RDJTP()
        {
            categories = new Categories();
        }

        public string Interpret(string input)
        {
            
            Request request = JsonConvert.DeserializeObject<Request>(input);

            if (request.Method == "create")
			{
				//create functionality 
			}

            else if (request.Method == "read")
            {
                //read single category by id
                if (request.Path.Contains("/categories/"))
                {
                    int id = Int32.Parse(request.Path.Substring(12));
                    string body = JsonConvert.SerializeObject(categories.GetCategory(id));
                    Response response = new Response("1 OK", body);
                    //convert to json
                    return JsonConvert.SerializeObject(response);
                }
			}

            else if (request.Method == "update")
            {
               //update functionality 
            }

			else if (request.Method == "delete")
			{
				//delete functionality 
			}
            return JsonConvert.SerializeObject(new Response("4 Bad Request"));
        }
    }
}
