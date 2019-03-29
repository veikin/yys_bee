using System.Collections.Generic;

namespace YYS_Bee.Model
{
    public class GameSetting
    {
        /// <summary>
        /// 窗口名称
        /// </summary>
        public string WindowName { get; set; }
        /// <summary>
        /// WindowWidth
        /// </summary>
        public int WindowWidth { get; set; }
        /// <summary>
        /// WindowHeight
        /// </summary>
        public int WindowHeight { get; set; }
        /// <summary>
        /// 使用软件模拟  键鼠操作，不占用当前键鼠
        /// </summary>
        public bool IsSoftwareSimulation { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public List<GameIndexTemplate> Templates { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
}
