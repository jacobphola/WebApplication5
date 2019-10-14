using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class QueueController : Controller
    {
        static Queue<string> myQueue = new Queue<string>();
        static string lastItem = "";

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Add1()
        {
            if (myQueue.Count == 0)
            {
                myQueue.Enqueue("New Entry 1");
                lastItem = "New Entry 1";
            }
            else
            {
                string top = lastItem;
                string last = top.Substring(9, top.Length - 9);

                myQueue.Enqueue("New Entry " + (Int32.Parse(last) + 1));
                lastItem = "New Entry " + (Int32.Parse(last) + 1);

            }

            return View("Index");
        }

        public ActionResult AddHuge()
        {
            myQueue.Clear();

            for (var entryNum = 1; entryNum < 2001; entryNum++)
            {
                myQueue.Enqueue("New Entry " + entryNum);
                lastItem = "New Entry " + entryNum;
            }

            return View("Index");
        }

        public ActionResult Display()
        {
            try
            {
                foreach (var sData in myQueue)
                {
                    ViewBag.myQueue += "<p>" + sData + "<p>";
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
            Queue<string> holdQueue = new Queue<string>();


            try
            {
                ViewBag.deleteNotFound = "The Record you wish to delete does not exist";
                while (myQueue.Count > 0)
                {
                    if (myQueue.Peek() == "New Entry " + delete)
                    {
                        myQueue.Dequeue();
                        ViewBag.deleteNotFound = "";
                    }
                    else
                    {
                        holdQueue.Enqueue(myQueue.Dequeue());
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Index");
            }

            while (holdQueue.Count > 0)
            {
                myQueue.Enqueue(holdQueue.Dequeue());
            }

            return View("Index");
        }

        public ActionResult clearQueue()
        {
            myQueue.Clear();

            return View("Index");
        }

        public ActionResult SearchForm()
        {
            return View();
        }

        public ActionResult SearchQueue(int Search)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Queue<string> holdQueue = new Queue<string>();

            sw.Start();
            try
            {
                ViewBag.searchNotFound = "The Record you searched for does not exist";
                while (myQueue.Count > 0)
                {
                    if (myQueue.Peek() == "New Entry " + Search)
                    {
                        ViewBag.searchedItem = myQueue.Peek();
                        holdQueue.Enqueue(myQueue.Dequeue());
                        ViewBag.searchNotFound = "Item Found!";
                    }
                    else
                    {
                        holdQueue.Enqueue(myQueue.Dequeue());
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Index");
            }

            while (holdQueue.Count > 0)
            {
                myQueue.Enqueue(holdQueue.Dequeue());
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            ViewBag.StopWatch = "Your search was completed in " + ts.TotalSeconds + " seconds";

            return View("Index");
        }
    }
}