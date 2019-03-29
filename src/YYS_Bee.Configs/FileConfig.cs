using YYS_Bee.Model;

namespace YYS_Bee.Configs
{
    public class FileConfig
    {
        public  string WindowImageTempDir { get; set; }

        public string WindowImageName { get; set; }

        public string TemplateIndexImageDir { get; set; }
       
        public string GetTemplateIndexImagePath(string imageName)
        {
            return System.IO.Path.Combine(TemplateIndexImageDir, imageName);
        }
        public string TemplatesPath { get; set; }

    }

    
}
