using MVCLibraray.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCLibraray.Controllers
{
    public class BookController : Controller
    {
        string baseUrl = "https://localhost:44344/";
        // GET: Book
        public async Task<ActionResult> Index()
        {
            List<BookModel> BookInfo = new List<BookModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Book");
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
        public ActionResult AddOrEdit(BookModel book)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Book", book).Result;
            return RedirectToAction("index");
        }
    }
}