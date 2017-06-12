using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CADDiagramDrawing
{
    internal class TrainIDLabel : IDecorateLabel
    {
        /// <summary>
        /// 当前标记的方向
        /// </summary>
        internal Direction CurrentDirection { get; set; }
        /// <summary>
        /// 当前标记是否为始发终到标记
        /// </summary>
        internal bool IsTerminal { get; set; }
        /// <summary>
        /// 车次文字高度
        /// </summary>
        private double TextHeight { get; set; } = Parameters.TrainIDTextHeight;
        /// <summary>
        /// 标记竖线长度
        /// </summary>
        internal double VerticalBarHeight { get; set; } = Parameters.TrainIDVerticalBarHeight;
        /// <summary>
        /// 列车运行线起点X坐标
        /// </summary>
        private double OriginX { get; set; }
        /// <summary>
        /// 列车运行线起点Y坐标
        /// </summary>
        private double OriginY { get; set; }
        /// <summary>
        /// 基线Y坐标
        /// </summary>
        private double BaseLineY { get; set; }
        /// <summary>
        /// 基线左端点X坐标
        /// </summary>
        private double BaseLineLeft { get; set; }
        /// <summary>
        /// 基线右端点X坐标
        /// </summary>
        private double BaseLineRight { get; set; }
        /// <summary>
        /// 车次名
        /// </summary>
        internal string TrainName { get; set; }
        /// <summary>
        /// 车次标志基线宽
        /// </summary>
        private double BaseLineWidth { get; set; } = Parameters.TrainIDWidth;
        /// <summary>
        /// 车次位置占用矩形
        /// </summary>
        internal RectangleF Rect { get; set; } = new RectangleF();
        /// <summary>
        /// 绘制车次
        /// </summary>
        /// <param name="doc"></param>
        void IDecorateLabel.Draw(DXFLibrary.Document doc)
        {
            /*绘制T形标志*/
            DXFLibrary.Line l = null;
            if (CurrentDirection == Direction.Down)
                l = new DXFLibrary.Line("TrainID", OriginX, -OriginY, OriginX, -(OriginY - VerticalBarHeight));
            else
                l = new DXFLibrary.Line("TrainID", OriginX, -OriginY, OriginX, -(OriginY + VerticalBarHeight));
            doc.add(l);

            l = new DXFLibrary.Line("TrainID", BaseLineLeft, -BaseLineY, BaseLineRight, -BaseLineY);
            doc.add(l);

            /*接入标记的修饰*/
            if (!IsTerminal)
            {
                if (CurrentDirection == Direction.Down)
                    l = new DXFLibrary.Line("TrainID", BaseLineLeft, -BaseLineY, BaseLineLeft - Parameters.EnterDecorationDistance, -(BaseLineY - TextHeight));
                else
                    l = new DXFLibrary.Line("TrainID", BaseLineLeft, -BaseLineY, BaseLineLeft - Parameters.EnterDecorationDistance, -(BaseLineY + TextHeight));
                doc.add(l);
            }

            /*绘制文字*/
            DXFLibrary.Text t = new DXFLibrary.Text(TrainName, Rect.X, -(Rect.Y + TextHeight), TextHeight, "TrainID");
            doc.add(t);
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
                BaseLineY = Y - VerticalBarHeight;
            }
            else
            {
                BaseLineY = Y + VerticalBarHeight;
            }

            /*确定基线水平位置*/
            if(IsTerminal)
            {
                BaseLineLeft = X - BaseLineWidth / 2;
                BaseLineRight = X + BaseLineWidth / 2;
                if(CurrentDirection==Direction.Down)
                    Rect = new RectangleF((float)BaseLineLeft, (float)(BaseLineY - TextHeight), (float)BaseLineWidth, (float)TextHeight);
                else
                    Rect = new RectangleF((float)BaseLineLeft, (float)BaseLineY, (float)BaseLineWidth, (float)TextHeight);
            }
            else
            {
                BaseLineLeft = X - BaseLineWidth;
                BaseLineRight = X;
                if(CurrentDirection==Direction.Down)
                    Rect = new RectangleF((float)BaseLineLeft, (float)(BaseLineY - TextHeight), (float)BaseLineWidth, (float)TextHeight);
                else
                    Rect = new RectangleF((float)BaseLineLeft, (float)BaseLineY, (float)BaseLineWidth, (float)TextHeight);
            }
        }
        /// <summary>
        /// 调整车次框位置
        /// </summary>
        internal void AdjustTrainIDPosition()
        {
            VerticalBarHeight += Parameters.TrainIDVerticalBarHeight;
            Calculate(OriginX, OriginY);
        }
        
    }
}
