using System.Collections.Generic;

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
        /// 检测点
        /// </summary>
        public List<CheckPosition> CheckPosition { get; set; }
        /// <summary>
        /// 点击配置
        /// </summary>
        public List<ClickConfig> ClickConfig { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 排序——正序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 间隔（秒）
        /// </summary>
        public double Interval { get; set; }
    }
    public class CheckPosition
    {
        /// <summary>
        /// x轴
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// y轴
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 色值
        /// </summary>
        public string ColorValue { get; set; }
    }

    public class ClickConfig
    {
        /// <summary>
        /// x
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// x
        /// </summary>
        public int X_Max { get; set; }
        /// <summary>
        /// y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Y
        /// </summary>
        public int Y_Max { get; set; }
        /// <summary>
        /// 点击组
        /// </summary>
        public ClickSub [] ClickSub { get; set; }
        /// <summary>
        /// 点击结束后是否移动到随机的位置
        /// </summary>
        public bool ClickEndShowRandomPosition { get; set; }

        public int Order { get; set; }
    }
    /// <summary>
    /// 点击策略
    /// </summary>
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
