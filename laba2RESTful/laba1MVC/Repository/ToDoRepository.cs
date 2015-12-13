using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using laba1MVC.Models;

namespace laba1MVC.Repository
{
    public class List
    {
        public List<ToDoModel> list;
    }
    public static class ToDoRepository
    {
        public static void Save(List<ToDoModel> list)
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<ToDoModel>));
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            var wfile = new System.IO.StreamWriter(path);
            writer.Serialize(wfile, list);
            wfile.Close();
        }
        public static List<ToDoModel> Get()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.Xml.Serialization.XmlSerializer reader =
        new System.Xml.Serialization.XmlSerializer(typeof(List<ToDoModel>));
            System.IO.StreamReader file = new System.IO.StreamReader(
                path);
            List<ToDoModel> overview = (List<ToDoModel>)reader.Deserialize(file);
            file.Close();
            return overview;
        }
    }
}