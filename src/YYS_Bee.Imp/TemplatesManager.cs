using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using YYS_Bee.Configs;
using YYS_Bee.Model;

namespace YYS_Bee.Imp
{
    public class TemplatesManager
    {
        /// <summary>
        /// 程序根目录
        /// </summary>
        private static string _appBaseDir = AppTools.GetAppBaseDir();

        public static List<GameSetting> GetAllGameSettings()
        {
            string dir = GetTemplateDir();

            string [] files=Directory.GetFiles(dir, "*.json");
            List<GameSetting> gameSettings=new List<GameSetting>();
            foreach (var path in files)
            {
                GameSetting gameSetting= JsonConvert.DeserializeObject<GameSetting>(File.ReadAllText(path, Encoding.GetEncoding("gb2312")));
                gameSetting.FileName = Path.GetFileName(path);
                gameSettings.Add(gameSetting);
            }
            return gameSettings;
        }
        public static GameSetting GetGameSettings(string fileName)
        {
            string path = Path.Combine(GetTemplateDir(), fileName);
            GameSetting gameSetting = JsonConvert.DeserializeObject<GameSetting>(File.ReadAllText(path, Encoding.GetEncoding("gb2312")));
            gameSetting.FileName = fileName;
            return gameSetting;
        }
        public static string GetTemplateDir()
        {
            return System.IO.Path.Combine(_appBaseDir, FileConfig.TemplatesPath);
        }
    }

}
