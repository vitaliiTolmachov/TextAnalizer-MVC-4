using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SortlistFileReader.Controllers
{

    public class HomeController : Controller
    {
        //Создаем клас который будет представлять нужное
        public class Word
        {
            public string charchter { get; set; }
            public int repeat { get; set; }
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Принимаем данные из формы
        /// </summary>
        /// <param name="file">Текст из файла</param>
        /// <param name="pastedText">текст из мемо-поля</param>
        /// <param name="downloadType">значение радиокнопки</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Result(HttpPostedFileBase file, string pastedText, string downloadType)
        {
            string tempString = null;
            //Если радикнопка была установлена в положении "Файл" тогда текст будем обрабатывать с файла
            if (downloadType == "file")
            {
                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes((int)file.InputStream.Length);
                tempString = System.Text.Encoding.UTF8.GetString(binData).ToLower();
            }
            //Если радикнопка была установлена в положении "мемо-поле" тогда текст будем обрабатывать с мемо-поля
            else
            {
                tempString = pastedText;
            }
            //Удаляем все что меньше 3 символов
            string textWithotSmallWords = Regex.Replace(tempString, @"\b\w{1,3}\b", " ");
            
            //Удаляем спецсимволы
            string someText = Regex.Replace(textWithotSmallWords, @"\b[^a-zа-я]+", " ");
            //Разбиваем всю строку на массив слова
            var res = someText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            
            //Групируем слова которые встречаются более 2 раз, используя LINQ
            var query = from x in res
                        group x by x into partition
                        where partition.Count() > 2
                        select new Word
                        { charchter = partition.Key, repeat = partition.Count() };
            //Передаем во вьюху коллекцию отобраных слов
            return View(query);

        }
    }
}