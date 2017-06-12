using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXFLibrary;

namespace CADDiagramDrawing
{
    internal class TrainEndLabel : IDecorateLabel
    {
        /// <summary>
        /// 结束标志方向
        /// </summary>
        internal Direction CurrentDirection { get; set; }
        /// <summary>
        /// 结束标志是否为终到标志
        /// </summary>
        internal bool IsTerminal { get; set; }
        /// <summary>
        /// 标志竖线长度
        /// </summary>
        internal double VerticalBarHeight { get; set; } = Parameters.TrainIDVerticalBarHeight;
        /// <summary>
        /// 运行线终点X坐标
        /// </summary>
        private double OriginX { get; set; }
        /// <summary>
        /// 运行线终点Y坐标
        /// </summary>
        private double OriginY { get; set; }
        /// <summary>
        /// 基线垂直位置Y
        /// </summary>
        private double BaseLineY { get; set; }
        /// <summary>
        /// 基线左端X坐标
        /// </summary>
        private double BaseLineLeft { get; set; }
        /// <summary>
        /// 基线右端X坐标
        /// </summary>
        private double BaseLineRight { get; set; }
        /// <summary>
        /// 基线宽度
        /// </summary>
        private double BaseLineWidth { get; set; } = Parameters.EndLabelWidth;
        /// <summary>
        /// 绘制结束标志
        /// </summary>
        /// <param name="doc"></param>
        void IDecorateLabel.Draw(DXFLibrary.Document doc)
        {
            /*绘制T形标志*/
            DXFLibrary.Line l = null;
            if (CurrentDirection == Direction.Down)
                l = new DXFLibrary.Line("TrainID", OriginX, -OriginY, OriginX, -(OriginY + VerticalBarHeight));
            else
                l = new DXFLibrary.Line("TrainID", OriginX, -OriginY, OriginX, -(OriginY - VerticalBarHeight));
            doc.add(l);

            l = new DXFLibrary.Line("TrainID", BaseLineLeft, -BaseLineY, BaseLineRight, -BaseLineY);
            doc.add(l);

            /*交出标记的修饰*/
            if (IsTerminal)
            {
                if (CurrentDirection == Direction.Down)
                {
                    l = new DXFLibrary.Line("TrainID", BaseLineLeft, -BaseLineY, BaseLineLeft + BaseLineWidth / 2, -(BaseLineY + BaseLineWidth / 2));
                    doc.add(l);
                    l = new DXFLibrary.Line("TrainID", BaseLineRight, -BaseLineY, BaseLineRight - BaseLineWidth / 2, -(BaseLineY + BaseLineWidth / 2));
                    doc.add(l);
                }
                else
                {
                    l = new DXFLibrary.Line("TrainID", BaseLineLeft, -BaseLineY, BaseLineLeft + BaseLineWidth / 2, -(BaseLineY - BaseLineWidth / 2));
                    doc.add(l);
                    l = new DXFLibrary.Line("TrainID", BaseLineRight, -BaseLineY, BaseLineRight - BaseLineWidth / 2, -(BaseLineY - BaseLineWidth / 2));
                    doc.add(l);
                }
            }
            else
            {
                if (CurrentDirection == Direction.Down)
                {
                    l = new DXFLibrary.Line("TrainID", BaseLineRight, -BaseLineY, BaseLineRight - BaseLineWidth / 2, -(BaseLineY + BaseLineWidth / 2));
                    doc.add(l);
                }
                else
                {
                    l = new DXFLibrary.Line("TrainID", BaseLineRight, -BaseLineY, BaseLineRight - BaseLineWidth / 2, -(BaseLineY - BaseLineWidth / 2));
                    doc.add(l);
                }
            }
        }
        /// <summary>
        /// 计算坐标
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        internal void Calculate(double X, double Y)
        {
            OriginX = X;
            OriginY = Y;

            /*计算基线垂直位置*/
            if (CurrentDirection == Direction.Down)
            {
                BaseLineY = Y + VerticalBarHeight;
            }
            else
            {
                BaseLineY = Y - VerticalBarHeight;
            }

            /*根据是否为终到标志计算基线水平位置*/
            if (IsTerminal)
            {
                BaseLineLeft = X - BaseLineWidth / 2;
                BaseLineRight = X + BaseLineWidth / 2;                
            }
            else
            {
                BaseLineLeft = X;
                BaseLineRight = X + BaseLineWidth;               
            }
        }
    }


}