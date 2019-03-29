namespace YYS_Bee.Model
{
    /// <summary>
    /// 游戏查找模板
    /// </summary>
    public class GameIndexTemplate
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板图名称
        /// </summary>
        public string TemplateImageName { get; set; }
        /// <summary>
        /// 点击配置
        /// </summary>
        public ClickConfig ClickConfig { get; set; }
        /// <summary>
        /// 容错值
        /// </summary>
        public byte Similar { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 排序——正序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 执行间隔 秒
        /// </summary>
        public float Interval { get; set; }
    }

    public class ClickConfig
    {
        /// <summary>
        /// 是否使用模板的位置？不使用，则使用窗体范围
        /// </summary>
        public bool UseTemplateRange { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int X_Deviation { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int Y_Deviation { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int Max_X_Deviation { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int Max_Y_Deviation { get; set; }
        /// <summary>
        /// 点击组
        /// </summary>
        public ClickSub [] ClickSub { get; set; }
        /// <summary>
        /// 点击结束后是否移动到随机的位置
        /// </summary>
        public bool ClickEndShowRandomPosition { get; set; }
    }

    public class ClickSub
    {
        /// <summary>
        /// 随机启用点击
        /// </summary>
        public bool IsRandom { get; set; }
        /// <summary>
        /// 是否休眠-模拟手动 随机休眠
        /// </summary>
        public bool Sleeped { get; set; }
        /// <summary>
        /// 间隔最小值
        /// </summary>
        public int IntervalMin { get; set; }
        /// <summary>
        /// 间隔最大值
        /// </summary>
        public int IntervalMax { get; set; }
    }
}
