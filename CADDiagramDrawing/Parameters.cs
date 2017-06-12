using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADDiagramDrawing
{
    /// <summary>
    /// 参数设置
    /// </summary>
    static public class Parameters
    {
        #region 车次
        /// <summary>
        /// 车次标志竖线长度
        /// </summary>
        static public double TrainIDVerticalBarHeight { get; set; } = 5;
        /// <summary>
        /// 车次标志宽度
        /// </summary>
        static public double TrainIDWidth { get; set; } = 10;
        /// <summary>
        /// 车次文字高
        /// </summary>
        static public double TrainIDTextHeight { get; set; } = 3;
        /// <summary>
        /// 接入标志小斜线向左的偏移量
        /// </summary>
        static public double EnterDecorationDistance { get; set; } = 4;
        /// <summary>
        /// 结束标志宽度
        /// </summary>
        static public double EndLabelWidth { get; set; } = 5;
        #endregion

        #region 图面
        /// <summary>
        /// 图面高度
        /// </summary>
        static public double DiagramHeight { get; set; } = 500;
        /// <summary>
        /// 图面宽度
        /// </summary>
        static public double DiagramWidth { get; set; } = 1440;
        /// <summary>
        /// 图面边缘距离
        /// </summary>
        static public double DiagramMargin { get; set; } = 10;
        /// <summary>
        /// 运行图区块间间隔
        /// </summary>
        static public double BlockGap { get; set; } = 40;
        #endregion

        #region 时刻
        /// <summary>
        /// 时刻文字高
        /// </summary>
        static public double TimeTextHeight { get; set; } = 1;
        /// <summary>
        /// 时刻数字与运行线的偏移量
        /// </summary>
        static public double TimeExcurtion { get; set; } = 2;
        /// <summary>
        /// 时刻文字宽度
        /// </summary>
        static public double TimeTextWidth { get; set; } = 1;
        #endregion

    }
}
