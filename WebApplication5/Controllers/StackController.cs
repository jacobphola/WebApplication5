using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class StackController : Controller
    {
        static Stack<string> myStack = new Stack<string>();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Add1()
        {
            if (myStack.Count == 0)
            {
                myStack.Push("New Entry 1");
            }
            else
            {
                string top = myStack.Peek();
                string last = top.Substring(9, top.Length - 9);

                myStack.Push("New Entry " + (Int32.Parse(last) + 1));
               
            }
            
            return View("Index");
        }

        public ActionResult AddHuge()
        {
            myStack.Clear();

            for (var entryNum = 1; entryNum < 2001; entryNum++)
            {
                myStack.Push("New Entry " + entryNum);
            }

            return View("Index");
        }

        public ActionResult Display()
        {
            try
            {
                foreach(var sData in myStack)
                {
                    ViewBag.myStack += "<p>" + sData + "<p>";
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
            Stack<string> holdStack = new Stack<string>();

            
            try
            {
                ViewBag.deleteNotFound = "The Record you wish to delete does not exist";
                while (myStack.Count > 0)
                {
                    if (myStack.Peek() == "New Entry " + delete)
                    {
                        myStack.Pop();
                        ViewBag.deleteNotFound = "";
                    }
                    else
                    {
                        holdStack.Push(myStack.Pop());
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Index");
            }

            while (holdStack.Count > 0)
            {
                myStack.Push(holdStack.Pop());
            }

            return View("Index");
        }

        public ActionResult clearStack()
        {
            myStack.Clear();

            return View("Index");
        }

        public ActionResult SearchForm()
        {
            return View();
        }

        public ActionResult SearchStack(int Search)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Stack<string> holdStack = new Stack<string>();

            sw.Start();
            try
            {
                ViewBag.searchNotFound = "The Record you searched for does not exist";
                while (myStack.Count > 0)
                {
                    if (myStack.Peek() == "New Entry " + Search)
                    {
                        ViewBag.searchedItem = myStack.Peek();
                        holdStack.Push(myStack.Pop());
                        ViewBag.searchNotFound = "Item Found!";
                    }
                    else
                    {
                        holdStack.Push(myStack.Pop());
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Index");
            }

            while (holdStack.Count > 0)
            {
                myStack.Push(holdStack.Pop());
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            ViewBag.StopWatch = "Your search was completed in " + ts.TotalSeconds + " seconds";

            return View("Index");
        }
    }
}