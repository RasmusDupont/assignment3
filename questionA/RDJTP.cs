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
            
            
            
            if (!(request.Date != null)) return JsonConvert.SerializeObject(new Response("missing date"));
            if (IsDigitsOnly(request.Date) == false) { return JsonConvert.SerializeObject(new Response("illegal date")); }
            if (!(request.Method != null)) return JsonConvert.SerializeObject(new Response("4 missing method"));
            if (!(request.Method == "read" || request.Method == "update" || request.Method == "delete" || request.Method == "create" || request.Method == "echo")) return JsonConvert.SerializeObject(new Response("illegal method"));
            if (request.Method == "echo") {
                {
                    if (!(request.Body != null)) return JsonConvert.SerializeObject(new Response("missing body"));
                    string cate = JsonConvert.SerializeObject(request.Body);
                    String r = request.Body;
                    //convert to json
                   
                    string body = JsonConvert.SerializeObject(cate);
                    body = body.Replace("\"", "").Replace("\\", "");
                    Response response = new Response(r, body);


                    return JsonConvert.SerializeObject(response);

                }
            }

            if (!(request.Path.Contains("categories") || request.Path.Contains("testing"))) return JsonConvert.SerializeObject(new Response("4 Bad Request")); 

            
            if (request.Method == "create")
            {
                if (!(request.Body != null)) return JsonConvert.SerializeObject(new Response("missing body"));
                if (!(request.Path != null)) return JsonConvert.SerializeObject(new Response("4 Bad Request"));
                Category cate = JsonConvert.DeserializeObject<Category>(request.Body);
                if (cate.Name == null || cate.Name == "null" || cate.Name == "") { return JsonConvert.SerializeObject(new Response("4 Bad Request")); }



                if (cate.Id <= 0)
                {
                    cate.Id = CategoryList.Getlist().Count + 1;
                }


                //convert to json

                string body = JsonConvert.SerializeObject(cate);
                Response response = new Response("2 created", body);

                CategoryList.Getlist().Add(new Category(cate.Id, cate.Name));
                return JsonConvert.SerializeObject(response);

            }

            else if (request.Method == "read")
            {
                string s = request.Path;
                s = string.Join("", s.ToCharArray().Where(Char.IsDigit));

              
                if (request.Path.Contains("/categories/"))
                {
                    if (s == "" || s == null) { return JsonConvert.SerializeObject(new Response("4 Bad Request")); }
                    int id = Int32.Parse(s);
                    string body = JsonConvert.SerializeObject(CategoryList.GetCategory(id));
                    if (body == null || body == "null") { return JsonConvert.SerializeObject(new Response("5 not found")); }
                       
                    Response response = new Response("1 Ok", body);
                    return JsonConvert.SerializeObject(response);
                }
                if (request.Path.Contains("/categories") && s == "" || s == null)
                {
                    string body = JsonConvert.SerializeObject(CategoryList.Getlist());
                    Response response = new Response("1 Ok", body);
                    return JsonConvert.SerializeObject(response);
                }
                return JsonConvert.SerializeObject(new Response("4 Bad Request"));
            }

            else if (request.Method == "update")
            {
                if (!(request.Body != null)) return JsonConvert.SerializeObject(new Response("missing body"));
                if (request.Path.Contains("/categories/"))
                {
                    if (request.Body.Contains("name")) { 
                        string s = request.Path.ToString();
                    s = string.Join("", s.ToCharArray().Where(Char.IsDigit));
                    int id = Int32.Parse(s);
                    
                    Category cate = JsonConvert.DeserializeObject<Category>(request.Body);
                    CategoryList.Updatelist(id, cate.Name);
                    string body = JsonConvert.SerializeObject(CategoryList.GetCategory(id));
                    if (body == null || body == "null") { return JsonConvert.SerializeObject(new Response("5 not found")); }
                    Response response = new Response("3 updated", body);
                    //convert to json
                    return JsonConvert.SerializeObject(response);
                }
                    return JsonConvert.SerializeObject(new Response("illegal body"));
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
            if (!(request.Body != null)) return JsonConvert.SerializeObject(new Response("missing resource"));

            return JsonConvert.SerializeObject(new Response("4 Bad Request"));
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

    }
}

