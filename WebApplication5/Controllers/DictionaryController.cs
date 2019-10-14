using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class DictionaryController : Controller
    {
        static Dictionary<string,int> myDictionary = new Dictionary<string,int>();
        static int currNum = 1;

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Add1()
        {
            myDictionary.Add("New Entry " + currNum, currNum);
            currNum++;
            

            return View("Index");
        }

        public ActionResult AddHuge()
        {
            myDictionary.Clear();

            for (currNum = 1; currNum < 2001; currNum++)
            {
                myDictionary.Add("New Entry " + currNum, currNum);
            }

            return View("Index");
        }

        public ActionResult Display()
        {
            try
            {
                foreach (var sData in myDictionary)
                {
                    ViewBag.myDictionary += "<p>Key: " + sData.Key + "<br>Value: " + sData.Value + "<p>";
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Index");

            }

            return View("Index");
        }


        public ActionResult DeleteForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int delete)
        {
            try
            {
                
                if (myDictionary.Remove("New Entry " + delete))
                {
                    ViewBag.deleteNotFound = "Record deleted";
                }
                else
                {
                    ViewBag.deleteNotFound = "The Record you wish to delete does not exist";
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
            }

            return View("Index");
        }

        public ActionResult clearDictionary()
        {
            myDictionary.Clear();

            return View("Index");
        }

        public ActionResult SearchForm()
        {
            return View();
        }

        public ActionResult SearchDictionary(int Search)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            try
            {

                if (myDictionary.ContainsKey("New Entry " + Search))
                {
                    ViewBag.deleteNotFound = "Item Found!";
                    ViewBag.searchedItem = "Key: New Entry " + Search + "<br>Value: " + Search;
                }
                else
                {
                    ViewBag.deleteNotFound = "The Record you searched for does not exist";
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            
            ViewBag.StopWatch = "Your search was completed in " + ts.TotalSeconds + " seconds";

            return View("Index");
        }
    }
}