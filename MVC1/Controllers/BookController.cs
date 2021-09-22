using MVC1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC1.Controllers
{
    public class BookController : Controller
    {
        HttpClient client;
        string bearerToken = string.Format("yuC3u0yt2Ubn2kNAVMn8zokZGsoA5hv7bLkClmvFa6n3sbrck76sK6cUXxdWesBVzwIryicrX1WxwgRWmNQe774jJQ7NHz9gvsP8uBZQblDN3Ibtpjy1g0fnmrT3QqjSog9FYrqFpM77rpbcycAXnVDtnbxuab0wSYF7iN7OoCYbo-eqG3OJRCUfF8VkpW1DUEurS0IF-W6-wv0nzfde9FXNJ_LBnl6jrsTzh1ErWbEIhefU4Bfy6kfaEnHbnGLnQ23QHKnCfjnYWgVpDk6phUGFGNEH4NwhTJBve6GWuNB2Ynhcm2V5h0SDRGdzNp6Gu0dYuGCixSPBeoLVMlu53oEQjBr3rOLzHXlphsj55Gpl6X84YaePZBjyv2UkKW3yVxR5Tft0M1uWhR2vixDN_Ua-pq5eKxgz0CsCp-h2qRu6czGRss17-qYQJOG1MNSgd9pkczp_v-2qPHxSve9MTuavm35sVXl79WLlxufGc2hGxKSOeQymreRAfwOGb1fS");
    string url = string.Format("https://localhost:44344/api/Book");
       
        // GET: Book

        //public BookController()
        //{
            //client = new HttpClient();
            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // just adding a JObject you can create a class 

            //JObject tokenJobject = new JObject(
            //                               new JProperty("Email", "library@library.com"),
            //                               new JProperty("Password", "Test1!"),
            //                               new JProperty("ConfirmPassword", "Test1!"));
            //HttpContent baseContent = new StringContent(tokenJobject.ToString(), Encoding.UTF8, "application/json");
           //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "yuC3u0yt2Ubn2kNAVMn8zokZGsoA5hv7bLkClmvFa6n3sbrck76sK6cUXxdWesBVzwIryicrX1WxwgRWmNQe774jJQ7NHz9gvsP8uBZQblDN3Ibtpjy1g0fnmrT3QqjSog9FYrqFpM77rpbcycAXnVDtnbxuab0wSYF7iN7OoCYbo-eqG3OJRCUfF8VkpW1DUEurS0IF-W6-wv0nzfde9FXNJ_LBnl6jrsTzh1ErWbEIhefU4Bfy6kfaEnHbnGLnQ23QHKnCfjnYWgVpDk6phUGFGNEH4NwhTJBve6GWuNB2Ynhcm2V5h0SDRGdzNp6Gu0dYuGCixSPBeoLVMlu53oEQjBr3rOLzHXlphsj55Gpl6X84YaePZBjyv2UkKW3yVxR5Tft0M1uWhR2vixDN_Ua-pq5eKxgz0CsCp-h2qRu6czGRss17-qYQJOG1MNSgd9pkczp_v-2qPHxSve9MTuavm35sVXl79WLlxufGc2hGxKSOeQymreRAfwOGb1fS");


        //}
        public async Task<ActionResult> Index()
        {
            List<BookModel> BookInfo = new List<BookModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "yuC3u0yt2Ubn2kNAVMn8zokZGsoA5hv7bLkClmvFa6n3sbrck76sK6cUXxdWesBVzwIryicrX1WxwgRWmNQe774jJQ7NHz9gvsP8uBZQblDN3Ibtpjy1g0fnmrT3QqjSog9FYrqFpM77rpbcycAXnVDtnbxuab0wSYF7iN7OoCYbo-eqG3OJRCUfF8VkpW1DUEurS0IF-W6-wv0nzfde9FXNJ_LBnl6jrsTzh1ErWbEIhefU4Bfy6kfaEnHbnGLnQ23QHKnCfjnYWgVpDk6phUGFGNEH4NwhTJBve6GWuNB2Ynhcm2V5h0SDRGdzNp6Gu0dYuGCixSPBeoLVMlu53oEQjBr3rOLzHXlphsj55Gpl6X84YaePZBjyv2UkKW3yVxR5Tft0M1uWhR2vixDN_Ua-pq5eKxgz0CsCp-h2qRu6czGRss17-qYQJOG1MNSgd9pkczp_v-2qPHxSve9MTuavm35sVXl79WLlxufGc2hGxKSOeQymreRAfwOGb1fS");
                HttpResponseMessage res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var BookResponse = res.Content.ReadAsStringAsync().Result;
                    BookInfo = JsonConvert.DeserializeObject<List<BookModel>>(BookResponse);
                }

                return View(BookInfo);
            }
            //IEnumerable<BookModel> bookList;
            //HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Book").Result;
            //bookList = response.Content.ReadAsAsync<IEnumerable<BookModel>>().Result;
            //return View(response);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            return View(new BookModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddOrEdit(BookModel book)
        {

            HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "yuC3u0yt2Ubn2kNAVMn8zokZGsoA5hv7bLkClmvFa6n3sbrck76sK6cUXxdWesBVzwIryicrX1WxwgRWmNQe774jJQ7NHz9gvsP8uBZQblDN3Ibtpjy1g0fnmrT3QqjSog9FYrqFpM77rpbcycAXnVDtnbxuab0wSYF7iN7OoCYbo-eqG3OJRCUfF8VkpW1DUEurS0IF-W6-wv0nzfde9FXNJ_LBnl6jrsTzh1ErWbEIhefU4Bfy6kfaEnHbnGLnQ23QHKnCfjnYWgVpDk6phUGFGNEH4NwhTJBve6GWuNB2Ynhcm2V5h0SDRGdzNp6Gu0dYuGCixSPBeoLVMlu53oEQjBr3rOLzHXlphsj55Gpl6X84YaePZBjyv2UkKW3yVxR5Tft0M1uWhR2vixDN_Ua-pq5eKxgz0CsCp-h2qRu6czGRss17-qYQJOG1MNSgd9pkczp_v-2qPHxSve9MTuavm35sVXl79WLlxufGc2hGxKSOeQymreRAfwOGb1fS");
                HttpResponseMessage response = await client.PostAsJsonAsync(url, book);
                var respondString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("index");
                }
                return RedirectToAction("Error");
            }
        }
    }
