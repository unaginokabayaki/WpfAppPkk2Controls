using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppPkk2
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        // フィールドの既定のアクセス修飾子はprivate
        static string databaseName = "Shop.db";
        //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string folderPath = Environment.CurrentDirectory;
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);
    }
}
