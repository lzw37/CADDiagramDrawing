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
            /// <summary>
            /// 时间线分段的构造函数
            /// </summary>
            /// <param name="top">时间线分段顶部</param>
            /// <param name="bottom">时间线分段底部</param>
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
        /// <param name="doc">DXF文件对象</param>
        internal void Draw(DXFLibrary.Document doc)
        {
            string layerName = "0";
            switch(this.Type)//*根据时间线类型确定图层*/
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

            /*绘制时间线分段*/
            foreach (TimeLineTopAndBottom tl in TopAndBottomSet)
            {
                DXFLibrary.Line l = new DXFLibrary.Line(layerName, X, -tl.Top, X, -tl.Bottom);
                doc.add(l);
            }
        }
        /// <summary>
        /// 生成时间线分段
        /// </summary>
        /// <param name="top">上端</param>
        /// <param name="bottom">下端</param>
        internal void CreateSectors(double top, double bottom)
        {
            TopAndBottomSet.Add(new TimeLineTopAndBottom(top, bottom));
        }

        /// <summary>
        /// 计算坐标
        /// </summary>
        /// <param name="diagramLeft">区块左侧</param>
        /// <param name="XRatio">X坐标换算比例</param>
        internal void Calculate(double diagramLeft, double XRatio)
        {
            X = diagramLeft + (Time.Second + Time.Minute * 60 + Time.Hour * 3600) * XRatio;
        }

        /// <summary>
        /// 时间线种类
        /// </summary>
        internal TimeLineType Type { get; set; }

        /// <summary>
        /// 所代表的时间
        /// </summary>
        internal DateTime Time { get; set; }
    }
    /// <summary>
    /// 时间线类型
    /// </summary>
    internal enum TimeLineType
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