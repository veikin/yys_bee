using System.Collections.Generic;
using YYS_Bee.Configs;
using YYS_Bee.Model;
using Newtonsoft.Json;
using System.Linq;

namespace YYS_Bee.Imp
{
    public class ConfigRepository
    {
        public static FileConfig fileConfig;
        public static List<GameIndexTemplate> templates;
        public static void ConfigInit(GameSetting gameSetting)
        {
            string appBaseDir = AppTools.GetAppBaseDir();
            fileConfig = new FileConfig()
            {
                WindowImageName = "currentGameImage.jpg",
                WindowImageTempDir = appBaseDir + "Temp",
                TemplateIndexImageDir = appBaseDir + "Templates\\"+ gameSetting.WindowName+ "\\Images",
                TemplatesPath = appBaseDir + "Templates\\" + gameSetting.WindowName + "\\templates.json"
            };
            templates = JsonConvert.DeserializeObject<List<GameIndexTemplate>>(System.IO.File.ReadAllText(fileConfig.TemplatesPath,System.Text.Encoding.GetEncoding("gb2312")));
        }

        public static List<GameIndexTemplate> GetCurrentTemplates()
        {
            var result= JsonConvert.DeserializeObject<List<GameIndexTemplate>>(System.IO.File.ReadAllText(fileConfig.TemplatesPath, System.Text.Encoding.GetEncoding("gb2312")));
            if (result != null)
                result = result.OrderBy(n => n.Order).ToList();
            return result;
        }

    }
}
