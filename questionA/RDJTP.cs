using System;
using Newtonsoft.Json;
using System.Linq;

namespace questionAserver
{
    public class RDJTP
    {
        

        public RDJTP()
        {
            
        }

        public string Interpret(Request request)
        {
            if (!(request.Method != null)) return JsonConvert.SerializeObject(new Response("4 missing method"));
            
            if (request.Method == "create")
			{
                if (!(request.Path != null)) return JsonConvert.SerializeObject(new Response("4 Bad Request"));
                
                    string body = request.Body;
                    Response response = new Response("2 created", body);
                Category cate = JsonConvert.DeserializeObject<Category>(request.Body);

                CategoryList.Getlist().Add(new Category(cate.Id,cate.Name));
                    //convert to json
                    return JsonConvert.SerializeObject(response);
                
            }

            else if (request.Method == "read")
            {
                //read single category by id
                if (request.Path.Contains("/categories/"))
                {
                    string s = request.Path.ToString();
                    s = string.Join("", s.ToCharArray().Where(Char.IsDigit));
                    int id = Int32.Parse(s);
                    string body = JsonConvert.SerializeObject(CategoryList.GetCategory(id));
                    Response response = new Response("1 Ok", body);
                    //convert to json
                    return JsonConvert.SerializeObject(response);
                }
			}

            else if (request.Method == "update")
            {
                if (request.Path.Contains("/categories/"))
                {
                    string s = request.Path.ToString();
                    s = string.Join("", s.ToCharArray().Where(Char.IsDigit));
                    int id = Int32.Parse(s);
                    Category cate = JsonConvert.DeserializeObject<Category>(request.Body);
                    CategoryList.Updatelist(id, cate.Name);
                    string body = JsonConvert.SerializeObject(CategoryList.GetCategory(id));
                    Response response = new Response("1 Ok", body);
                    //convert to json
                    return JsonConvert.SerializeObject(response);
                }
                return JsonConvert.SerializeObject(new Response("4 Bad Request"));
            }

			else if (request.Method == "delete")
			{
                String s = request.Path.ToString();
                if (!(request.Path != null)) return JsonConvert.SerializeObject(new Response("4 Bad Request"));
                if (!(s.Any(Char.IsDigit))) return JsonConvert.SerializeObject(new Response("4 Bad Request"));
                s = string.Join("", s.ToCharArray().Where(Char.IsDigit));
                int id = Int32.Parse(s);
                if (CategoryList.GetCategory(id) == null)
                {
                    return JsonConvert.SerializeObject(new Response("5 not found"));
                }
                string body = JsonConvert.SerializeObject(CategoryList.GetCategory(id));
                Response response = new Response("1 Ok", body);
                //convert to json
                return JsonConvert.SerializeObject(response);
            }

            return JsonConvert.SerializeObject(new Response("4 Bad Request"));
        }
          
        }
    }

