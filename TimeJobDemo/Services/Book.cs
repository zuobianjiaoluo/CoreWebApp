using System;

namespace TimeJobDemo.Services
{
    public class Book:IBook
    {
        public string GetDate()
        {
            return "时间："+DateTime.Now.ToLongTimeString();
        }
    }
}
