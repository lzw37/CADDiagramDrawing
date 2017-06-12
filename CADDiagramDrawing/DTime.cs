using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 时间线
    /// </summary>
    public class DTime
    {
        /// <summary>
        /// 时间线分段
        /// </summary>
        internal class TimeLineTopAndBottom
        {
            internal TimeLineTopAndBottom(double top, double bottom)
            {
                Top = top;
                Bottom = bottom;
            }
            internal double Top;
            internal double Bottom;
        }
        /// <summary>
        /// 时间分段集合
        /// </summary>
        internal List<TimeLineTopAndBottom> TopAndBottomSet { get; set; } = new List<TimeLineTopAndBottom>();
        /// <summary>
        /// 时间线横坐标
        /// </summary>
        internal double X { get; set; }
        /// <summary>
        /// 绘制时间线
        /// </summary>
        /// <param name="doc"></param>
        internal void Draw(DXFLibrary.Document doc)
        {
            string layerName = "0";
            switch(this.Type)
            {
                case TimeLineType.HalfHour:
                    layerName = "HalfTimes";
                    break;
                case TimeLineType.Hour:
                    layerName = "HourTimes";
                    break;
                case TimeLineType.Normal:
                    layerName = "Times";
                    break;
            }
            foreach (TimeLineTopAndBottom tl in TopAndBottomSet)
            {
                DXFLibrary.Line l = new DXFLibrary.Line(layerName, X, -tl.Top, X, -tl.Bottom);
                doc.add(l);
            }
        }
        /// <summary>
        /// 生成时间线分段
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        internal void CreateSectors(double top, double bottom)
        {
            TopAndBottomSet.Add(new TimeLineTopAndBottom(top, bottom));
        }
        /// <summary>
        /// 计算坐标
        /// </summary>
        /// <param name="left"></param>
        /// <param name="XRatio"></param>
        internal void Calculate(double left, double XRatio)
        {
            X = left + (Time.Second + Time.Minute * 60 + Time.Hour * 3600) * XRatio;
        }
        /// <summary>
        /// 时间线种类
        /// </summary>
        public TimeLineType Type { get; set; }
        /// <summary>
        /// 所代表的时间
        /// </summary>
        public DateTime Time { get; set; }
    }
    /// <summary>
    /// 时间线类型
    /// </summary>
    public enum TimeLineType
    {
        /// <summary>
        /// 半小时线
        /// </summary>
        HalfHour,
        /// <summary>
        /// 十分线
        /// </summary>
        Normal,
        /// <summary>
        /// 小时线
        /// </summary>
        Hour,
    }
}